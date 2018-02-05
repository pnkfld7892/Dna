using Microsoft.Extensions.Logging;

namespace Dna
{
    /// <summary>
    /// The configuration for a <see cref="FileLogger"/>
    /// </summary>
    public class FileLoggerConfiguration
    {
        #region public properties
        /// <summary>
        /// The level of log that should be processed
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Trace;


        /// <summary>
        /// Do we want to log the time?
        /// </summary>
        public bool LogTime { get; set; } = true;


        #endregion
    }
}
