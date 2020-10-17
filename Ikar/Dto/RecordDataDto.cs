using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataServer.Dto
{
    public class RecordDataDto
    {
        [BindRequired]
        public double Temperature { get; set; }
        [BindRequired]
        public bool Fall { get; set; }
        [BindRequired]
        public bool Lie { get; set; }
        [BindRequired]
        public bool FullCloth { get; set; }
        [BindRequired]
        public int RecordingSessionId { get; set; }
    }
}
