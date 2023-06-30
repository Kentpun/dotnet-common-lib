using System.ComponentModel;

namespace HKSH.Common.Enums
{
    /// <summary>
    /// 狀態
    /// </summary>
    public enum TaskStatus
    {
        /// <summary>
        /// 新任務
        /// </summary>
        [Description("New")] New = 1001,

        /// <summary>
        /// 處理中
        /// </summary>
        [Description("InProgress")] InProgress = 1002,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("Completed")] Completed = 1003,

        /// <summary>
        /// 失敗任務
        /// </summary>
        [Description("Failed")] Failed = 1004,

        /// <summary>
        /// 已取消
        /// </summary>
        [Description("Canceled")] Canceled = 1005,
    }
}