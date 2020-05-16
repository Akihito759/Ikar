using System;
using System.Collections.Generic;
using System.Text;

namespace DataClient.Interfaces
{
    public interface ILogger
    {
        void Log(string message);
        void ClinetLog(string message);
        void ServerLog(string message);
    }
}
