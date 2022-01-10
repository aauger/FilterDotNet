namespace FilterDotNet.Interfaces
{
    public interface IAnalyzer
    {
        string Name { get; }
        string Analyze(IImage input);
    }
}
