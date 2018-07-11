using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Common.Helpers
{
    /// <summary>
    /// log helper
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// The info log path
        /// </summary>
        private static readonly string InfoPath;

        /// <summary>
        /// The error path
        /// </summary>
        private static readonly string ErrorPath;

        /// <summary>
        /// The rw lock
        /// </summary>
        private static ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Initializes the <see cref="LogHelper"/> class.
        /// </summary>
        static LogHelper()
        {
            var path = $"{Environment.CurrentDirectory}\\Logs";
            FileHelper.CreateDirectory(path);

            InfoPath = $"{path}\\InfoLogs-{DateTime.Now.ToString(FormatorHelper.LogFileTimeFormator)}.log";
            ErrorPath = $"{path}\\ErrorLogs-{DateTime.Now.ToString(FormatorHelper.LogFileTimeFormator)}.log";
        }

        //private static List<string> messages = new List<string>();

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Info(string message)
        {
            message = FormatorHelper.InfoLog($"Info: {Thread.CurrentThread.ManagedThreadId.ToString()} {message}");
            WriteToFile(InfoPath, message);

            Console.WriteLine(message);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Debug(string message)
        {
            message = FormatorHelper.InfoLog($"Debug: {message}");
            WriteToFile(InfoPath, message);

            System.Diagnostics.Debug.WriteLine(message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void Error(string message, Exception exception)
        {
            message = FormatorHelper.ExceptionLog($"Error: {message}", exception);
            WriteToFile(ErrorPath, message);

            Console.WriteLine(message);
        }

        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="message">The message.</param>
        private static void WriteToFile(string path, string message)
        {
            _rwLock.EnterWriteLock();
            File.AppendAllText(path, $"{message}{Environment.NewLine}", Encoding.UTF8);
            _rwLock.ExitWriteLock();
        }
    }
}
