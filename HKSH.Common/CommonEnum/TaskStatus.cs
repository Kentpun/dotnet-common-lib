

using System.ComponentModel;

namespace HKSH.Common.CommonEnum
{
    /// <summary>
    /// 状态
    /// </summary>
    public enum TaskStatus
    {
        /// <summary>
        /// 新任务
        /// </summary>
        [Description("New")] New = 1001,

        /// <summary>
        /// 处理中
        /// </summary>
        [Description("InProgress")] InProgress = 1002,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("Completed")] Completed = 1003,

        /// <summary>
        /// 失败任务
        /// </summary>
        [Description("Failed")] Failed = 1004,

        /// <summary>
        /// 已取消
        /// </summary>
        [Description("Canceled")] Canceled = 1005,
    }
}