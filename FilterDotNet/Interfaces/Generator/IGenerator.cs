namespace FilterDotNet.Interfaces
{
    public interface IGenerator
    {
        string Name { get; }
        IImage Generate();
    }
}
