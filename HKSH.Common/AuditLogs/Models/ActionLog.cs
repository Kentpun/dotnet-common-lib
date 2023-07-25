namespace HKSH.Common.AuditLogs.Models
{
    /// <summary>
    /// ActionLog
    /// </summary>
    public class ActionLog
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; } = null!;

        /// <summary>
        /// Gets or sets the action date.
        /// </summary>
        /// <value>
        /// The action date.
        /// </value>
        public DateTime ActionDate { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public string User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        /// <value>
        /// The device.
        /// </value>
        public string Device { get; set; } = null!;

        /// <summary>
        /// Gets or sets the function.
        /// </summary>
        /// <value>
        /// The function.
        /// </value>
        public string Function { get; set; } = null!;


        /// <summary>
        /// Gets or sets the function identifier.
        /// </summary>
        /// <value>
        /// The function identifier.
        /// </value>
        public string FunctionId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the sub function.
        /// </summary>
        /// <value>
        /// The sub function.
        /// </value>
        public string SubFunction { get; set; } = null!;

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public string PatientID { get; set; } = null!;

        /// <summary>
        /// Gets or sets the visti file no.
        /// </summary>
        /// <value>
        /// The visti file no.
        /// </value>
        public string VistiFileNo { get; set; } = null!;

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; } = null!;

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; } = null!;
    }
}