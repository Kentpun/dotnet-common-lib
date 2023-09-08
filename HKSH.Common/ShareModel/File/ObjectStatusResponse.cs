namespace HKSH.Common.ShareModel.File
{
    /// <summary>
    /// 对象状态
    /// </summary>
    public class ObjectStatusResponse
    {
        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        /// <value>
        /// The name of the object.
        /// </value>
        public string ObjectName { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public long Size { get; private set; }

        /// <summary>
        /// Gets the last modified.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        public DateTime LastModified { get; private set; }

        /// <summary>
        /// Gets the e tag.
        /// </summary>
        /// <value>
        /// The e tag.
        /// </value>
        public string ETag { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the meta data.
        /// </summary>
        /// <value>
        /// The meta data.
        /// </value>
        public Dictionary<string, string> MetaData { get; } = new();

        /// <summary>
        /// Gets the version identifier.
        /// </summary>
        /// <value>
        /// The version identifier.
        /// </value>
        public string VersionId { get; private set; } = string.Empty;

        /// <summary>
        /// Gets a value indicating whether [delete marker].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete marker]; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteMarker { get; private set; }

        /// <summary>
        /// Gets the expires.
        /// </summary>
        /// <value>
        /// The expires.
        /// </value>
        public DateTime? Expires { get; private set; }
    }
}