using DataClient.Interfaces;
using DataClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataClient.Mocks
{
    public class GridEyeMock : IGridEyeReader
    {
        public GridEyeDataModel GetCurrentState()
        {
            return new GridEyeDataModel
            {
                TimeStamp = DateTime.Now,
                Temperature = GenerateRandomTemperature()
            };
        }

        private double[] GenerateRandomTemperature()
        {
            var tempArray = new double[64];
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = GetRandomNumber(0, 40);
            }
            return tempArray;
        }

        private double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
