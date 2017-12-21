using CarDealership.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Tables
{
    public class Model
    {
        public int ModelId { get; set; }

   
        public string ModelName { get; set; }
        public string UserId { get; set; }
        public DateTime DateAdded { get; set; }

        public int MakeId { get; set; }

        public virtual User User { get; set; }
        public virtual Make Make { get; set; }
    }
}
