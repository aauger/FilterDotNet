using FilterDotNet.Interfaces;

namespace FilterDotNet.LibraryConfigurators
{
    public class LambdaPluginConfigurator<T> : IPluginConfigurator<T>
    {
        private readonly Func<T> _lambda;

        public LambdaPluginConfigurator(Func<T> lambda)
        {
            _lambda = lambda;
        }

        public T GetPluginConfiguration()
        {
            return this._lambda();
        }
    }
}
