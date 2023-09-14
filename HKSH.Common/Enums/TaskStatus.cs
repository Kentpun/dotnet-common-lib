namespace HKSH.Common.Enums
{
    /// <summary>
    /// 狀態
    /// </summary>
    public class TaskStatus
    {
        /// <summary>
        /// The dic
        /// </summary>
        private static readonly Dictionary<string, TaskStatus> dic = new Dictionary<string, TaskStatus>();

        /// <summary>
        /// The new
        /// </summary>
        public static readonly TaskStatus New = new TaskStatus("New", "New");

        /// <summary>
        /// The in progress
        /// </summary>
        public static readonly TaskStatus InProgress = new TaskStatus("InProgress", "InProgress");

        /// <summary>
        /// The in progress
        /// </summary>
        public static readonly TaskStatus Completed = new TaskStatus("Completed", "Completed");

        /// <summary>
        /// The failed
        /// </summary>
        public static readonly TaskStatus Failed = new TaskStatus("Failed", "Failed");

        /// <summary>
        /// The canceled
        /// </summary>
        public static readonly TaskStatus Canceled = new TaskStatus("Canceled", "Canceled");

        /// <summary>
        /// The code
        /// </summary>
        private readonly string _code;

        /// <summary>
        /// The value
        /// </summary>
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskStatus"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="value">The value.</param>
        private TaskStatus(string code, string value)
        {
            _code = code;
            _value = value;
            dic.Add(_code, this);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="TaskStatus"/>.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentException">Invalid task status code.</exception>
        public static implicit operator TaskStatus(string code)
        {
            if (dic.ContainsKey(code))
            {
                return dic[code];
            }
            else
            {
                throw new ArgumentException("Invalid task status code.", code);
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