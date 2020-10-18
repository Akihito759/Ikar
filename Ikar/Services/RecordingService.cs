using DataClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DataServer.Services
{

    public class RecordingStatus
    {
        public bool IsDeviceConnected { get; set; }
        public bool isRecording { get; set; }
        public TimeSpan RecordingTime { get; set; }
        public long Frames { get; set; }
        public TimeSpan TimeLeft { get; set; } = new TimeSpan(0, 0, 0);

    }

    public class RecordingService
    {
        public bool IsRecording { get; private set; }
        public bool IsDeviceConnected { get; private set; }

        public int MaxFrames { get; private set; }

        private Stopwatch stopwatch = new Stopwatch();
        private CacheService<GridEyeDataModel> cacheService;

        public RecordingService(CacheService<GridEyeDataModel> cacheService)
        {
            this.cacheService = cacheService;
        }

        public void StartRecording(int seconds = 5)
        {
            if (IsRecording)
            {
                throw new Exception("Current recording in progress...");
            }
            cacheService.Clear();
            IsRecording = true;
            MaxFrames = 10 * seconds;
            while (IsRecording) { }
        }

        public void Record(GridEyeDataModel data)
        {
            if(cacheService.Count() >= MaxFrames)
            {
                IsRecording = false;
                return;
            }
            this.cacheService.Add(data);
        }

        public void StopRecording()
        {
            IsRecording = false;
        }

        public void SetDeviceConnectionStatus(bool isDeviceConnected)
        {
            IsDeviceConnected = isDeviceConnected;
        }

        public RecordingStatus GetStatus()
        {
            return new RecordingStatus
            {
                isRecording = IsRecording,
                IsDeviceConnected = IsDeviceConnected,
                RecordingTime = stopwatch.Elapsed,
                Frames = cacheService.Get().Count(),
            };
        }


    }
}
