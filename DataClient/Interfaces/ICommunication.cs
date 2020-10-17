using System;
using System.Collections.Generic;
using System.Text;

namespace DataClient.Interfaces
{
    public interface ICommunication
    {
        void SendData(string json);
        void Connect(string url);
        void Ping();
    }
}
