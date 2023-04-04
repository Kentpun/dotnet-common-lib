namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// 分页查询响应模型
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