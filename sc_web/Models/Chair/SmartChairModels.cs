using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sc_web.Models.Chair
{
    public class SmartChairModel
    {
        [Key]
        public string AuthKey { get; set; }
        public string Name { get; set; }

        public List<HeartRateSensorModel> SensorData { get; set; }
        // TODO - add more data collections 
    }
}