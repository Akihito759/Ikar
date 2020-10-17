using DataClient.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataServer.Models
{
    public class GridEyeRecordModel : GridEyeRecordMiniModel
    {
        public bool Lie { get; set; } = false;
        public bool FullCloth { get; set; }
        public int RecordingSessionId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public double RoomTemperature { get; set; }
        public long FramesCount { get; set; }

        public GridEyeRecordModel(List<GridEyeDataModel> rawData)
        {
            ThermistorTemperature = rawData.Select(x => x.ThermistorTemperature).ToArray();
            PixelTemperatures = rawData.SelectMany(x => x.Temperature).ToArray();
            FramesCount = ThermistorTemperature.Length;
        }

        

        [JsonConstructor]
        public GridEyeRecordModel()
        {

        }
    }
}
