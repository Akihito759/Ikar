using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServer.Models
{
    public class GridEyeRecordMiniModel
    {
        public bool Fall { get; set; }
        public double[] ThermistorTemperature { get; set; }
        public double[] PixelTemperatures { get; set; }
    }
}
