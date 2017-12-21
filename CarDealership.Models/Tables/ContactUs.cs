using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Tables
{
    public class ContactUs
    {
        public int ContactUsId { get; set; }
        public int? VehicleId { get; set; }

        [Required(ErrorMessage = "Contact Name Required")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "E-mail Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Required")]
        public string  Phone { get; set; }

        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
