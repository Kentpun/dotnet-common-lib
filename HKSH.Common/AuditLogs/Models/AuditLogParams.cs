namespace HKSH.Common.AuditLogs.Models
{
    /// <summary>
    /// SaveChanges 審計日誌傳遞參數
    /// </summary>
    public class AuditLogParams
    {
        /// <summary>
        /// 模塊.
        /// </summary>
        /// <value>
        /// The module.
        /// </value>
        public string Module { get; set; } = null!;

        /// <summary>
        /// 業務關聯字段.
        /// </summary>
        /// <value>
        /// The type of the business.
        /// </value>
        public string BusinessType { get; set; } = null!;

        /// <summary>
        /// 前端展示部分.
        /// </summary>
        /// <value>
        /// The section.
        /// </value>
        public string? Section { get; set; }
    }
}
