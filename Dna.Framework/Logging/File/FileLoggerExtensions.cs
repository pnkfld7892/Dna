using Microsoft.Extensions.Logging;

namespace Dna
{
    /// <summary>
    /// Extensions for the <see cref="FileLogger"/>
    /// </summary>
    public static class FileLoggerExtensions
    {
        /// <summary>
        /// Adds a new file logger tot he specific path
        /// </summary>
        /// <param name="builder">The log builder to add to</param>
        /// <param name="path">The path where to write to</param>
        /// <returns></returns>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string path, FileLoggerConfiguration configuration = null)
        {
            //create default configuration if not procided
            if (configuration == null)
                configuration = new FileLoggerConfiguration();

            //add file provider
            builder.AddProvider(new FileLoggerProvider(path,configuration));

            return builder;
        }
    }
}
