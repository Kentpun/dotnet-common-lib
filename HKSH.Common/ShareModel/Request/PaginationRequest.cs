namespace HKSH.Common.ShareModel.Request
{
    /// <summary>
    /// 分頁查詢請求模型
    /// </summary>
    public class PaginationRequest
    {
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets the index of the page.
        /// </summary>
        /// <value>
        /// The index of the page.
        /// </value>
        public int PageIndex
        {
            get
            {
                return Page < 1 ? 0 : Page - 1;
            }
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        /// <value>
        /// The order by.
        /// </value>
        public string? OrderBy { get; set; }
    }
}