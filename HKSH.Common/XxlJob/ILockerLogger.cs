namespace HKSH.Common.XxlJob
{
    /// <summary>
    /// Locker版日志执行器
    /// </summary>
    public interface ILockerLogger
    {
        void Log(string pattern, params object[] format);

        void LogError(Exception ex);
    }
}