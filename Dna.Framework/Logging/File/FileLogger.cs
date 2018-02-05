using System;
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Dna
{
    /// <summary>
    /// A logger that writes the logs to file
    /// </summary>
    public class FileLogger : ILogger
    {
        #region static properties
        protected static ConcurrentDictionary<string,object> FileLocks = new ConcurrentDictionary<string, object>();
        #endregion

        #region protected members

        /// <summary>
        /// The category for this logger
        /// </summary>
        protected string mCategoryName;

        /// <summary>
        /// the path to log to
        /// </summary>
        protected string mFilePath;

        /// <summary>
        /// the configuration to use
        /// </summary>
        protected FileLoggerConfiguration mConfiguration;

        #endregion

        #region ctor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="categoryName">The category for this logger</param>
        /// <param name="filePath">the file path to write to</param>
        /// <param name="configuration">the configuration to use</param>
        public FileLogger(string categoryName,string filePath, FileLoggerConfiguration configuration)
        {
            filePath = Path.GetFullPath(filePath);

            mCategoryName = categoryName;
            mFilePath = filePath;
            mConfiguration = configuration;
        }
        #endregion

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// Enabled if the log level is the same or greater than the configuration
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            //enable if the log level is greater than or equal to what we want to log
            return logLevel >= mConfiguration.LogLevel;
        }

        /// <summary>
        /// Logs the message to file
        /// </summary>
        /// <typeparam name="TState">the type details of the message</typeparam>
        /// <param name="logLevel">log level</param>
        /// <param name="eventId"><teh id/param>
        /// <param name="state">Details of the message</param>
        /// <param name="exception">Any exception to log</param>
        /// <param name="formatter">the formatter for converting the state and exception to a message string</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;
            // Get current time
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");

            //prepend timeLogString if desired
            var timeLogString = mConfiguration.LogTime ? $"[{currentTime}] " : "";

            var message = formatter(state, exception);
            //write the message to the log file
            var output = $"{timeLogString}{message} {Environment.NewLine}";

            //normalize
            //TODO: make use of configuration base path

            //TODO: Lock
            var fileLock = FileLocks.GetOrAdd(mFilePath.ToUpper(), path => new object());

            lock (fileLock)
            {
                //write message to file
                File.AppendAllText(mFilePath, output);

            }
        }
    }
}
