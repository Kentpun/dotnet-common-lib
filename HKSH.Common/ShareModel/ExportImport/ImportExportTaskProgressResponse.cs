using TaskStatus = HKSH.Common.Enums.TaskStatus;

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
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public TaskStatus Status { get; set; }
    }
}