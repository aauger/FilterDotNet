using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        List<IImage>? _patchChunks;

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

            throw new NotImplementedException();
        }

        public IFilter Initialize()
        {
            (this._patchSource, this._patchWidth, this._patchHeight) = this._pluginConfigurator.GetPluginConfiguration();
            this._patchChunks = Chunkify(this._patchSource, this._patchWidth, this._patchHeight);
            this._ready = true;
            return this;
        }

        private List<IImage> Chunkify(IImage patchSource, int patchWidth, int patchHeight)
        {
            List<IImage> patchChunks = new List<IImage>();
            Parallel.For(0, patchSource.Width / patchWidth, (int xp) =>
            {
                Parallel.For(0, patchSource.Height / patchHeight, (int yp) =>
                {
                    int x = xp * patchWidth;
                    int y = yp * patchHeight;
                    int widthCurrentPatch = patchSource.Width - x;
                    int heightCurrentPatch = patchSource.Height - y;
                    IImage currentPatch = this._engine.CreateImage(widthCurrentPatch, heightCurrentPatch);
                    for (int xz = 0; xz < widthCurrentPatch; xz++)
                    {
                        for (int yz = 0; yz < heightCurrentPatch; yz++)
                        {
                            currentPatch.SetPixel(xz, yz, patchSource.GetPixel(xz + x, yz + y));
                        }
                    }
                    patchChunks.Add(currentPatch);
                });
            });
            return patchChunks;
        }
    }
}
