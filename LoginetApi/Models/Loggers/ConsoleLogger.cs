using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginetApi.Models.Interfaces;

namespace LoginetApi.Models.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("{0}: {1}", DateTime.Now, message);
        }
    }
}