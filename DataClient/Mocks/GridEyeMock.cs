using DataClient.Interfaces;
using DataClient.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DataClient.Mocks
{
    public class GridEyeMock : IGridEyeReader
    {
        public GridEyeDataModel GetCurrentState()
        {
            return new GridEyeDataModel
            {
                TimeStamp = DateTime.Now,
                Temperature = GenerateRandomTemperature(),
                ThermistorTemperature = GetRandomNumber(20, 34)
            };
        }

        private double[] GenerateRandomTemperature()
        {
            
            var tempArray = new double[64];
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = GetRandomNumber(20, 34);
            }
            Thread.Sleep(49);
            return tempArray;
        }

        private double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
