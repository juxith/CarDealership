using CarDealership.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Tables
{
    public class Make
    {
        public int MakeId { get; set; }
   
        public string MakeName { get; set; }
        public string UserId { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual User User { get; set; }
    }
}
