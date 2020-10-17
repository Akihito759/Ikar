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
            var communication = unity.container.Resolve<ICommunication>();
            communication.Connect("https://localhost:5001/");
            //communication.Connect("http://192.168.1.119:5000/");
            var reader = unity.container.Resolve<IGridEyeReader>();
            var lastData = new double[64];
            while (true)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                // the code that you want to measure comes here
                
                
                var currentState = reader.GetCurrentState();
                
                
                if (!AreArraysSame(lastData, currentState.Temperature))
                {
                    
                    communication.SendData(JsonSerializer.Serialize(currentState));
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                }
                
                lastData = (double[]) reader.GetCurrentState().Temperature.Clone();

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
