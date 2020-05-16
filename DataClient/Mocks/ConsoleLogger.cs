using DataClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataClient.Mocks
{
    public class ConsoleLogger : ILogger
    {
        public void ClinetLog(string message)
        {
            Console.WriteLine("client:"+message);
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void ServerLog(string message)
        {
            Console.WriteLine("Server:"+message);
        }
    }
}
