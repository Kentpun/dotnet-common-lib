using XxlJob.Core.Model;

namespace HKSH.Common.XxlJob
{
    public interface IJobBaseHandler
    {
        Task<ReturnT> Execute(JobContext context);
    }
}