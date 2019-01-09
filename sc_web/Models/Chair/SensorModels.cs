using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sc_web.Models.Chair
{
    public class HeartRateSensorModel
    {
        [Key]
        public DateTime Timestamp { get; set; }

        public int MeasuredBPM { get; set; }

    }

    // TODO: Add additional sensor models here, and incorporate them into SmartChairModel
}