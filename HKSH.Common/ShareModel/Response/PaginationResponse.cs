namespace HKSH.Common.ShareModel.Response
{
    /// <summary>
    /// 分頁查詢響應模型
    /// </summary>
    public class PaginationResponse<T> where T : class, new()
    {
        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<T>? Items { get; set; }
    }
}