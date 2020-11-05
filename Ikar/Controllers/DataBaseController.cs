using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataServer.Controllers
{
    [ApiController]
    [Route("api/DataBase")]
    public class DataBaseController : Controller
    {
        DataBaseService dataBaseService;
        FilterService filterService;
        
        public DataBaseController(DataBaseService dataBaseService, FilterService filterService)
        {
            this.dataBaseService = dataBaseService;
            this.filterService = filterService;
        }

        [HttpGet("DataBaseController")]
        public IActionResult GenerateMasterFile()
        {
            dataBaseService.MergeJsonToMainFile();
            return Ok();
        }
        [HttpPost("ConvertJsonToCsv")]
        public IActionResult ConvertJsonToCsv(string name)
        {
            dataBaseService.ConvertJsonToCsv(name);
            return Ok();
        }

        [HttpPost("ApplyFilterOnData")]
        public IActionResult ApplyFilterOnData(string name,double degree)
        {
            var data = dataBaseService.GetGridEyeRecordMiniModels(name);
            filterService.Binarisation(data, degree);
            dataBaseService.SaveGridEyeRecordMiniModels(data, $"!trans-{name}");
            return Ok();
        }
    }
}