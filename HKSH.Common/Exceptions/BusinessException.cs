//  Mou,Xiaohua 2023/06/14 15:00

using System.Runtime.Serialization;

namespace HKSH.Common.Exceptions
{
    /// <summary>
    /// business exception
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        /// <summary>
        /// constructor
        /// </summary>
        public BusinessException()
        {
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message"></param>
        public BusinessException(string? message) : base(message)
        {
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BusinessException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}