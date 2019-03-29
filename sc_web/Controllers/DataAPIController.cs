using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using sc_web.Models;
using System.Linq;
using System.Web.Http;

namespace sc_web.Controllers
{
    [RoutePrefix("api/Data")]
    public class DataAPIController : ApiController
    {
        /// <summary>
        /// Application DB context
        /// </summary>
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public DataAPIController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        private System.DateTime CorrectTimeStamps(System.DateTime timestamp)
        {
            if (timestamp == System.DateTime.MinValue)
            {
                return System.DateTime.Now;
            }
            else
            {
                return timestamp;
            }
        }

        [Route("PostHeartRateData")]
        public IHttpActionResult PostHeartRateData(Models.Chair.HeartRateSensorModel data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            if (!Request.Headers.TryGetValues("X-AuthKey", out var authKeyList))
            {
                return BadRequest("No AuthKey");
            }

            var authKey = authKeyList.FirstOrDefault();

            var chair = ApplicationDbContext.Users.SelectMany(u => u.PairedChairs)
                .Where(c => c.AuthKey == authKey)
                .FirstOrDefault();
                
            if (chair == null)
            {
                return BadRequest("Bad AuthKey");
            }

            data.Timestamp = CorrectTimeStamps(data.Timestamp);

            chair.HeartSensorData.Add(data);
            ApplicationDbContext.SaveChanges();

            return Ok();
        }

        [Route("PostPostureData")]
        public IHttpActionResult PostPostureData(Models.Chair.PostureSensorModel data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            if (!Request.Headers.TryGetValues("X-AuthKey", out var authKeyList))
            {
                return BadRequest("No AuthKey");
            }

            var authKey = authKeyList.FirstOrDefault();

            var chair = ApplicationDbContext.Users.SelectMany(u => u.PairedChairs)
                .Where(c => c.AuthKey == authKey)
                .FirstOrDefault();

            if (chair == null)
            {
                return BadRequest("Bad AuthKey");
            }

            data.Timestamp = CorrectTimeStamps(data.Timestamp);

            chair.PostureSensorData.Add(data);
            ApplicationDbContext.SaveChanges();

            return Ok();
        }

        [Route("PostMotionData")]
        public IHttpActionResult PostMotionData(Models.Chair.MotionEventModel data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            if (!Request.Headers.TryGetValues("X-AuthKey", out var authKeyList))
            {
                return BadRequest("No AuthKey");
            }

            var authKey = authKeyList.FirstOrDefault();

            var chair = ApplicationDbContext.Users.SelectMany(u => u.PairedChairs)
                .Where(c => c.AuthKey == authKey)
                .FirstOrDefault();

            if (chair == null)
            {
                return BadRequest("Bad AuthKey");
            }

            data.Timestamp = CorrectTimeStamps(data.Timestamp);

            chair.MotionSensorData.Add(data);
            ApplicationDbContext.SaveChanges();

            return Ok();
        }

        [Route("PostOccupancyData")]
        public IHttpActionResult PostOccupancyData(Models.Chair.OccupancySessionModel data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            if (!Request.Headers.TryGetValues("X-AuthKey", out var authKeyList))
            {
                return BadRequest("No AuthKey");
            }

            var authKey = authKeyList.FirstOrDefault();

            var chair = ApplicationDbContext.Users.SelectMany(u => u.PairedChairs)
                .Where(c => c.AuthKey == authKey)
                .FirstOrDefault();

            if (chair == null)
            {
                return BadRequest("Bad AuthKey");
            }

            data.Timestamp = CorrectTimeStamps(data.Timestamp);

            chair.OccupancySessionData.Add(data);
            ApplicationDbContext.SaveChanges();

            return Ok();
        }

        [Route("PostAirQualityData")]
        public IHttpActionResult PostAirQualityData(Models.Chair.AirQualityModel data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            if (!Request.Headers.TryGetValues("X-AuthKey", out var authKeyList))
            {
                return BadRequest("No AuthKey");
            }

            var authKey = authKeyList.FirstOrDefault();

            var chair = ApplicationDbContext.Users.SelectMany(u => u.PairedChairs)
                .Where(c => c.AuthKey == authKey)
                .FirstOrDefault();

            if (chair == null)
            {
                return BadRequest("Bad AuthKey");
            }

            data.Timestamp = CorrectTimeStamps(data.Timestamp);

            chair.AirQualitySensorData.Add(data);
            ApplicationDbContext.SaveChanges();

            return Ok();
        }
    }
}
