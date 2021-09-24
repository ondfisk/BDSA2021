using System;

namespace Lecture03
{
    public enum LogLevel { Verbose, Warning, Debug, Error };

    public delegate void Logger(string input, LogLevel logLevel = LogLevel.Debug);

    public class SubSystem
    {
        private Logger _logger;

        public SubSystem(Logger logger)
        {
            _logger = logger;
        }

        public void Operation(string input)
        {
            _logger(input, LogLevel.Verbose);
        }
    }
}
