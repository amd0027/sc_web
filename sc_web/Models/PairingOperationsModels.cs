using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sc_web.Models
{
    public class UpdateCheck
    {
        public int LatestMajorVersion { get; set; }
        public int LatestMinorVersion { get; set; }

        public string FirmwareURL { get; set; }

        public override string ToString()
        {
            return $"v{LatestMajorVersion}.{LatestMinorVersion}";
        }
    }

    public class PairingOperation
    {
        public string ID { get; set; }
        public string DeviceUUID { get; set; }
        public string AuthKey { get; set; }
    }
}