using AAImageFilter.Interfaces;

namespace AAImageFilter.LibraryConfigurators
{
    public class LambdaAnalyzerConfigurator<T> : IAnalyzerConfigurator<T>
    {
        private readonly Func<T> _lambda;

        public LambdaAnalyzerConfigurator(Func<T> lambda)
        { 
            this._lambda = lambda;
        }

        public T GetAnalyzerConfiguration()
        {
            return this._lambda();
        }
    }
}
