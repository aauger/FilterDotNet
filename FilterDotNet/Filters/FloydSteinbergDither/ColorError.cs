namespace FilterDotNet.Filters
{
    public class ColorError
    {
        public int RedError { get; set; }
        public int GreenError { get; set; }
        public int BlueError { get; set; }
        public int Average() => ((RedError*RedError) + (GreenError*GreenError) + (BlueError*BlueError)) / 3;
        public int SqAverage() => (int)Math.Sqrt(((RedError * RedError) + (GreenError * GreenError) + (BlueError * BlueError)));
    }
}
