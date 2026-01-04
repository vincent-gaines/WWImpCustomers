using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWImpCustomers.Infrastructure
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message) => Console.WriteLine($"INFO: {message}");
        public void Error(string message, Exception ex) =>
            Console.WriteLine($"ERROR: {message} - {ex.Message}");
    }
}
