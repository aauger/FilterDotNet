using AAImageFilter.Interfaces;

namespace AAImageFilter.LibraryConfigurators
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
            return _lambda();
        }
    }
}
