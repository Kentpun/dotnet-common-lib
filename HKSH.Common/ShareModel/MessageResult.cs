namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// MessageResult
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
        /// <returns></returns>
        public static MessageResult<T> SuccessResult(T? data = default, string? message = "", string? module = "")
        {
            return new MessageResult<T>
            {
                Success = true,
                Data = data,
                Message = string.IsNullOrEmpty(message) ? "Operation successful" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module
            };
        }

        /// <summary>
        /// return failure result
        /// </summary>
        /// <param name="messageModel">The message model.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static MessageResult<T> FailureResult(T? data = default, string? message = "", string? module = "")
        {
            return new MessageResult<T>
            {
                Success = false,
                Data = data,
                Message = string.IsNullOrEmpty(message) ? "Operation failed" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module
            };
        }
    }

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

        public string? Module { get; set; }

        /// <summary>
        /// Successes the result.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static MessageResult SuccessResult(object? data = default, string? message = "", string? module = "")
        {
            return new MessageResult
            {
                Success = true,
                Data = data,
                Message = string.IsNullOrEmpty(message) ? "Operation successful" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module
            };
        }

        /// <summary>
        /// Failures the result.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static MessageResult FailureResult(string? message = "", string? module = "")
        {
            return new MessageResult
            {
                Success = false,
                Message = string.IsNullOrEmpty(message) ? "Operation failed" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module
            };
        }
    }
}