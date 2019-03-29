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

    public class PostureSensorModel
    {
        [Key]
        public DateTime Timestamp { get; set; }

        public int PostureData { get; set; }
    }

    public class OccupancySessionModel
    {
        [Key]
        public DateTime Timestamp { get; set; }

        public DateTime SitDownTime { get; set; }

        // A 32-bit integer will allow for 42 days of sitting - more than enough
        public int ElapsedTimeMs { get; set; }
    }

    public class MotionEventModel
    {
        [Key]
        public DateTime Timestamp { get; set; }

        [Flags]
        public enum MotionAxis : short
        {
            X = 1,
            Y = 2,
            Z = 4,
            RotateSideSide  = 8,
            RotateFrontBack = 16
        }

        public short Axis { get; set; }
        public short Level { get; set; }
    }

    public class AirQualityModel
    {
        [Key]
        public DateTime Timestamp { get; set; }

        public int CO2 { get; set; }
        public int VOC { get; set; }
    }
}