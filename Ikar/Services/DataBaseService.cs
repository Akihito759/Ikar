using DataServer.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataServer.Services
{
    public class DataBaseService
    {
        private string rootPath;

        public DataBaseService(IHostEnvironment environment)
        {
            this.rootPath = Path.Combine(environment.ContentRootPath, "DataBase");
        }

        public void MergeJsonToMainFile()
        {
            var measurements = new List<GridEyeRecordMiniModel>();
            var filesPaths = Directory.GetFiles(rootPath, "*.min.json");
            foreach(var filePath in filesPaths)
            {
                if (filePath.Contains("Master.min.json"))
                {
                    continue;
                }
               var localMeasurments =
                    JsonConvert.DeserializeObject<List<GridEyeRecordMiniModel>>
                    (File.ReadAllText(filePath));
                measurements.AddRange(localMeasurments);
            }
            File.WriteAllText(getMainFilePath(), JsonConvert.SerializeObject(measurements));
        }

        public void ConvertJsonToCsv(string fileName)
        {
            var data = GetGridEyeRecordMiniModels(fileName);

            using(var stream = new StreamWriter(getCSVPath(fileName)))
            foreach(var measurment in data)
            {
                    stream.Write(measurment.Fall + ","+ getArrayString(measurment.PixelTemperatures));
                    stream.Write(Environment.NewLine);
            }
        }

        public ICollection<GridEyeRecordMiniModel> GetGridEyeRecordMiniModels(string fileName)
            => JsonConvert.DeserializeObject
                <List<GridEyeRecordMiniModel>>(File.ReadAllText(getFilePath(fileName)));
        public void SaveGridEyeRecordMiniModels(ICollection<GridEyeRecordMiniModel> data, string fileName)
            => File.WriteAllText(getFilePath(fileName), JsonConvert.SerializeObject(data));

        private string getMainFilePath() => Path.Combine(rootPath, "Master.min.json");
        private string getFilePath(string name) => Path.Combine(rootPath, $"{name}.min.json");
        private string getCSVPath(string name) => Path.Combine(rootPath, $"{name}.csv");
        private string getArrayString(double[] array) => string.Join(",", array.Select(x => x.ToString(new CultureInfo("en-US", false))).ToArray());
    }
}
