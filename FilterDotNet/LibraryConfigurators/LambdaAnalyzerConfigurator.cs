using FilterDotNet.Interfaces;

namespace FilterDotNet.LibraryConfigurators
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
