using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace CarDealership.Data
{
    public class EFRepository : IRepositoryInterface
    {
        public List<Vehicle> GetVehiclesBySearch(bool? isNew, string searchTerm, decimal minPrice, decimal maxPrice, int minYear, int maxYear)
        {
            var filterByThis = new List<Vehicle>();

            switch (isNew)
            {
                case true:
                    filterByThis = GetAllNewVehicles();
                    break;
                case false:
                    filterByThis = GetAllUsedVehicles();
                    break;
                default:
                    filterByThis = GetAllVehicles();
                    break;
            }

            if (searchTerm == "_" && minPrice == 0 && maxPrice == 0 && minYear == 0 && maxYear == 0)
            {
                var noFilters = filterByThis.OrderBy(p => p.MSRP).Take(20);
                return noFilters.ToList();
            }

            if (maxPrice == 0)
            {
                maxPrice = filterByThis.Max(s => s.SalePrice);
            }

            if (maxYear == 0)
            {
                maxYear = filterByThis.Max(y => y.VehicleYear);
            }

            if (searchTerm == "_")
            {
                var dropFilter = filterByThis.Where(p => p.SalePrice > minPrice && p.SalePrice <= maxPrice);
                dropFilter = dropFilter.Where(y => y.VehicleYear >= minYear && y.VehicleYear <= maxYear).OrderBy(m => m.MSRP).Take(20);
                return dropFilter.ToList();
            }
            else
            {
                string[] disectTerm = searchTerm.Split(' ');

                foreach (var term in disectTerm)
                {
                    if (!(filterByThis.Where(n => n.Model.ModelName.Contains(term) || n.Model.Make.MakeName.Contains(term)).ToList().Count == 0))
                    {
                        filterByThis = filterByThis.Where(n => n.Model.ModelName.Contains(term) || n.Model.Make.MakeName.Contains(term)).ToList();
                    }
                    else if (int.TryParse(term, out int thisYear))
                    {
                        if (!(filterByThis.Where(y => y.VehicleYear == thisYear).ToList().Count == 0))
                        {
                            filterByThis = filterByThis.Where(y => y.VehicleYear == thisYear).ToList();
                        }
                    }
                    else
                    {
                        filterByThis.Clear();
                    }
                }
                filterByThis = filterByThis.Where(p => p.SalePrice >= minPrice && p.SalePrice <= maxPrice).ToList();
                filterByThis = filterByThis.Where(y => y.VehicleYear >= minYear && y.VehicleYear <= maxYear).OrderBy(m => m.MSRP).ToList();

                var returnThis = filterByThis.Take(20);

                return returnThis.ToList();
            }
        }

        public List<Vehicle> GetAllNewVehicles()
        {
            using (var ctx = new CarDealershipEntities())
            {
                var newVehicles = ctx.Vehicles.Include("Model").Include("Model.Make").Include("BodyType").Include("BodyColor").Include("InteriorColor").Include("Special").Where(i => i.IsNewType == true).Where(i => i.IsPurchased == false);
                return newVehicles.ToList();
            }
        }
        public List<Vehicle> GetAllUsedVehicles()
        {
            using (var ctx = new CarDealershipEntities())
            {
                var usedVehicles = ctx.Vehicles.Include("Model").Include("Model.Make").Include("BodyType").Include("BodyColor").Include("InteriorColor").Include("Special").Where(i => i.IsNewType == false).Where(i => i.IsPurchased == false);
                return usedVehicles.ToList();
            }
        }

        public void AddVehicle(Vehicle addThisVehicle)
        {
            using (var ctx = new CarDealershipEntities())
            {
                //model
                addThisVehicle.Model = ctx.Models.SingleOrDefault(m => m.ModelId == addThisVehicle.Model.ModelId);
                //make
                //addThisVehicle.Model.Make = ctx.Makes.SingleOrDefault(m => m.MakeId == addThisVehicle.Model.MakeId);
                //bodyType
                addThisVehicle.BodyType = ctx.BodyTypes.SingleOrDefault(b => b.BodyTypeId == addThisVehicle.BodyTypeId);
                //bodyColor
                addThisVehicle.BodyColor = ctx.BodyColors.SingleOrDefault(c => c.BodyColorId == addThisVehicle.BodyColorId);
                //interiorColor
                addThisVehicle.InteriorColor = ctx.InteriorColors.SingleOrDefault(i => i.InteriorColorId == addThisVehicle.InteriorColorId);

                ctx.Vehicles.Add(addThisVehicle);
                ctx.SaveChanges();
            }
        }
        public void EditVehicle(Vehicle editThisVehicle)
        {
            using (var ctx = new CarDealershipEntities())
            {
                //model
                editThisVehicle.Model = ctx.Models.SingleOrDefault(m => m.ModelId == editThisVehicle.ModelId);
                //make
                editThisVehicle.Model.Make = ctx.Makes.SingleOrDefault(m => m.MakeId == editThisVehicle.Model.MakeId);
                //bodyType
                editThisVehicle.BodyType = ctx.BodyTypes.SingleOrDefault(b => b.BodyTypeId == editThisVehicle.BodyTypeId);
                //bodyColor
                editThisVehicle.BodyColor = ctx.BodyColors.SingleOrDefault(c => c.BodyColorId == editThisVehicle.BodyColorId);
                //interiorColor
                editThisVehicle.InteriorColor = ctx.InteriorColors.SingleOrDefault(i => i.InteriorColorId == editThisVehicle.InteriorColorId);

                ctx.Vehicles.Attach(editThisVehicle);
                ctx.Entry(editThisVehicle).State = System.Data.Entity.EntityState.Modified;

                ctx.SaveChanges();
            }
        }
        public void DeleteVehicle(int vehicleId)
        {
            using (var ctx = new CarDealershipEntities())
            {
                var deleteThisOne = ctx.Vehicles.Include("Model").Include("Model.Make").Include("BodyType").Include("BodyColor").Include("InteriorColor").Include("Special").SingleOrDefault(i => i.VehicleId == vehicleId);
                ctx.Vehicles.Remove(deleteThisOne);
                ctx.SaveChanges();
            }
        }

        public Vehicle GetSingleVehicle(int vehicleId)
        {
            using (var ctx = new CarDealershipEntities())
            {
                var findThisOne = ctx.Vehicles.Include("Model").Include("Model.Make").Include("BodyType").Include("BodyColor").Include("InteriorColor").Include("Special").SingleOrDefault(i => i.VehicleId == vehicleId);
                return findThisOne;
            }
        }

        public List<Vehicle> GetAllVehicles()
        {
            using (var ctx = new CarDealershipEntities())
            {
                var allUnPurchasedVehicles = ctx.Vehicles.Include("Model").Include("Model.Make").Include("BodyType").Include("BodyColor").Include("InteriorColor").Include("Special").Where(i => i.IsPurchased == false);
                return allUnPurchasedVehicles.ToList();
            }
        }

        public List<Vehicle> GetAllFeatured()
        {
            using (var ctx = new CarDealershipEntities())
            {
                var isFeaturedVehicles = ctx.Vehicles.Include("Model").Include("Model.Make").Include("BodyType").Include("BodyColor").Include("InteriorColor").Include("Special").Where(i => i.IsFeatured == true);
                return isFeaturedVehicles.ToList();
            }
        }

        //Specials
        public List<Special> GetAllSpecials()
        {
            using (var ctx = new CarDealershipEntities())
            {
                return ctx.Specials.ToList();
            }
        }
        public void AddSpecial(Special addThisSpecial)
        {
            using (var ctx = new CarDealershipEntities())
            {
                ctx.Specials.Add(addThisSpecial);
                ctx.SaveChanges();
            }
        }

        //DO NOT NEED
        public void EditSpecial(Special editThisSpecial)
        {
            using (var ctx = new CarDealershipEntities())
            {
                var editThis = ctx.Specials.SingleOrDefault(s => s.SpecialId == editThisSpecial.SpecialId);
                ctx.Specials.Remove(editThis);
            }
        }
        public void DeleteSpecial(int specialId)
        {
            using (var ctx = new CarDealershipEntities())
            {
                var deleteThis = ctx.Specials.SingleOrDefault(s => s.SpecialId == specialId);
                ctx.Specials.Remove(deleteThis);
                ctx.SaveChanges();
            }
        }

        //ContactUs
        public void CreateContactUs(ContactUs addThisContactUs)
        {
            using (var ctx = new CarDealershipEntities())
            {
                ctx.ContactUs.Add(addThisContactUs);
                ctx.SaveChanges();
            }
        }

        //Purchase
        public void AddAPurchase(Purchase addThisPurchase)
        {
            using (var ctx = new CarDealershipEntities())
            {
                UserStore<User> Store = new UserStore<User>(ctx);
                UserManager<User> userManager = new UserManager<User>(Store);
                var user = userManager.FindById(addThisPurchase.UserId);

                addThisPurchase.User = user;
                addThisPurchase.Vehicle.IsPurchased = true;
                addThisPurchase.DateOfPurchase = DateTime.Now;
                ctx.Purchases.Add(addThisPurchase);
                ctx.PurchaseTypes.Attach(addThisPurchase.PurchaseType);
                ctx.Vehicles.Attach(addThisPurchase.Vehicle);
                ctx.Users.Attach(addThisPurchase.User);
                ctx.Entry(addThisPurchase.Vehicle).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public List<PurchaseType> GetAllPurchaseTypes()
        {
            using (var ctx = new CarDealershipEntities())
            {
                return ctx.PurchaseTypes.ToList();
            }
        }

        public PurchaseType GetPurchaseType(int id)
        {
            using (var ctx = new CarDealershipEntities())
            {
                var type = ctx.PurchaseTypes.SingleOrDefault(i => i.PurchaseTypeId == id);
                return type;
            }
        }

        //Users
        public List<User> GetAllUsers()
        {
            using (var ctx = new CarDealershipEntities())
            {
                return ctx.Users.Include("Roles").ToList();
            }
        }

        public User GetSingleUser(string userId)
        {
            using (var ctx = new CarDealershipEntities())
            {
                var thisUser = ctx.Users.SingleOrDefault(u => u.Id == userId);
                return thisUser;
            }
        }
        public void AddUser(User addThisUser)
        {
            using (var ctx = new CarDealershipEntities())
            {
                var thisUser = ctx.Users.Add(addThisUser);
                ctx.SaveChanges();
            }
        }
        public void EditUser(User editThisUser)
        {
            using (var ctx = new CarDealershipEntities())
            {
                ctx.Users.Attach(editThisUser);
                ctx.SaveChanges();
            }
        }

        //Roles
        public List<IdentityRole> GetAllRoles()
        {
            using (var ctx = new CarDealershipEntities())
            {
                return ctx.Roles.ToList();
            }
        }

        //Makes
        public List<Make> GetAllMakes()
        {
            using (var ctx = new CarDealershipEntities())
            {
                return ctx.Makes.Include("User").ToList();
            }
        }
        public void AddMake(Make addThisMake)
        {
            using (var ctx = new CarDealershipEntities())
            {
                //handles user
                UserStore<User> Store = new UserStore<User>(new CarDealershipEntities());
                UserManager<User> userManager = new UserManager<User>(Store);

                addThisMake.User = userManager.FindById(addThisMake.UserId);

                //Handle Date
                addThisMake.DateAdded = DateTime.Now;
                ctx.Users.Attach(addThisMake.User);
                ctx.Makes.Add(addThisMake);
                ctx.SaveChanges();
            }
        }

        //Models
        public List<Model> GetAllModels()
        {
            using (var ctx = new CarDealershipEntities())
            {
                return ctx.Models.Include("Make").Include("User").ToList();
            }
        }
        public void AddModels(Model addThisModel)
        {
            using (var ctx = new CarDealershipEntities())
            {
                //handles user
                UserStore<User> Store = new UserStore<User>(new CarDealershipEntities());
                UserManager<User> userManager = new UserManager<User>(Store);

                addThisModel.User = userManager.FindById(addThisModel.UserId);
                //handles Makes
                addThisModel.Make = ctx.Makes.Single(m => m.MakeId == addThisModel.MakeId);

                //Handle Date
                addThisModel.DateAdded = DateTime.Now;
                ctx.Users.Attach(addThisModel.User);
                ctx.Models.Add(addThisModel);
                ctx.SaveChanges();
            }
        }
        public List<Model> GetAllModelsByMake(int makeId)
        {
            using (var ctx = new CarDealershipEntities())
            {
                var modelsByMake = ctx.Models.Include("Make").Where(m => m.Make.MakeId == makeId);
                return modelsByMake.ToList();
            }
        }

        //BodyTypes
        public List<BodyType> GetAllBodyTypes()
        {
            using (var ctx = new CarDealershipEntities())
            {
                return ctx.BodyTypes.ToList();
            }
        }

        public BodyType GetBodyType(int id)
        {
            using (var ctx = new CarDealershipEntities())
            {
                var type = ctx.BodyTypes.SingleOrDefault(i => i.BodyTypeId == id);
                return type;
            }
        }

        //BodyColor
        public List<BodyColor> GetAllBodyColor()
        {
            using (var ctx = new CarDealershipEntities())
            {
                return ctx.BodyColors.ToList();
            }
        }

        public BodyColor GetBodyColor(int id)
        {
            using (var ctx = new CarDealershipEntities())
            {
                var color = ctx.BodyColors.SingleOrDefault(i => i.BodyColorId == id);
                return color;
            }
        }

        //InteriorColor
        public List<InteriorColor> GetAllInteriorColor()
        {
            using (var ctx = new CarDealershipEntities())
            {
                return ctx.InteriorColors.ToList();
            }
        }

        public InteriorColor GetInteriorColor(int id)
        {
            using (var ctx = new CarDealershipEntities())
            {
                var color = ctx.InteriorColors.SingleOrDefault(i => i.InteriorColorId == id);
                return color;
            }
        }

        //InventoryReports
        public List<InventoryReport> GetAllInventoryReportsForVehicles(bool isNew)
        {
            using (var ctx = new CarDealershipEntities())
            {
                var listModel = (from m in ctx.Models
                                 join s in ctx.Makes on m.Make.MakeId equals s.MakeId
                                 join v in ctx.Vehicles on m.ModelId equals v.ModelId
                                 where v.IsNewType == isNew
                                 select new
                                 {
                                     model = m,
                                     vehicle = v,
                                     make = s
                                 }).ToList();

                var groupedByModel = listModel
                    .GroupBy(i => new { i.vehicle.VehicleYear, i.model.ModelName })
                    .Select(g => new InventoryReport
                    {
                        VehicleYear = g.Key.VehicleYear,
                        Make = g.First().model.Make.MakeName,
                        Model = g.Key.ModelName,
                        Count = g.Count(),
                        StockValue = g.Sum(i => i.vehicle.MSRP)
                    });

                return groupedByModel.ToList();
            }
        }

        public List<InventoryReport> GetAllInventoryReportsForOldVehicles()
        {
            throw new NotImplementedException();
        }

        //SalesReports
        public List<SaleReport> GetAllSalesReports()
        {
            throw new NotImplementedException();
        }
        public List<SaleReport> GetSaleReportsByUser(int userId)
        {
            throw new NotImplementedException();
        }
        public List<SaleReport> GetSaleReportsByUserAndDate(string userId, DateTime min, DateTime max)
        {
            var listOfSalesReports = new List<SaleReport>();

            UserStore<User> Store = new UserStore<User>(new CarDealershipEntities());
            UserManager<User> userManager = new UserManager<User>(Store);

            using (var ctx = new CarDealershipEntities())
            {
                if (userId == "none")
                {
                    var users = ctx.Users;

                    foreach (var user in users)
                    {
                        var thisSalesReport = new SaleReport();

                        var groupUser = userManager.FindById(user.Id);
                        var groupSales = ctx.Purchases.Where(i => i.UserId == groupUser.Id).Where(d => d.DateOfPurchase >= min && d.DateOfPurchase <= max);
                        var groupCount = groupSales.Count();
                        var groupSalesPrice = groupSales.Sum(p => p.PurchasePrice);

                        if (groupSalesPrice == null)
                        {
                            groupSalesPrice = 0;
                        }

                        thisSalesReport.FirstName = groupUser.FirstName;
                        thisSalesReport.LastName = groupUser.LastName;
                        thisSalesReport.TotalSales = groupSalesPrice;
                        thisSalesReport.TotalVehicles = groupCount;

                        listOfSalesReports.Add(thisSalesReport);
                    }
                }
                else
                {
                    var thisSalesReport = new SaleReport();

                    var thisUser = userManager.FindById(userId);
                    var thisSales = ctx.Purchases.Where(i => i.UserId == thisUser.Id).Where(d => d.DateOfPurchase >= min && d.DateOfPurchase <= max);
                    var thisCount = thisSales.Count();
                    var thisSalesPrice = thisSales.Sum(p => p.PurchasePrice);

                    if (thisSalesPrice == null)
                    {
                        thisSalesPrice = 0;
                    }

                    thisSalesReport.FirstName = thisUser.FirstName;
                    thisSalesReport.LastName = thisUser.LastName;
                    thisSalesReport.TotalSales = thisSalesPrice;
                    thisSalesReport.TotalVehicles = thisCount;

                    listOfSalesReports.Add(thisSalesReport);
                }
            }

            return listOfSalesReports;
        }
    }
}
