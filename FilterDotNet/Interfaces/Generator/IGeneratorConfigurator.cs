namespace FilterDotNet.Interfaces
{
    public interface IGeneratorConfigurator<T>
    {
        T GetGeneratorConfiguration();
    }
}
