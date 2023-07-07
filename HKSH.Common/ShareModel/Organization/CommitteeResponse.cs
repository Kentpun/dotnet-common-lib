namespace HKSH.Common.ShareModel.Organization
{
    /// <summary>
    /// Committee Response
    /// </summary>
    public class CommitteeResponse
    {
        /// <summary>
        /// Gets or sets the committee identifier.
        /// </summary>
        /// <value>
        /// The committee identifier.
        /// </value>
        public int CommitteeId { get; set; }

        /// <summary>
        /// Gets or sets the committee code.
        /// </summary>
        /// <value>
        /// The committee code.
        /// </value>
        public string? CommitteeCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the committee sort order.
        /// </summary>
        /// <value>
        /// The committee sort order.
        /// </value>
        public decimal? CommitteeSortOrder { get; set; }

        /// <summary>
        /// Gets or sets the committee full name en.
        /// </summary>
        /// <value>
        /// The committee full name en.
        /// </value>
        public string? CommitteeFullNameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the committee full name tc.
        /// </summary>
        /// <value>
        /// The committee full name tc.
        /// </value>
        public string? CommitteeFullNameTc { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the committee short name en.
        /// </summary>
        /// <value>
        /// The committee short name en.
        /// </value>
        public string? CommitteeShortNameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the committee short name tc.
        /// </summary>
        /// <value>
        /// The committee short name tc.
        /// </value>
        public string? CommitteeShortNameTc { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the correspondence address en.
        /// </summary>
        /// <value>
        /// The correspondence address en.
        /// </value>
        public string? CorrespondenceAddressEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the correspondence address tc.
        /// </summary>
        /// <value>
        /// The correspondence address tc.
        /// </value>
        public string? CorrespondenceAddressTc { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the committee alias.
        /// </summary>
        /// <value>
        /// The committee alias.
        /// </value>
        public string? CommitteeAlias { get; set; } = string.Empty;
    }
}