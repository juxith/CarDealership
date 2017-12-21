using CarDealership.Models.Identity;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data
{
    public interface IRepositoryInterface
    {
        //Vehicle
        List<Vehicle> GetVehiclesBySearch(bool? isNew, string searchTerm, decimal minPrice, decimal maxPrice, int minYear, int maxYear);

        List<Vehicle> GetAllNewVehicles();
        List<Vehicle> GetAllUsedVehicles();

        void AddVehicle(Vehicle addThisVehicle);
        void EditVehicle(Vehicle editThisVehicle);
        void DeleteVehicle(int vehicleId);

        Vehicle GetSingleVehicle(int vehicleId);

        List<Vehicle> GetAllFeatured();

        //Specials
        List<Special> GetAllSpecials();
        void AddSpecial(Special addThisSpecial);
        void EditSpecial(Special editThisSpecial);
        void DeleteSpecial(int specialId);

        //ContactUs
        void CreateContactUs(ContactUs addThisContactUs);

        //Purchase
        void AddAPurchase(Purchase addThisPurchase);
        List<PurchaseType> GetAllPurchaseTypes();
        PurchaseType GetPurchaseType(int id);

        //Users
        List<User> GetAllUsers();
        User GetSingleUser(string userId);
        void AddUser(User addThisUser);
        void EditUser(User editThisUser);
        //void ChangePassword(User thisUser, string password, string confirmPassword);

        //Roles
        List<IdentityRole> GetAllRoles();

        //Makes
        List<Make> GetAllMakes();
        void AddMake(Make addThisMake);

        //Models
        List<Model> GetAllModels();
        void AddModels(Model addThisModel);
        List<Model> GetAllModelsByMake(int makeId);

        //BodyTypes
        List<BodyType> GetAllBodyTypes();
        BodyType GetBodyType(int id);

        //BodyColor
        List<BodyColor> GetAllBodyColor();

        BodyColor GetBodyColor(int id);

        //InteriorColor
        List<InteriorColor> GetAllInteriorColor();

        InteriorColor GetInteriorColor(int id);

        //InventoryReports
        List<InventoryReport> GetAllInventoryReportsForVehicles(bool isNew);
        List<InventoryReport> GetAllInventoryReportsForOldVehicles();

        //SalesReports
        List<SaleReport> GetAllSalesReports();
        List<SaleReport> GetSaleReportsByUser(int userId);
        List<SaleReport> GetSaleReportsByUserAndDate(string userId, DateTime min, DateTime max);
    }
}
