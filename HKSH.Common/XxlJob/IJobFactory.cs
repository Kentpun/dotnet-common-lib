namespace HKSH.Common.XxlJob
{
    public interface IJobFactory
    {
        IJobBaseHandler GetJobHandler(IServiceProvider provider, string handlerName);
    }
}