using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Tables
{
    public class Special
    {
       

        public int SpecialId { get; set; }
        [Required(ErrorMessage = "Title Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description Required")]
        public string SpecialDescription { get; set; }
    }
}
