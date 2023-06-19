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
        /// <param name="message">The message.</param>
        /// <param name="module">The module.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static MessageResult<T> SuccessResult(T? data = default, string? message = "", string? module = "", string? code = "")
        {
            return new MessageResult<T>
            {
                Success = true,
                Data = data,
                Message = string.IsNullOrEmpty(message) ? "Operation successful" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module,
                Code = code
            };
        }

        /// <summary>
        /// Failures the result.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        /// <param name="module">The module.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static MessageResult<T> FailureResult(T? data = default, string? message = "", string? module = "", string? code = "")
        {
            return new MessageResult<T>
            {
                Success = false,
                Data = data,
                Message = string.IsNullOrEmpty(message) ? "Operation failed" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module,
                Code = code
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
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string? Code { get; set; }

        /// <summary>
        /// Successes the result.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        /// <param name="module">The module.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static MessageResult SuccessResult(object? data = default, string? message = "", string? module = "", string? code = "")
        {
            return new MessageResult
            {
                Success = true,
                Data = data,
                Message = string.IsNullOrEmpty(message) ? "Operation successful" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module,
                Code = code
            };
        }

        /// <summary>
        /// Failures the result.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="module">The module.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static MessageResult FailureResult(string? message = "", string? module = "", string? code = "")
        {
            return new MessageResult
            {
                Success = false,
                Message = string.IsNullOrEmpty(message) ? "Operation failed" : message,
                Module = string.IsNullOrEmpty(module) ? "System" : module,
                Code = code
            };
        }
    }
}