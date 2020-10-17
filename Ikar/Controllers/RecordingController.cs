using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServer.Dto;
using DataServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataServer.Controllers
{
    [ApiController]
    [Route("api/Recording")]
    public class RecordingController : Controller
    {
        private readonly RecordingService recordingService;
        private readonly SavingService savingService;

        public RecordingController(RecordingService recordingService, SavingService savingService)
        {
            this.savingService = savingService;
            this.recordingService = recordingService;
        }

        [HttpGet("Status")]
        public ActionResult<RecordingStatus> Status()
        {
            return recordingService.GetStatus();
        }

        [HttpPost("Start")]
        public ActionResult StartRecording()
        {
            recordingService.StartRecording();
            return Ok();
        }

        [HttpPost("Save")]
        public ActionResult SaveRecording([FromBody] RecordDataDto dto)
        {
            if (recordingService.IsRecording)
            {
                throw new Exception("Service is recording please wait and try later");
            }
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("invalid model state");
            }
            savingService.Save(false, 32);
            return Ok();
        }

        [HttpPost("Stop")]
        public ActionResult StopRecording()
        {
            return Ok();

        }

        [HttpPost("StartAndSave")]
        public ActionResult StartAndSaveRecording([FromBody] RecordDataDto dto)
        {
            recordingService.StartRecording(10);
            savingService.Save(dto.Fall, dto.Temperature, dto.RecordingSessionId);

            return Ok();
        }

    }
}