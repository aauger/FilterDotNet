using FilterDotNet.Interfaces;
using FilterDotNet.LibraryConfigurators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDotNet.Filters
{
    public class SobelFilter : IFilter
    {
        /* DI */
        private readonly IEngine _engine;

        /* Properties */
        public string Name => "Sobel Edge Detection";

        public SobelFilter(IEngine engine)
        {
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._engine.CreateImage(input.Width, input.Height);

            var sobelLeftRightConfigurator = CreateSobelConfigurator(new [,]{ { 1.0, 0.0, -1.0 }, { 2.0, 0.0, -2.0 }, { 1.0, 0.0, -1.0 } });

            IImage sobelLeftRight = CreateSobelFilter(sobelLeftRightConfigurator)
                .Apply(input);

            var sobelTopBottomConfigurator = CreateSobelConfigurator(new [,]{ { 1.0, 2.0, 1.0 }, { 0.0, 0.0, 0.0 }, { -1.0, -2.0, -1.0 } });

            IImage sobelTopBottom = CreateSobelFilter(sobelTopBottomConfigurator)
                .Apply(input);

            Parallel.For(0, output.Width, (int x) => {
                Parallel.For(0, output.Height, (int y) => {
                    IColor lrc = sobelLeftRight.GetPixel(x, y);
                    IColor tbc = sobelTopBottom.GetPixel(x, y);

                    IColor blended = this._engine.CreateColor(
                        (lrc.R + tbc.R) / 2,
                        (lrc.G + tbc.G) / 2,
                        (lrc.B + tbc.B) / 2,
                        255
                        );

                    output.SetPixel(x, y, blended);
                });
            });

            return output;
        }

        private static LambdaPluginConfigurator<ConvolutionConfiguration> CreateSobelConfigurator(double[,] values)
        {
            return new LambdaPluginConfigurator<ConvolutionConfiguration>(() => new() {
                Bias = 1.0,
                Normalize = false,
                Values = values
            });
        }

        private ConvolutionFilter CreateSobelFilter(IPluginConfigurator<ConvolutionConfiguration> configurator)
        {
            return (ConvolutionFilter)new ConvolutionFilter(configurator, this._engine).Initialize();
        }
    }
}
