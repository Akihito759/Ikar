using DataClient.Models;
using DataServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataServer.Services
{
    public class SavingService
    {
        private CacheService<GridEyeDataModel> cacheService;
        private IHostEnvironment environment;
        private string rootPath;
        public SavingService(CacheService<GridEyeDataModel> cacheService,  IHostEnvironment environment)
        {
            this.cacheService = cacheService;
            this.environment = environment;
            this.rootPath = Path.Combine(environment.ContentRootPath, "DataBase");
        }

        public GridEyeRecordModel GetRawRecord()
        {
            return new GridEyeRecordModel(cacheService.Get());
        }

        public void Save(bool fall, double temperature, int sessionId = 1, string description = null)
        {
            if(cacheService.Get().Count > 0)
            {
                var record = new GridEyeRecordModel(cacheService.Get())
                {
                    Fall = fall,
                    RoomTemperature = temperature,
                    RecordingSessionId = sessionId,
                    Description = description,
                };

                var tempCollection = getCollection(sessionId);
                tempCollection.Add(record);
                saveCollection(tempCollection, sessionId);
                cacheService.Clear();
            }

            else
            {
                throw new Exception("No data to save");
            }
        }

        private ICollection<GridEyeRecordModel> getCollection(int sessionId)
        {
            var path = getPath(sessionId);
            if (!File.Exists(path))
            {
                File.CreateText(path).Close();
                return new List<GridEyeRecordModel>();
            }

            return JsonConvert.DeserializeObject<List<GridEyeRecordModel>>(File.ReadAllText(path));
        }

        private void saveCollection(ICollection<GridEyeRecordModel> data, int sessionId)
        {
            var path = getPath(sessionId);
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
            var miniPath = getMiniPath(sessionId);
            ICollection<GridEyeRecordMiniModel> xyz = data.Select(x => x.ConvertToMiniModel()).ToList();
            File.WriteAllText(miniPath, JsonConvert.SerializeObject(xyz));
        }

        private string getPath(int sessionId) => Path.Combine(rootPath, +sessionId + ".json");
        private string getMiniPath(int sessionId) => Path.Combine(rootPath, +sessionId + ".min.json");

    }
}
