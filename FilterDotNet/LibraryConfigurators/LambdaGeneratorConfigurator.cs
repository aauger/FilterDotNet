using FilterDotNet.Interfaces;

namespace FilterDotNet.LibraryConfigurators
{
    public class LambdaGeneratorConfigurator<T> : IGeneratorConfigurator<T>
    {
        private readonly Func<T> _lambda;

        public LambdaGeneratorConfigurator(Func<T> lambda)
        { 
            this._lambda = lambda;
        }

        public T GetGeneratorConfiguration()
        {
            return this._lambda();
        }
    }
}
