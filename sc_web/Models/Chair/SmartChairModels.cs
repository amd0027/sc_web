using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sc_web.Models.Chair
{
    public class SmartChairModel
    {
        [Key]
        public string AuthKey { get; set; }
        public string Name { get; set; }
        public string WebKey { get; set; }

        public List<HeartRateSensorModel> HeartSensorData { get; set; }
        public List<PostureSensorModel> PostureSensorData { get; set; }
        public List<MotionEventModel> MotionSensorData { get; set; }
        public List<OccupancySessionModel> OccupancySessionData { get; set; }
    }
}