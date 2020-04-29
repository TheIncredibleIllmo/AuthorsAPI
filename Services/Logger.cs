using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAPI.Services
{
    public interface ILogger
    {
        void LogMessage(string message);
    }

    public class Logger : ILogger
    {
        public Logger()
        {
            LogMessage("Logger was started...");
        }

        public void LogMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
        }
    }
}
