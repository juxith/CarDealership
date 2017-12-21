using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Models
{
    public class VehicleVM
    {
        public Vehicle Vehicle { get; set; }
        public List<SelectListItem> BodyTypeItems { get; set; }
        public List<SelectListItem> BodyColorItems { get; set; }
        public List<SelectListItem> InteriorItems { get; set; }
        public List<SelectListItem> MakeItems { get; set; }
        public List<SelectListItem> ModelItems { get; set; }
        public List<SelectListItem> TransmissionItems { get; set; }
        public List<SelectListItem> TypeItems { get; set; }

        public VehicleVM()
        {
            Vehicle = new Vehicle();
            BodyTypeItems = new List<SelectListItem>();
            BodyColorItems = new List<SelectListItem>();
            InteriorItems = new List<SelectListItem>();
            MakeItems = new List<SelectListItem>();
            ModelItems = new List<SelectListItem>();
            TransmissionItems= ListTransmission;
            TypeItems = ListOfTypes;
        }

        public List<SelectListItem> ListTransmission = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Automatic", Value="true"},
            new SelectListItem() {Text = "Manual", Value="false"}
        };

        public List<SelectListItem> ListOfTypes = new List<SelectListItem>()
        {
            new SelectListItem() {Text="New", Value="true"},
            new SelectListItem() {Text = "Used", Value="false"}
        };

        public void SetMakeItems(IEnumerable<Make> makes)
        {
            foreach (var make in makes)
            {
                MakeItems.Add(new SelectListItem()
                {
                    Value = make.MakeId.ToString(),
                    Text = make.MakeName
                });
            }
        }

        public void SetModelItems(IEnumerable<Model> models)
        {
            foreach (var model in models)
            {
                ModelItems.Add(new SelectListItem()
                {
                    Value = model.ModelId.ToString(),
                    Text = model.ModelName
                });
            }
        }

        public void SetBodyTypeItems(IEnumerable<BodyType> bodyTypes)
        {
            foreach(var type in bodyTypes)
            {
                BodyTypeItems.Add(new SelectListItem()
                {
                    Value = type.BodyTypeId.ToString(),
                    Text = type.BodyTypeName
                });
            }
        }

        public void SetBodyColorItems(IEnumerable<BodyColor> bodyColors)
        {
            foreach (var color in bodyColors)
            {
                BodyColorItems.Add(new SelectListItem()
                {
                    Value = color.BodyColorId.ToString(),
                    Text = color.ColorName
                });
            }
        }

        public void SetInteriorItems(IEnumerable<InteriorColor> interiorColors)
        {
            foreach (var color in interiorColors)
            {
                InteriorItems.Add(new SelectListItem()
                {
                    Value = color.InteriorColorId.ToString(),
                    Text = color.InteriorColorName
                });
            }
        }
    }
}