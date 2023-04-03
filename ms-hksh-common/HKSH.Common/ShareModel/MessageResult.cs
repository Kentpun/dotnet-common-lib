namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// MessageResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MessageResult" />
    public class MessageResult<T> : MessageResult
    {
        /// <summary>
        /// the return data
        /// </summary>
        public new T? Data { get; set; }

        /// <summary>
        /// Successes the result.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static MessageResult<T> SuccessResult(T? data = default)
        {
            return new MessageResult<T>
            {
                Success = true,
                Data = data,
                Message = "操作成功！",
                Code = 1
            };
        }
    }

    /// <summary>
    /// MessageResult
    /// </summary>
    public class MessageResult
    {
        #region Fields

        /// <summary>
        /// success flag
        /// </summary>
        public bool? Success { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// message code model
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// the return data
        /// </summary>
        public object? Data { get; set; }

        #endregion Fields

        /// <summary>
        /// Successes the result.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static MessageResult SuccessResult(object? data = default)
        {
            return new MessageResult
            {
                Success = true,
                Data = data,
                Message = "Operate successfully！",
                Code = 1
            };
        }

        /// <summary>
        /// return failure result
        /// </summary>
        /// <returns></returns>
        public static MessageResult FailureResult(object? data = default)
        {
            return new MessageResult
            {
                Success = false,
                Message = "Internal server error, please contact the administrator！",
                Code = 0,
                Data = data
            };
        }
    }
}