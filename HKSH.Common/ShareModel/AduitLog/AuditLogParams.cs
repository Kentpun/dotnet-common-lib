namespace HKSH.Common.ShareModel.AduitLog
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
        /// 業務關聯字段是否自動拼接主鍵.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [business type join primary key]; otherwise, <c>false</c>.
        /// </value>
        public bool? BusinessTypeJoinPrimaryKey { get; set; }

        /// <summary>
        /// 前端展示部分.
        /// </summary>
        /// <value>
        /// The section.
        /// </value>
        public string? Section { get; set; }
    }
}