using FilterDotNet.Generators;
using FilterDotNet.Interfaces;
using static FastImageProvider.Injectables;

namespace NET6ImageFilter
{
    public class GeneratorInitializer
    {
        public static void AddGenerators(List<IGenerator> list)
        {
            list.AddRange(new IGenerator[]
            {
                new CircleFillTestGenerator(FiEngine),
                new LineDrawTestGenerator(FiEngine),
                new MandelbrotGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiEngine),
                new PerlinNoiseGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiEngine),
                new TriangleDrawTestGenerator(FiEngine),
                new TriangleFillTestGenerator(FiEngine),
                new XyModGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiEngine),
            });
        }
    }
}