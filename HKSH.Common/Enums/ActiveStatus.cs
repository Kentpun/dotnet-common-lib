namespace HKSH.Common.Enums
{
    /// <summary>
    /// Active Status
    /// </summary>
    public class ActiveStatus
    {
        /// <summary>
        /// The dic
        /// </summary>
        private static readonly Dictionary<string, ActiveStatus> dic = new Dictionary<string, ActiveStatus>();

        /// <summary>
        /// The active
        /// </summary>
        public static readonly ActiveStatus Active = new ActiveStatus("Active", "Active");

        /// <summary>
        /// The inactive
        /// </summary>
        public static readonly ActiveStatus Inactive = new ActiveStatus("Inactive", "Inactive");

        /// <summary>
        /// The code
        /// </summary>
        private readonly string _code;

        /// <summary>
        /// The value
        /// </summary>
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveStatus"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="value">The value.</param>
        private ActiveStatus(string code, string value)
        {
            _code = code;
            _value = value;
            dic.Add(_value, this);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="ActiveStatus"/>.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentException">Invalid active status value.</exception>
        public static implicit operator ActiveStatus(string code)
        {
            if (dic.ContainsKey(code))
            {
                return dic[code];
            }
            throw new ArgumentException("Invalid active status value.", code);
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