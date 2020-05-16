using Unity;
using DataClient.Interfaces;
using System;
using System.Threading.Tasks;
using System.Text.Json;

namespace DataClient
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var unity = IoCContainer.CrateContainer();
            var comm = unity.container.Resolve<ICommunication>();
            comm.Connect();
            var reader = unity.container.Resolve<IGridEyeReader>();
            while (true)
            {
                comm.SendData(JsonSerializer.Serialize(reader.GetCurrentState()));
                

            }

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
