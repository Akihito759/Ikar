using DataSever.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataSever.Hubs
{
    public class DataHub: Hub
    {
        private StupidCasheService cache;

        public DataHub(StupidCasheService service)
        {
            cache = service;
        }

        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task CommunicationTest(string message)
        {
            return Clients.Caller.SendAsync("Message", message.ToLower() == "ping" ? "pong" : "Don't get ping message");
        }

        public void DataRecived(string json)
        {
            cache.dataContainer.Add(json);
        }
    }
}
