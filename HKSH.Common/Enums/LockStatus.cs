namespace HKSH.Common.Enums
{
    /// <summary>
    /// LockStatus
    /// </summary>
    public class LockStatus
    {
        /// <summary>
        /// The dic
        /// </summary>
        private static readonly Dictionary<string, LockStatus> dic = new Dictionary<string, LockStatus>();

        /// <summary>
        /// The english
        /// </summary>
        public static readonly LockStatus No = new LockStatus("No", "No");

        /// <summary>
        /// The chinese
        /// </summary>
        public static readonly LockStatus Yes = new LockStatus("Yes", "Yes");

        /// <summary>
        /// The code
        /// </summary>
        private readonly string _code;

        /// <summary>
        /// The value
        /// </summary>
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="LockStatus"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="value">The value.</param>
        private LockStatus(string code, string value)
        {
            _code = code;
            _value = value;
            dic.Add(_code, this);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="LockStatus"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentException">Invalid active status value. - value</exception>
        public static implicit operator LockStatus(string code)
        {
            if (dic.ContainsKey(code))
            {
                return dic[code];
            }
            else
            {
                throw new ArgumentException("Invalid lock status code.", code);
            }
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