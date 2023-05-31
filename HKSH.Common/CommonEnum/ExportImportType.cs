using System.ComponentModel;

namespace HKSH.Common.CommonEnum
{
    /// <summary>
    /// ExportImportType
    /// </summary>
    public enum ExportImportType
    {
        /// <summary>
        /// The export
        /// </summary>
        [Description("Export")]
        Export = 1001,

        /// <summary>
        /// The import
        /// </summary>
        [Description("Import")]
        Import = 1002
    }
}