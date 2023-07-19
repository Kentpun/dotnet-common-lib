﻿namespace HKSH.Common.AuditLogs.Models
{
    /// <summary>
    /// ActivityLog
    /// </summary>
    public class ActivityLog
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
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public string User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <value>
        /// The module.
        /// </value>
        public string Module { get; set; } = null!;

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; } = null!;

        /// <summary>
        /// Gets or sets the function.
        /// </summary>
        /// <value>
        /// The function.
        /// </value>
        public string Function { get; set; } = null!;

        /// <summary>
        /// Gets or sets the visti file no.
        /// </summary>
        /// <value>
        /// The visti file no.
        /// </value>
        public string VistiFileNo { get; set; } = null!;
    }
}