using System.ComponentModel;

namespace HKSH.Common.Enums
{
    /// <summary>
    /// Export and import type
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