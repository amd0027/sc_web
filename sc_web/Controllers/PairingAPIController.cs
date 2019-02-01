using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using sc_web.DataLayer;
using sc_web.Models;

namespace sc_web.Controllers
{
    [RoutePrefix("api/PairingOperations")]
    public class PairingAPIController : ApiController
    {
        private PairingOperationsContext db = new PairingOperationsContext();

        private static readonly char[] ALLOWED_CHARS = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private const int PAIRING_CODE_LENGTH = 6;

        // GET api/<controller>
        [Route("GetLatestVersion")]
        public Models.UpdateCheck GetLatestVersion()
        {
            UpdateCheck updateCheck;

            // TODO: Pull the latest version from the configuration data somewhere
            updateCheck = new Models.UpdateCheck()
            {
                LatestMajorVersion = 0,
                LatestMinorVersion = 3,
                FirmwareURL = "https://uahsmartchair.com/FirmwareDistrib/v03.bin"
            };

            return updateCheck;
        }

        // GET: api/PairingOperations/PairingCode/x
        [Route("GetPairingCode/{uuid}")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetPairingCode(string uuid)
        {
            PairingOperation existing = PairingOperationForUUID(uuid);
            if (existing != null)
            {
                // return the previously generated pairing key
                return Ok(existing.ID);
            }

            // generate a new pairing key
            PairingOperation pairingOperation = new PairingOperation();
            pairingOperation.DeviceUUID = uuid;

            pairingOperation.ID = GetRandomPairingCode();
            while (PairingOperationExists(pairingOperation.ID))
            {
                // make sure it is unique
                pairingOperation.ID = GetRandomPairingCode();
            }

            db.PairingOperations.Add(pairingOperation);
            db.SaveChanges();

            return Ok(pairingOperation.ID);
        }

        // GET: api/PairingOperations/id/uuid
        [Route("GetPairingStatus/{id}/{uuid}")]
        [ResponseType(typeof(PairingOperation))]
        public IHttpActionResult GetPairingStatus(string id, string uuid)
        {
            PairingOperation pairingOperation = db.PairingOperations.Find(id);
            if (pairingOperation == null)
            {
                return NotFound();
            }

            if (pairingOperation.DeviceUUID == uuid)
            {
                // only allow the status to be polled if the UUID is known
                return Ok(pairingOperation);
            }
            else
            {
                return BadRequest();
            }
        } 

        // GET: api/PairingOperations/FinishPairing/
        [Route("FinishPairing/{id}/{uuid}")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetFinishPairing(string id, string uuid)
        {
            PairingOperation pairingOperation = db.PairingOperations.Find(id);
            if (pairingOperation == null)
            {
                return NotFound();
            }

            if (pairingOperation.DeviceUUID == uuid)
            {
                // only allow the pairing session to be closed by the original device
                db.Entry(pairingOperation).State = EntityState.Deleted;
                db.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PairingOperationExists(string id)
        {
            return db.PairingOperations.Count(e => e.ID == id) > 0;
        }

        protected PairingOperation PairingOperationForUUID(string uuid)
        {
            return db.PairingOperations.SingleOrDefault(e => e.DeviceUUID == uuid);
        }

        private string GetRandomPairingCode()
        {
            Random random = new Random();
            System.IO.StringWriter pairingCodeWriter = new System.IO.StringWriter();
            for (int i = 0; i < PAIRING_CODE_LENGTH; i++)
            {
                pairingCodeWriter.Write(ALLOWED_CHARS[random.Next(0, ALLOWED_CHARS.Length)]);
            }

            return pairingCodeWriter.ToString();
        }
    }
}