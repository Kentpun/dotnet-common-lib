namespace HKSH.Common.Enums
{
    /// <summary>
    /// Language
    /// </summary>
    public class Language
    {
        /// <summary>
        /// The dic
        /// </summary>
        private static readonly Dictionary<string, Language> dic = new Dictionary<string, Language>();

        /// <summary>
        /// The english
        /// </summary>
        public static readonly Language English = new Language("en", "English");

        /// <summary>
        /// The chinese
        /// </summary>
        public static readonly Language Chinese = new Language("zh", "Chinese");

        /// <summary>
        /// The code
        /// </summary>
        private readonly string _code;

        /// <summary>
        /// The value
        /// </summary>
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="value">The value.</param>
        private Language(string code, string value)
        {
            _code = code;
            _value = value;
            dic.Add(_code, this);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="Language"/>.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentException">Invalid language code.</exception>
        public static implicit operator Language(string code)
        {
            if (dic.ContainsKey(code))
            {
                return dic[code];
            }
            else
            {
                throw new ArgumentException("Invalid language code.", code);
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