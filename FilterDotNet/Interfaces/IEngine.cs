namespace FilterDotNet.Interfaces
{
    public interface IEngine
    {
        Func<int, int, IImage> CreateImage { get; }
        Func<int, int, int, int, IColor> CreateColor { get; }
        Func<int, int> Clamp { get; }
        int MaxValue { get; }
        int MinValue { get; }
    }
}
