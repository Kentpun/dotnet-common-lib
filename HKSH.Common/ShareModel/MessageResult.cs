namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// MessageResult
    /// </summary>
    /// <seealso cref="MessageResult" />
    public class MessageResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MessageResult"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public object? Data { get; set; }

        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <value>
        /// The module.
        /// </value>
        public string? Module { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string? Code { get; set; }

        /// <summary>
        /// Successes the result.
        /// </summary>
        /// <returns></returns>
        public static MessageResult SuccessResult()
        {
            return new MessageResult
            {
                Success = true,
                Message = "Operation successful",
                Module = "System",
                Code = SystemMessage.Success.Code
            };
        }

        /// <summary>
        /// Successes the result.
        /// </summary>
        /// <param name="messageModel">The message model.</param>
        /// <param name="module">The module.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static MessageResult SuccessResult(MessageModel messageModel, string? module = "", string message = "")
        {
            return new MessageResult
            {
                Success = true,
                Message = string.IsNullOrEmpty(message) ? "Operation successful" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module,
                Code = messageModel?.Code ?? SystemMessage.Success.Code
            };
        }

        /// <summary>
        /// Successes the result.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="messageModel">The message model.</param>
        /// <param name="module">The module.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static MessageResult SuccessResult(object? data, MessageModel? messageModel = null, string? module = "", string message = "")
        {
            return new MessageResult
            {
                Data = data,
                Success = true,
                Message = string.IsNullOrEmpty(message) ? "Operation successful" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module,
                Code = messageModel?.Code ?? SystemMessage.Success.Code
            };
        }

        /// <summary>
        /// Failures the result.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <returns></returns>
        public static MessageResult FailureResult(string? module = "")
        {
            return new MessageResult
            {
                Success = false,
                Code = SystemMessage.Failure.Code,
                Message = "Operation failed",
                Module = string.IsNullOrEmpty(module) ? "System" : module
            };
        }

        /// <summary>
        /// Failures the result.
        /// </summary>
        /// <param name="messageModel">The message model.</param>
        /// <param name="module">The module.</param>
        /// <param name="message">The message.</param>
        /// <param name="module">The module.</param>
        /// <returns></returns>
        public static MessageResult FailureResult(string message, string? module = "")
        {
            return new MessageResult
            {
                Success = false,
                Code = SystemMessage.Failure.Code,
                Message = message,
                Module = string.IsNullOrEmpty(module) ? "System" : module
            };
        }
        /// <summary>
        /// Failures the result.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="module">The module.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static MessageResult FailureResult(MessageModel messageModel, string? module = "", string message = "")
        {
            return new MessageResult
            {
                Success = false,
                Message = string.IsNullOrEmpty(message) ? "Operation failed" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module,
                Code = messageModel.Code
            };
        }

        /// <summary>
        /// Failures the result.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="messageModel">The message model.</param>
        /// <param name="module">The module.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static MessageResult FailureResult(object? data, MessageModel? messageModel = null, string? module = "", string message = "")
        {
            return new MessageResult
            {
                Data = data,
                Success = false,
                Message = string.IsNullOrEmpty(message) ? "Operation failed" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module,
                Code = messageModel?.Code ?? SystemMessage.Success.Code
            };
        }
    }

    /// <summary>
    /// MessageResult<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageResult<T> : MessageResult
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public new T? Data { get; set; }

        /// <summary>
        /// Successes the result.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="messageModel">The message model.</param>
        /// <param name="message">The message.</param>
        /// <param name="module">The module.</param>
        /// <returns></returns>
        public static MessageResult<T> SuccessResult(T? data = default, MessageModel? messageModel = null, string? message = "", string? module = "")
        {
            return new MessageResult<T>
            {
                Success = true,
                Data = data,
                Message = string.IsNullOrEmpty(message) ? "Operation successful" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module,
                Code = messageModel?.Code ?? SystemMessage.Success.Code
            };
        }

        /// <summary>
        /// Failures the result.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="messageModel">The message model.</param>
        /// <param name="module">The module.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static MessageResult<T> FailureResult(T? data = default, MessageModel? messageModel = null, string? module = "", string? message = "")
        {
            return new MessageResult<T>
            {
                Success = false,
                Data = data ?? default,
                Message = string.IsNullOrEmpty(message) ? "Operation failed" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module,
                Code = messageModel?.Code ?? SystemMessage.Failure.Code,
            };
        }
    }
}