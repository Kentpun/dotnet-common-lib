namespace HKSH.Common.Enums
{
    public class PersonnelType
    {
        /// <summary>
        /// The dic
        /// </summary>
        private static readonly Dictionary<string, PersonnelType> dic = new Dictionary<string, PersonnelType>();

        /// <summary>
        /// The doctor
        /// </summary>
        public static readonly PersonnelType Doctor = new PersonnelType("Doctor", "Doctor");

        /// <summary>
        /// The nurse
        /// </summary>
        public static readonly PersonnelType Nurse = new PersonnelType("Nurse", "Nurse");

        /// <summary>
        /// The others
        /// </summary>
        public static readonly PersonnelType Others = new PersonnelType("Others", "Others");

        /// <summary>
        /// The code
        /// </summary>
        private readonly string _code;

        /// <summary>
        /// The value
        /// </summary>
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonnelType"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="value">The value.</param>
        private PersonnelType(string code, string value)
        {
            _code = code;
            _value = value;
            dic.Add(_code, this);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="PersonnelType"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentException">Invalid active status value. - value</exception>
        public static implicit operator PersonnelType(string code)
        {
            if (dic.ContainsKey(code))
            {
                return dic[code];
            }
            else
            {
                throw new ArgumentException("Invalid personnel type code.", code);
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
