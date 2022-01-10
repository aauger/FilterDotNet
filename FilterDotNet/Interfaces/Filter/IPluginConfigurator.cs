namespace FilterDotNet.Interfaces
{
    public interface IPluginConfigurator<T>
    {
        T GetPluginConfiguration();
    }
}
