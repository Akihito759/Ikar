using DataClient.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace DataClient
{
    public class SignalRClient: ICommunication
    {
        ILogger logger;
        public string HubUrl { get; set; } = "https://localhost:5001/dataHub";

        HubConnection connection;

        public SignalRClient(ILogger logger)
        {
            this.logger = logger;
        }


        public  void Connect(string url)
        {
            HubUrl = url + "dataHub";
            logger.Log("starting connection:" + url);
            connection = new HubConnectionBuilder()
                 .WithUrl(HubUrl)
                 .Build();
            mapMethods(connection);
            connection.StartAsync().Wait(); 
                Ping();
        }

        private void mapMethods(HubConnection connection)
        {
            connection.On<string>("Message", message => logger.ServerLog(message));
        }

        public async void SendData(string json)
        {
            logger.ClinetLog("Sending Data");
            await connection.SendAsync("DataRecived", json);
        }



        public async void Ping()
        {
            logger.ClinetLog("Ping");
           await connection.SendAsync("CommunicationTest", "ping");
        }
    }
}
