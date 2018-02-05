using System;
using System.IO;

namespace Dna
{
    /// <summary>
    /// Formats a message when the callers source information is provided
    /// </summary>
    public static class LoggerSourceFormatter
    {
        /// <summary>
        /// Formats the message 
        /// </summary>
        /// <param name="state">The state information about the log</param>
        /// <param name="exception">The exception</param>
        /// <returns></returns>
        public static string Format(object [] state, Exception exception)
        {
            //get values from state
            var origin = (string)state[0];
            var filePath = (string)state[1];
            var lineNumber = (int)state[2];
            var message = (string)state[3];

            //get any exception message
            var exceptionMessage = exception?.ToString();

            //newline if we have an exception
            if (exception != null)
                exceptionMessage += System.Environment.NewLine;
            //format the message string
            return  $"{message} [{Path.GetFileName(filePath)} > {origin}() > Line {lineNumber}]";

            return "test";
        }
    }
}
