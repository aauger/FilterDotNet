using AAImageFilter.Interfaces;
using AAImageFilter.LibraryConfigurators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Filters
{
    public class SobelFilter : IFilter
    {
        /* DI */
        private readonly Func<int, int, IImage> _imageCreator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        public string Name => "Sobel Edge Detection";

        public SobelFilter(Func<int, int, IImage> imageCreator, Func<int, int, int, int, IColor> colorCreator)
        { 
            this._imageCreator = imageCreator;
            this._colorCreator = colorCreator;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._imageCreator(input.Width, input.Height);

            var sobelLeftRightConfigurator = new LambdaPluginConfigurator<ConvolutionConfiguration>(() => new ConvolutionConfiguration
            {
                Bias = 1.0,
                Normalize = false,
                Values = new double[,] { { 1.0, 0.0, -1.0 }, { 2.0, 0.0, -2.0 }, { 1.0, 0.0, -1.0 } }
            });

            IImage sobelLeftRight = new ConvolutionFilter(sobelLeftRightConfigurator,             
                this._imageCreator,
                this._colorCreator)
                .Initialize()
                .Apply(input);

            var sobelTopBottomConfigurator = new LambdaPluginConfigurator<ConvolutionConfiguration>(() => new ConvolutionConfiguration
            {
                Bias = 1.0,
                Normalize = false,
                Values = new double[,] { { 1.0, 2.0, 1.0 }, { 0.0, 0.0, 0.0 }, { -1.0, -2.0, -1.0 } }
            });

            IImage sobelTopBottom = new ConvolutionFilter(sobelTopBottomConfigurator,
                this._imageCreator,
                this._colorCreator)
                .Initialize()
                .Apply(input);

            Parallel.For(0, output.Width, (int x) => {
                Parallel.For(0, output.Height, (int y) => {
                    IColor lrc = sobelLeftRight.GetPixel(x, y);
                    IColor tbc = sobelTopBottom.GetPixel(x, y);

                    IColor blended = this._colorCreator(
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
    }
}
