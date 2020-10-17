using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServer.Models
{
    public static class GridEyeExtension
    {
        public static GridEyeRecordMiniModel ConvertToMiniModel(this GridEyeRecordModel model)
        {
            return new GridEyeRecordMiniModel
            {
                Fall = model.Fall,
                PixelTemperatures = model.PixelTemperatures,
                ThermistorTemperature = model.ThermistorTemperature
            };
        }
    }
}
