using DataClient.Models;
using DataServer.Services;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataSever.Hubs
{
    public class DataHub: Hub
    {
        private RecordingService recordingService;

        public DataHub(RecordingService recordingService)
        {
            this.recordingService = recordingService;
        }

        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task CommunicationTest(string message)
        {
            bool isConnected = message.ToLower() == "ping";
            recordingService.SetDeviceConnectionStatus(isConnected);
            return Clients.Caller.SendAsync("Message", isConnected ? "pong" : "Don't get ping message");
        }

        public Task DataRecived(string json)
        {
            if (recordingService.IsRecording)
            {
                recordingService.Record(JsonConvert.DeserializeObject<GridEyeDataModel>(json));
            }
            Clients.Others.SendAsync("Message", json);
            return Clients.Caller.SendAsync("Message", "Recived Data");
        }
    }
}
