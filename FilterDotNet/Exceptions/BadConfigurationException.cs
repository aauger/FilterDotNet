using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDotNet.Exceptions
{
    public class BadConfigurationException : Exception
    {
        public BadConfigurationException(string message) : base(message)
        {
        }
    }
}
