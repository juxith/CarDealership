using CarDealership.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Tables
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int VehicleId { get; set; }
        [Required(ErrorMessage = "First Name Required")]
        public string CustomerFirstName { get; set; }
        [Required(ErrorMessage = "Last Name Required")]
        public string CustomerLastName { get; set; }
        [Required(ErrorMessage = "E-mail Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone Required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Street Required")]
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        [Required(ErrorMessage = "City Required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State Required")]
        public string StateAbrv { get; set; }
        [Required(ErrorMessage = "Zipcode Required")]
        public string Zipcode { get; set; }
        public DateTime DateOfPurchase { get; set; }

        public decimal? PurchasePrice { get; set; }

        public int PurchaseTypeId { get; set; }
        public string UserId { get; set; }

        public virtual PurchaseType PurchaseType { get; set; }
        public virtual User User {get; set;}
        public virtual Vehicle Vehicle { get; set; }
    }
}
