﻿using HKSH.Common.ShareModel.Request;

namespace HKSH.Common.ShareModel.User
{
    /// <summary>
    /// UserInfo Page Request
    /// </summary>
    /// <seealso cref="KeywordPageRequest" />
    public class UserInfoPageRequest : KeywordPageRequest
    {
        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>
        /// The group.
        /// </value>
        public string? Group { get; set; }

        /// <summary>
        /// Gets or sets the exclude user ids.
        /// </summary>
        /// <value>
        /// The exclude user ids.
        /// </value>
        public List<string>? ExcludeUserIds { get; set; }

        /// <summary>
        /// Gets or sets the location code.
        /// </summary>
        /// <value>
        /// The location code.
        /// </value>
        public string? LocationCode { get; set; }

        /// <summary>
        /// Gets or sets the department code.
        /// </summary>
        /// <value>
        /// The department code.
        /// </value>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the role.
        /// </summary>
        /// <value>
        /// The type of the role.
        /// </value>
        public string? RoleType { get; set; }
    }
}