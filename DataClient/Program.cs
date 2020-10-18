using Unity;
using DataClient.Interfaces;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;

namespace DataClient
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var unity = IoCContainer.CrateContainer();
            var communication = unity.container.Resolve<ICommunication>();
            var logger = unity.container.Resolve<ILogger>();
            IGridEyeReader reader;
            try
            {
                communication.Connect("https://localhost:5001/");
                reader = unity.container.Resolve<IGridEyeReader>("Mock");
            }
            catch
            {
                communication.Connect("http://192.168.1.119:5000/");
                reader = unity.container.Resolve<IGridEyeReader>();
            }

            var lastData = new double[64];
            var watch = System.Diagnostics.Stopwatch.StartNew();
            while (true)
            {
                var currentState = reader.GetCurrentState();
                
                
                if (!AreArraysSame(lastData, currentState.Temperature) && currentState.Temperature.Sum() != 0)
                {
                    
                    communication.SendData(JsonSerializer.Serialize(currentState));
                    watch.Stop();
                    logger.Log("time:"+watch.ElapsedMilliseconds.ToString()+"ms");
                    watch.Restart();
                }
                
                lastData = (double[]) currentState.Temperature.Clone();

            }
        }

        private static bool AreArraysSame(double[] arr1, double[] arr2)
        {
            for(int i = 0; i < arr1.Length; ++i)
            {
                if(arr1[i] != arr2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
