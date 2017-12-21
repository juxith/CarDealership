using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Tables
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        public string VinNumber { get; set; }
        [Required(ErrorMessage = "Model required")]
        public int ModelId { get; set; }
        [Required(ErrorMessage = "Model year required")]
        public int VehicleYear { get; set; }
        [Required(ErrorMessage = "Body Type Required")]
        public int BodyTypeId { get; set; }
        public bool IsAutomatic { get; set; }
        [Required(ErrorMessage = "Body Color Required")]
        public int BodyColorId { get; set; }
        [Required(ErrorMessage = "Interior Color Required")]
        public int InteriorColorId { get; set; }
        [Required(ErrorMessage = "Mileage Required")]
        public int Mileage { get; set; }
        [Required(ErrorMessage = "Sale Price Required")]
        public decimal SalePrice { get; set; }
        [Required(ErrorMessage = "MSRP Required Required")]
        public decimal MSRP { get; set; }
        public string VehicleDescription { get; set; }
        public bool? IsNewType { get; set; }
        public bool IsPurchased { get; set; }
        public bool IsFeatured { get; set; }
        public int? SpecialId { get; set; }
        public string ImageFileLink { get; set; }

        public virtual BodyType BodyType { get; set; }
        public virtual InteriorColor InteriorColor { get; set; }
        public virtual BodyColor BodyColor { get; set; }
        public virtual Model Model { get; set; }
        public virtual Special Special { get; set; }
    }
}
