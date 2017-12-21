using CarDealership.Models.Identity;
using CarDealership.Models.Tables;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data
{
    public class CarDealershipEntities : IdentityDbContext<User>
    {
        public CarDealershipEntities() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Special> Specials { get; set; }
        public DbSet<PurchaseType> PurchaseTypes { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<InteriorColor> InteriorColors { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<BodyType> BodyTypes { get; set; }
        public DbSet<BodyColor> BodyColors { get; set; }
    }
}
