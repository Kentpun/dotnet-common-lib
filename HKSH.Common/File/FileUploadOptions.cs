namespace HKSH.Common.File
{
    /// <summary>
    /// FileOptions
    /// </summary>
    public class FileUploadOptions
    {
        /// <summary>
        /// The section
        /// </summary>
        public const string Section = "FileOptions";

        /// <summary>
        /// The directory section
        /// </summary>
        public const string DirectorySection = "Directory";

        /// <summary>
        /// The handler section
        /// </summary>
        public const string HandlerSection = "Handler";

        /// <summary>
        /// 节点当前所属命名空间
        /// </summary>
        public const string MyPodNameSpace = "MY_POD_NAMESPACE";

        /// <summary>
        /// 默认命名空间
        /// </summary>
        public const string DefaultNameSpace = "demo";

        /// <summary>
        /// 默认文件存储根路径
        /// </summary>
        public const string DefaultBasePath = "File/Common";

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        /// <value>
        /// The directory.
        /// </value>
        public List<FileDirectoryOptions>? Directory { get; set; }

        /// <summary>
        /// Gets or sets the handler.
        /// </summary>
        /// <value>
        /// The handler.
        /// </value>
        public List<FileHandlerOptions>? Handler { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class FileDirectoryOptions
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the request path.
        /// </summary>
        /// <value>
        /// The request path.
        /// </value>
        public string RequestPath { get; set; } = string.Empty;
    }

    /// <summary>
    ///
    /// </summary>
    public class FileHandlerOptions
    {
        /// <summary>
        /// Gets or sets the base path.
        /// </summary>
        /// <value>
        /// The base path.
        /// </value>
        public string BasePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether [random file name].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [random file name]; otherwise, <c>false</c>.
        /// </value>
        public bool RandomFileName { get; set; } = true;

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; } = string.Empty;
    }
}