using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Dna
{
    /// <summary>
    /// Provides the ability to log to file
    /// </summary>
    public class FileLoggerProvider : ILoggerProvider
    {
        #region Protected Members
        /// <summary>
        /// The file path for this log
        /// </summary>
        protected string mFilePath;

        /// <summary>
        /// the Config to use when creating loggers
        /// </summary>
        protected readonly FileLoggerConfiguration mConfiguration;

        /// <summary>
        /// Keeps track of the loggers already created
        /// </summary>
        protected readonly ConcurrentDictionary<string, FileLogger> mLoggers = new ConcurrentDictionary<string, FileLogger>();
        #endregion

        #region Ctor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="path">Path to log to</param>
        public FileLoggerProvider(string path,FileLoggerConfiguration configuration)
        {
            mConfiguration = configuration;
            mFilePath = path;
        }
        #endregion

        #region Interface Implementation
        public ILogger CreateLogger(string categoryName)
        {
            return mLoggers.GetOrAdd(categoryName, name => new FileLogger(name, mFilePath, mConfiguration));
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            //clear the list of loggers
            mLoggers.Clear();
        }
        #endregion
    }
}
