using System.Runtime.Serialization;

namespace HKSH.Common.Exceptions
{
    /// <summary>
    /// UnAuthorizedException
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UnAuthorizedException : Exception
    {
        /// <summary>
        /// constructor
        /// </summary>
        public UnAuthorizedException()
        {
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message"></param>
        public UnAuthorizedException(string? message) : base(message)
        {
        }
    }
}