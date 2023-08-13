using HKSH.Common.Constants;

namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// MessageModel
    /// </summary>
    public abstract class MessageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageModel"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="ModuleCode">The module code.</param>
        public MessageModel(string code, string ModuleCode = GlobalConstant.SYSTEM_MESSAGE_PREFIX)
        {
            Code = $"B{ModuleCode}{code}";
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }
    }
}