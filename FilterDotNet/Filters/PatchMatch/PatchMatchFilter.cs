using FilterDotNet.Exceptions;
using FilterDotNet.Extensions;
using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class PatchMatchFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<(IImage, int, int)> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private bool _ready = false;
        private IImage? _patchSource;
        private int _patchWidth;
        private int _patchHeight;
        List<(int, int)>? _patchChunks;

        /* Properties */
        public string Name => "Patch Match";

        public PatchMatchFilter(IPluginConfigurator<(IImage, int, int)> pluginConfigurator, IEngine engine)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage output = this._engine.CreateImage(input.Width, input.Height);
            Parallel.For(0, input.Width / this._patchWidth, (int xp) =>
            {
                Parallel.For(0, input.Height / this._patchHeight, (int yp) =>
                {
                    int x = xp * this._patchWidth;
                    int y = yp * this._patchHeight;
                    (int xOff, int yOff) = BestFitPatch(input, this._patchSource!, x, y, this._patchChunks);
                    for (int xz = 0; xz < this._patchWidth; xz++)
                    {
                        for (int yz = 0; yz < this._patchHeight; yz++)
                        {
                            if (!output.OutOfBounds(xz + x, yz + y))
                            {
                                output.SetPixel(xz + x, yz + y, this._patchSource!.GetPixel(xz + xOff, yz + yOff));
                            }

                        }
                    }
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            (this._patchSource, this._patchWidth, this._patchHeight) = this._pluginConfigurator.GetPluginConfiguration();
            this._patchChunks = Chunkify(this._patchSource, this._patchWidth, this._patchHeight);
            this._ready = true;
            return this;
        }

        private List<(int, int)> Chunkify(IImage patchSource, int patchWidth, int patchHeight)
        {
            List<(int, int)> patchChunks = new();
            for (int xp = 0; xp < patchSource.Width / patchWidth; xp++)
            {
                for (int yp = 0; yp < patchSource.Height / patchHeight; yp++)
                {
                    int x = xp * patchWidth;
                    int y = yp * patchHeight;
                    patchChunks.Add((x, y));
                }
            }
            return patchChunks;
        }

        private (int, int) BestFitPatch(IImage input, IImage patchSource, int x, int y, List<(int, int)>? patchChunks)
        {
            (int, int) bestChunk = patchChunks!.AsParallel().MinBy(pc =>
             {
                 int sumErrors = 0;
                 for (int xz = 0; xz < this._patchWidth!; xz++)
                 {
                     for (int yz = 0; yz < this._patchHeight!; yz++)
                     {
                         if (input.OutOfBounds(xz + x, yz + y) || patchSource.OutOfBounds(xz + pc.Item1, yz + pc.Item2))
                         {
                             sumErrors += 441;
                             continue;
                         }

                         IColor real = input.GetPixel(xz + x, yz + y);
                         IColor patch = patchSource.GetPixel(xz + pc.Item1, yz + pc.Item2);

                         sumErrors +=
                             (int)Math.Sqrt(((real.R - patch.R) * (real.R - patch.R) +
                              (real.G - patch.G) * (real.G - patch.G) +
                              (real.B - patch.B) * (real.B - patch.B)));
                     }
                 }
                 return sumErrors;
             })!;
            return bestChunk;
        }
    }
}
