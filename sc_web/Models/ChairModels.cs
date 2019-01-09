using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sc_web.Models
{
    public class PairingRequest
    {
        [Required]
        [Display(Name = "Pairing Code")]
        public string PairingCode { get; set; }
    }
}