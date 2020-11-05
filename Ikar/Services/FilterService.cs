using DataClient.Models;
using DataServer.Helpers;
using DataServer.Models;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace DataServer.Services
{
    public class FilterService
    {

        public void Binarisation(ICollection<GridEyeRecordMiniModel> data, double threshold)
        {
            foreach (var frame in data)
            {
                frame.PixelTemperatures = frame.PixelTemperatures.ApplyBinarisation(threshold);
            }
        }

        public void MedianFilter(ICollection<GridEyeRecordMiniModel> data, bool doFrame = false)
        {
            foreach (var frame in data)
            {
                frame.PixelTemperatures = frame.PixelTemperatures.ApplyMedianFilter(doFrame);
            }
        }

        public void ApplyFilter(ICollection<GridEyeRecordMiniModel> data, double[,] filter, bool doFrame = false)
        {
            foreach (var frame in data)
            {
                frame.PixelTemperatures = frame.PixelTemperatures.ApplyFilterOnData(filter,doFrame);
            }
        }        
    }
}
