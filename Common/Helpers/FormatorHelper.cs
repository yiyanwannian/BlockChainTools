using System;

namespace Common.Helpers
{
    public static class FormatorHelper
    {
        /// <summary>
        /// The time formator
        /// </summary>
        public const string TimeFormator = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// The log file time formator
        /// </summary>
        public const string LogFileTimeFormator = "yyyy-MM-dd-HH-mm-ss";

        /// <summary>
        /// Logs the formator.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>System.String.</returns>
        public static string InfoLog(string message)
        {
            return $"[{DateTime.Now.ToString(TimeFormator)}] { message }{ (message.EndsWith(".") ? string.Empty : ".") }";
        }

        /// <summary>
        /// Exceptions the log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>System.String.</returns>
        public static string ExceptionLog(string message, Exception exception)
        {
            return $"{ message } { Environment.NewLine }Error: {exception.Message}; { Environment.NewLine }{exception.StackTrace}";
        }
    }
}
