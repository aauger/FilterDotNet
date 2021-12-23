namespace AAImageFilter.Interfaces
{
    public interface IAnalyzer
    {
        string Name { get; }
        string Analyze(IImage input);
    }
}
