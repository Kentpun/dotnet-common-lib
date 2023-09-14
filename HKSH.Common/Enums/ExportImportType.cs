namespace HKSH.Common.Enums
{
    /// <summary>
    /// Export and import type
    /// </summary>
    public class ExportImportType
    {
        /// <summary>
        /// The dic
        /// </summary>
        private static readonly Dictionary<string, ExportImportType> dic = new Dictionary<string, ExportImportType>();

        /// <summary>
        /// The export
        /// </summary>
        public static readonly ExportImportType Export = new ExportImportType("Export", "Export");

        /// <summary>
        /// The import
        /// </summary>
        public static readonly ExportImportType Import = new ExportImportType("Import", "Import");

        /// <summary>
        /// The code
        /// </summary>
        private readonly string _code;

        /// <summary>
        /// The value
        /// </summary>
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportImportType"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="value">The value.</param>
        private ExportImportType(string code, string value)
        {
            _code = code;
            _value = value;
            dic.Add(_value, this);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="ExportImportType"/>.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentException">Invalid export import type code.</exception>
        public static implicit operator ExportImportType(string code)
        {
            if (dic.ContainsKey(code))
            {
                return dic[code];
            }
            throw new ArgumentException("Invalid export import type code.", code);
        }

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <returns></returns>
        public string GetCode()
        {
            return _code;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        public string GetValue()
        {
            return _value;
        }
    }
}