using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServer.Helpers
{
    public class HighPass
    {
        public double[,] MeanRemoval = new double[3, 3]
        {
            {1, 1,1 },
            {1,-9,1 },
            {1, 1,1 }
        };
    }

    public class LowPass
    {

    }

    public static class FilterMasks
    {
        public static HighPass HighPass = new HighPass();
        public static LowPass LowPass = new LowPass();
    }
}
