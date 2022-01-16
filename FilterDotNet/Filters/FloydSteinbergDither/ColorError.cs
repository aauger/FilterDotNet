using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDotNet.Filters
{
    public class ColorError
    {
        public int RedError { get; set; }
        public int GreenError { get; set; }
        public int BlueError { get; set; }
        public int Avg() => ((RedError*RedError) + (GreenError*GreenError) + (BlueError*BlueError)) / 3;
    }
}
