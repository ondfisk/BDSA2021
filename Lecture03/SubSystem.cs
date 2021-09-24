using System;

namespace Lecture03
{
    public enum LogLevel { Verbose, Warning, Debug, Error };

    public class SubSystem
    {
        private Action<string, LogLevel> _logger;

        public SubSystem(Action<string, LogLevel> logger)
        {
            _logger = logger;

        }

        public void Operation(string input)
        {
            _logger(input, LogLevel.Verbose);
        }
    }
}
