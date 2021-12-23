namespace AAImageFilter.Interfaces
{
    public interface IFilter
    {
        string Name { get; }
        IImage Apply(IImage input);
    }
}
