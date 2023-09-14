namespace HKSH.Common.Enums
{
    /// <summary>
    /// Task Type
    /// </summary>
    public class TaskType
    {
        /// <summary>
        /// The dic
        /// </summary>
        private static readonly Dictionary<string, TaskType> dic = new Dictionary<string, TaskType>();

        /// <summary>
        /// The device
        /// </summary>
        public static readonly TaskType Device = new TaskType("Device", "Device");

        /// <summary>
        /// The location
        /// </summary>
        public static readonly TaskType Location = new TaskType("Location", "Location");

        /// <summary>
        /// The role
        /// </summary>
        public static readonly TaskType Role = new TaskType("Role", "Role");

        /// <summary>
        /// The module
        /// </summary>
        public static readonly TaskType Module = new TaskType("Module", "Module");

        /// <summary>
        /// The menu
        /// </summary>
        public static readonly TaskType Menu = new TaskType("Menu", "Menu");

        /// <summary>
        /// The dictionary setting
        /// </summary>
        public static readonly TaskType DictionarySetting = new TaskType("DictionarySetting", "DictionarySetting");

        /// <summary>
        /// The dictionary data set
        /// </summary>
        public static readonly TaskType DictionaryDataSet = new TaskType("DictionaryDataSet", "DictionaryDataSet");

        /// <summary>
        /// The dictionary
        /// </summary>
        public static readonly TaskType Dictionary = new TaskType("Dictionary", "Dictionary");

        /// <summary>
        /// The dictionary excel templte
        /// </summary>
        public static readonly TaskType DictionaryExcelTemplte = new TaskType("DictionaryExcelTemplte", "DictionaryExcelTemplte");

        /// <summary>
        /// The publish log
        /// </summary>
        public static readonly TaskType PublishLog = new TaskType("PublishLog", "PublishLog");

        /// <summary>
        /// The alert
        /// </summary>
        public static readonly TaskType Alert = new TaskType("Alert", "Alert");

        /// <summary>
        /// The button
        /// </summary>
        public static readonly TaskType Button = new TaskType("Button", "Button");

        /// <summary>
        /// The notification templte
        /// </summary>
        public static readonly TaskType NotificationTemplte = new TaskType("NotificationTemplte", "NotificationTemplte");

        /// <summary>
        /// The action log
        /// </summary>
        public static readonly TaskType ActionLog = new TaskType("ActionLog", "ActionLog");

        /// <summary>
        /// The activity log
        /// </summary>
        public static readonly TaskType ActivityLog = new TaskType("DevActivityLogice", "DevActivityLogice");

        /// <summary>
        /// The code
        /// </summary>
        private readonly string _code;

        /// <summary>
        /// The value
        /// </summary>
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskType"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="value">The value.</param>
        private TaskType(string code, string value)
        {
            _code = code;
            _value = value;
            dic.Add(_code, this);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="TaskType"/>.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentException">Invalid task status code.</exception>
        public static implicit operator TaskType(string code)
        {
            if (dic.ContainsKey(code))
            {
                return dic[code];
            }
            else
            {
                throw new ArgumentException("Invalid task type code.", code);
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