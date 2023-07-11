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
        public const string SECTION = "FileOptions";

        /// <summary>
        /// The directory section
        /// </summary>
        public const string DIRECTORY_SECTION = "Directory";

        /// <summary>
        /// The handler section
        /// </summary>
        public const string HANDLER_SECTION = "Handler";

        /// <summary>
        /// 節點當前所屬命名空間
        /// </summary>
        public const string MY_POD_NAME_SPACE = "MY_POD_NAMESPACE";

        /// <summary>
        /// 默認命名空間
        /// </summary>
        public const string DEFAULT_NAME_SPACE = "demo";

        /// <summary>
        /// 默認文件存儲根路徑
        /// </summary>
        public const string DEFAULT_BASE_PATH = "File/Common";

        /// <summary>
        /// Gets or sets the name space.
        /// </summary>
        /// <value>
        /// The name space.
        /// </value>
        public string NameSpace { get; set; } = null!;

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
    /// FileDirectoryOptions
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
    /// FileHandlerOptions
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