namespace HKSH.Common.ShareModel.ExportImport
{
    /// <summary>
    /// ImportExportTaskProgress
    /// </summary>
    public class ImportExportTaskProgressResponse
    {
        /// <summary>
        /// Gets or sets the task code.
        /// </summary>
        /// <value>
        /// The task code.
        /// </value>
        public string? TaskCode { get; set; }

        /// <summary>
        /// Gets or sets the progress.
        /// </summary>
        /// <value>
        /// The progress.
        /// </value>
        public int Progress { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// Source HKSH.Common/Enums/TaskStatus
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; } = string.Empty;
    }
}