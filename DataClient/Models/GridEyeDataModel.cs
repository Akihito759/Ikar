using System;
using System.Collections.Generic;
using System.Text;

namespace DataClient.Models
{
    public class GridEyeDataModel
    {
        public DateTime TimeStamp { get; set; }
        public double[] Temperature { get; set; } = new double[60];
    }
}
