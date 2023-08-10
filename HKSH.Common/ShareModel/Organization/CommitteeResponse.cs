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
        public string CommitteeCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the committee full name en.
        /// </summary>
        /// <value>
        /// The committee full name en.
        /// </value>
        public string? CommitteeFullNameEn { get; set; }

        /// <summary>
        /// Gets or sets the committee full name tc.
        /// </summary>
        /// <value>
        /// The committee full name tc.
        /// </value>
        public string? CommitteeFullNameTc { get; set; }

        /// <summary>
        /// Gets or sets the committee short name en.
        /// </summary>
        /// <value>
        /// The committee short name en.
        /// </value>
        public string? CommitteeShortNameEn { get; set; }

        /// <summary>
        /// Gets or sets the committee short name tc.
        /// </summary>
        /// <value>
        /// The committee short name tc.
        /// </value>
        public string? CommitteeShortNameTc { get; set; }

        /// <summary>
        /// Gets or sets the correspondence address en.
        /// </summary>
        /// <value>
        /// The correspondence address en.
        /// </value>
        public string? CorrespondenceAddressEn { get; set; }

        /// <summary>
        /// Gets or sets the correspondence address tc.
        /// </summary>
        /// <value>
        /// The correspondence address tc.
        /// </value>
        public string? CorrespondenceAddressTc { get; set; }

        /// <summary>
        /// Gets or sets the committee alias.
        /// </summary>
        /// <value>
        /// The committee alias.
        /// </value>
        public string? CommitteeAlias { get; set; }

        /// <summary>
        /// Gets or sets the location full name en.
        /// </summary>
        /// <value>
        /// The location full name en.
        /// </value>
        public string? LocationFullNameEn { get; set; }

        /// <summary>
        /// Gets or sets the location full name tc.
        /// </summary>
        /// <value>
        /// The location full name tc.
        /// </value>
        public string? LocationFullNameTc { get; set; }

        /// <summary>
        /// Gets or sets the type of the committee.
        /// </summary>
        /// <value>
        /// The type of the committee.
        /// </value>
        public string? CommitteeType { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>
        /// The fax.
        /// </value>
        public string? Fax { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string? Email { get; set; }
    }
}