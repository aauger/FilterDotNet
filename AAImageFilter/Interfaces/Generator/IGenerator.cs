namespace AAImageFilter.Interfaces
{
    public interface IGenerator
    {
        string Name { get; }
        IImage Generate();
    }
}
