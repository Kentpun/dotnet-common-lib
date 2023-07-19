using HKSH.Common.Constants;
using Microsoft.Extensions.Logging;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// Logger Extension
    /// </summary>
    public static class LoggerExtension
    {
        /// <summary>
        /// Logs the exc.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        public static void LogExc(this ILogger logger, Exception? exception) => logger.LogError(exception, GlobalConstant.LOG_ERROR, exception?.Message);

        /// <summary>
        /// Logs the exc.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        public static void LogExc(this ILogger logger, Exception? exception, string? message) => logger.LogError(exception, GlobalConstant.LOG_ERROR, message);

        /// <summary>
        /// Logs the exc.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        public static void LogExc(this ILogger logger, string? message) => logger.LogError(GlobalConstant.LOG_ERROR, message);

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="information">The information.</param>
        public static void LogInfo(this ILogger logger, string information) => logger.LogInformation(GlobalConstant.LOG_INFO, information);
    }
}