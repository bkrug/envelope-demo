using System;

namespace MusicXmlParser
{
    public interface ILogger
    {
        void WriteError(string message);
    }

    public class Logger : ILogger
    {
        public void WriteError(string message)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ForegroundColor = oldColor;
        }
    }
}
