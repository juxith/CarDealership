using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models.Tables;
using CarDealership.Models.Queries;
using CarDealership.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarDealership.Data
{
    public class InMemoryRepository : IRepositoryInterface
    {
        static List<User> _listOfUsers = new List<User>();
        //static List<Role> _listOfRoles = new List<Role>();
        static List<Vehicle> _listOfVehicles = new List<Vehicle>();
        static List<Purchase> _listOfPurchases = new List<Purchase>();
        static List<PurchaseType> _listOfPurchaseTypes = new List<PurchaseType>();
        static List<Make> _listOfMakes = new List<Make>();
        static List<Model> _listOfModels = new List<Model>();
        static List<Special> _listOfSpecials = new List<Special>();
        static List<InteriorColor> _listOfInteriorColors = new List<InteriorColor>();
        static List<BodyColor> _listOfBodyColors = new List<BodyColor>();
        static List<BodyType> _listOfBodyTypes = new List<BodyType>();
        static List<ContactUs> _listOfContactUs = new List<ContactUs>();

        public InMemoryRepository()
        {
            LoadLists();
        }

        public void LoadLists()
        {
        //    _listOfRoles = new List<Role>()
        //{
        //    new Role
        //    {
        //        Id = "role1",
        //        Name = "Admin"
        //    },
        //    new Role
        //    {
        //        Id = "role2",
        //        Name = "Sales"
        //    },
        //};

            _listOfUsers = new List<User>()
        {
            new User
            {
            Id = "admin1",
            FirstName = "Mark",
            LastName = "Johnson",
            },
            new User
            {
            Id = "user2",
            FirstName = "Lindsey",
            LastName = "Parlow",
            }
        };

            _listOfMakes = new List<Make>()
        {
            new Make
            {
                MakeId = 1,
                MakeName = "Chevrolet",
                UserId = "admin1",
                DateAdded = new DateTime(2009, 10, 31),

                User = _listOfUsers[0],

            },
            new Make
            {
                MakeId = 2,
                MakeName = "Subaru",
                UserId = "admin1",
                DateAdded = new DateTime(2009, 11, 10),

                User = _listOfUsers[0],
            }
        };

            _listOfModels = new List<Model>()
        {
            new Model
            {
                ModelId = 1,
                ModelName = "Silverado",
                UserId = "admin1",
                MakeId = 1,
                DateAdded = new DateTime(2009, 10, 31),

                User = _listOfUsers[0],
                Make = _listOfMakes[0]
            },
            new Model
            {
                ModelId = 2,
                ModelName = "Impreza WRX",
                UserId = "admin1",
                MakeId = 2,
                DateAdded = new DateTime(2010, 03, 16),

                User = _listOfUsers[0],
                Make = _listOfMakes[1]
            }
        };

            _listOfSpecials = new List<Special>()
        {
            new Special
            {
                SpecialId = 1,
                Title = "All NEW trucks on sale",
                SpecialDescription = "All NEW premium trucks on sale for the Christmas season. Answer Dad's letters to Santa",
            },
            new Special
            {
                SpecialId = 2,
                Title = "Spring Used Car Sale",
                SpecialDescription = "Start off spring with an upgrade. Check out all our used cars today for title prices covered.",
            }
        };

            _listOfBodyColors = new List<BodyColor>()
        {
            new BodyColor
            {
                BodyColorId = 1,
                ColorName= "Black",
            },
            new BodyColor
            {
                  BodyColorId = 2,
                ColorName= "White",
            }
        };

            _listOfInteriorColors = new List<InteriorColor>()
        {
            new InteriorColor
            {
                InteriorColorId = 1,
                InteriorColorName = "Black Leather"
            },
            new InteriorColor
            {
                InteriorColorId = 1,
                InteriorColorName = "Gunmetal"
            }
        };

            _listOfBodyTypes = new List<BodyType>()
        {
            new BodyType
            {
                BodyTypeId = 1,
                BodyTypeName = "Truck",
            },
            new BodyType
            {
                BodyTypeId = 2,
                BodyTypeName = "Sport",
            },
        };

            _listOfVehicles = new List<Vehicle>()
        {
            new Vehicle()
            {
                VehicleId = 1,
                VinNumber = "123456789101112123",
                ModelId = 1,
                VehicleYear = 2010,
                BodyTypeId = 1,
                IsAutomatic = true,
                BodyColorId = 1,
                InteriorColorId = 1,
                Mileage = 150000,
                SalePrice = 20000,
                MSRP = 25000,
                VehicleDescription = "A beautiful black car",
                IsNewType = false,
                IsPurchased = true,
                IsFeatured = true,
                ImageFileLink = null,

                BodyType = _listOfBodyTypes[0],
                Model = _listOfModels[0],
                InteriorColor = _listOfInteriorColors[0],
                BodyColor = _listOfBodyColors[0],
            },
            new Vehicle()
            {
                VehicleId = 2,
                VinNumber = "123456789101112123",
                ModelId = 2,
                VehicleYear = 2018,
                BodyTypeId = 2,
                IsAutomatic = false,
                BodyColorId = 2,
                InteriorColorId = 2,
                Mileage = 0,
                SalePrice = 29000,
                MSRP = 33000,
                VehicleDescription = "A beautiful white car",
                IsNewType = true,
                IsPurchased = false,
                IsFeatured = true,
                ImageFileLink = null,

                BodyType = _listOfBodyTypes[1],
                Model = _listOfModels[1],
                InteriorColor = _listOfInteriorColors[1],
                BodyColor = _listOfBodyColors[1],
            }
        };

            _listOfContactUs = new List<ContactUs>()
        {
            new ContactUs
            {
                ContactUsId= 1,
                ContactName= "Judy Thao",
                VehicleId = null,
                Email = "tsg@tsg.com",
                Phone = "555 555 5555",
                Message = "I'm interested in ",
            },
            new ContactUs
            {
                ContactUsId= 2,
                ContactName= "Aj Rohde",
                VehicleId = 1,
                Email = "tsg@tsg.com",
                Phone = "555 555 5555",
                Message = "I like that truck. Could you tell me more about it.",

                Vehicle = _listOfVehicles[0]
            }
        };


            _listOfPurchaseTypes = new List<PurchaseType>()
        {
            new PurchaseType
            {
                PurchaseTypeId = 1,
                PurchaseTypeName = "Bank Finance"
            },
            new PurchaseType
            {
                PurchaseTypeId = 2,
                PurchaseTypeName = "Dealer Finance"
            },
            new PurchaseType
            {
                PurchaseTypeId = 3,
                PurchaseTypeName = "Cash"
            }
        };

            _listOfPurchases = new List<Purchase>()
        {
            new Purchase
            {
                PurchaseId = 1,
                VehicleId = 1,
                CustomerFirstName = "Robert",
                CustomerLastName = "Reynolds",
                Email = "rr@gmail.com",
                Phone = "111 111 1111",
                Street1 = "12345 Guild Drive",
                City = "Arden Hills",
                StateAbrv = "MN",
                Zipcode = "55378",
                PurchasePrice = 19500,
                PurchaseTypeId = 1,
                UserId = "user2",

                PurchaseType = _listOfPurchaseTypes[0],
                User = _listOfUsers[0],
                Vehicle =_listOfVehicles[0]

            }
        };
        }


        //Vehicle
        public List<Vehicle> GetVehiclesBySearch(bool? isNew, string searchTerm, decimal minPrice, decimal maxPrice, int minYear, int maxYear)
        {
            var filterByThis = new List<Vehicle>();

            switch(isNew)
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
            var newVehicles = _listOfVehicles.Where(i => i.IsNewType == true);
            return newVehicles.ToList();
        }
        public List<Vehicle> GetAllUsedVehicles()
        {
            var usedVehicles = _listOfVehicles.Where(i => i.IsNewType == false);
            return usedVehicles.ToList();
        }

        public void AddVehicle(Vehicle addThisVehicle)
        {
            _listOfVehicles.Add(addThisVehicle);
        }
        public void EditVehicle(Vehicle editThisVehicle)
        {
            var deleteThisOne = _listOfVehicles.SingleOrDefault(i => i.VehicleId == editThisVehicle.VehicleId);
            _listOfVehicles.Remove(deleteThisOne);
            _listOfVehicles.Add(editThisVehicle);
        }
        public void DeleteVehicle(int vehicleId)
        {
            var deleteThisOne = _listOfVehicles.SingleOrDefault(i => i.VehicleId == vehicleId);
            _listOfVehicles.Remove(deleteThisOne);
        }

        public Vehicle GetSingleVehicle(int vehicleId)
        {
            var findThisOne = _listOfVehicles.SingleOrDefault(i => i.VehicleId == vehicleId);
            return findThisOne;
        }

        public List<Vehicle> GetAllVehicles()
        {
            var allUnPurchasedVehicles = _listOfVehicles.Where(i => i.IsPurchased == false);
            return allUnPurchasedVehicles.ToList();
        }

        public List<Vehicle> GetAllFeatured()
        {
            var isFeaturedVehicles = _listOfVehicles.Where(i => i.IsFeatured == true);
            return isFeaturedVehicles.ToList();
        }

        //Specials
        public List<Special> GetAllSpecials()
        {
            return _listOfSpecials;
        }
        public void AddSpecial(Special addThisSpecial)
        {
            _listOfSpecials.Add(addThisSpecial);
        }
        public void EditSpecial(Special editThisSpecial)
        {
            var editThis = _listOfSpecials.SingleOrDefault(s => s.SpecialId == editThisSpecial.SpecialId);
            _listOfSpecials.Remove(editThis);
            _listOfSpecials.Add(editThisSpecial);
        }
        public void DeleteSpecial(int specialId)
        {
            var deleteThis = _listOfSpecials.SingleOrDefault(s => s.SpecialId == specialId);
            _listOfSpecials.Remove(deleteThis);
        }

        //ContactUs
        public void CreateContactUs(ContactUs addThisContactUs)
        {
            var id = _listOfContactUs.Max(m => m.ContactUsId);
            addThisContactUs.ContactUsId = id;
            _listOfContactUs.Add(addThisContactUs);
        }

        //Purchase
        public void AddAPurchase(Purchase addThisPurchase)
        {
            addThisPurchase.Vehicle.IsPurchased = true;
            _listOfPurchases.Add(addThisPurchase);
        }

        public List<PurchaseType> GetAllPurchaseTypes()
        {
            return _listOfPurchaseTypes.ToList();
        }

        public PurchaseType GetPurchaseType(int id)
        {
            var type = _listOfPurchaseTypes.SingleOrDefault(i => i.PurchaseTypeId == id);
            return type;
        }

        //Users
        public List<User> GetAllUsers()
        {
            return _listOfUsers;
        }
        public User GetSingleUser(string userId)
        {
            var thisUser = _listOfUsers.SingleOrDefault(u => u.Id == userId);
            return thisUser;
        }
        public void AddUser(User addThisUser)
        {
            _listOfUsers.Add(addThisUser);
        }
        public void EditUser(User editThisUser)
        {
            var deleteThis = _listOfUsers.SingleOrDefault(u => u.Id == editThisUser.Id);
            _listOfUsers.Remove(deleteThis);
            _listOfUsers.Add(editThisUser);

        }
        //public void ChangePassword(User thisUser, string password, string confirmPassword)
        //{
        //    var thisUsersPassword = _listOfUsers.SingleOrDefault(u => u.Id == thisUser.Id);
        //    thisUsersPassword.Password = password;
        //}

        //Roles
        public List<IdentityRole> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        //Makes
        public List<Make> GetAllMakes()
        {
            return _listOfMakes;
        }
        public void AddMake(Make addThisMake)
        {
            _listOfMakes.Add(addThisMake);
        }

        //Models
        public List<Model> GetAllModels()
        {
            return _listOfModels;
        }
        public void AddModels(Model addThisModel)
        {
            _listOfModels.Add(addThisModel);
        }
        public List<Model> GetAllModelsByMake(int makeId)
        {
            var modelsByMake = _listOfModels.Where(m => m.Make.MakeId == makeId);
            return modelsByMake.ToList();
        }

        //BodyTypes
        public List<BodyType> GetAllBodyTypes()
        {
            return _listOfBodyTypes;
        }

        public BodyType GetBodyType(int id)
        {
            var type = _listOfBodyTypes.SingleOrDefault(i => i.BodyTypeId == id);
            return type;
        }

        //BodyColor
        public List<BodyColor> GetAllBodyColor()
        {
            return _listOfBodyColors;
        }

        public BodyColor GetBodyColor(int id)
        {
            var color = _listOfBodyColors.SingleOrDefault(i => i.BodyColorId == id);
            return color;
        }

        //InteriorColor
        public List<InteriorColor> GetAllInteriorColor()
        {
            return _listOfInteriorColors;
        }

        public InteriorColor GetInteriorColor(int id)
        {
            var color = _listOfInteriorColors.SingleOrDefault(i => i.InteriorColorId == id);
            return color;
        }

        //InventoryReports
        public List<InventoryReport> GetAllInventoryReportsForVehicles(bool isNew)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
