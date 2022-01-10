namespace FilterDotNet.Interfaces
{
    public interface IAnalyzerConfigurator<T>
    {
        T GetAnalyzerConfiguration();
    }
}
