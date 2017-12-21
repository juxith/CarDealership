using CarDealership.Data;
using CarDealership.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Tests
{
    [TestFixture]
    public class InMemoryTests
    {
        [Test]
        public void MockCanNoSearchFilter()
        {
            var newRepo = new InMemoryRepository();
            var newVehicles = newRepo.GetVehiclesBySearch(true, null, 0, 0, 0, 0);

            Assert.AreEqual(1, newVehicles.Count);
            Assert.AreEqual("Sport", newVehicles[0].BodyType.BodyTypeName);

            var oldRepo = new InMemoryRepository();
            var oldVehicles = oldRepo.GetVehiclesBySearch(false, null, 0, 0, 0, 0);
            Assert.AreEqual(1, oldVehicles.Count);
            Assert.AreEqual("Truck", oldVehicles[0].BodyType.BodyTypeName);
        }

        [Test]
        public void MockCanSearchFilter()
        {
            var newRepo = new InMemoryRepository();
            var newVehicles = newRepo.GetVehiclesBySearch(true, "Subaru", 10000, 35000, 0, 2020);
            Assert.AreEqual(1, newVehicles.Count);

            var oldRepo = new InMemoryRepository();

            var testYearTerm = oldRepo.GetVehiclesBySearch(false, "Silverado 2010", 10000, 0, 0, 0);
            Assert.AreEqual(1, testYearTerm.Count);

            var oldVehicles = oldRepo.GetVehiclesBySearch(false, "Subaru", 10000, 35000, 0, 0);
            Assert.AreEqual(0, oldVehicles.Count);
        }

        [Test]
        public void MockGetAllUsedVehicles()
        {
            var repo = new InMemoryRepository();
            var vehicles = repo.GetAllUsedVehicles();

            Assert.AreEqual(1, vehicles.Count);
            Assert.AreEqual(1, vehicles[0].VehicleId);
        }

        [Test]
        public void MockGetAllNewVehicles()
        {
            var repo = new InMemoryRepository();
            var vehicles = repo.GetAllNewVehicles();

            Assert.AreEqual(1, vehicles.Count);
            Assert.AreEqual(2, vehicles[0].VehicleId);
        }

        [Test]
        public void MockAddVehicle()
        {
            var repo = new InMemoryRepository();

            var allVehicles = repo.GetAllVehicles();

            var addThis = new Vehicle()
            {
                VehicleId = 3,
                VinNumber = "2222222222222222",
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
                ImageFileLink = null,
            };

            repo.AddVehicle(addThis);

            Assert.AreEqual(3, allVehicles.Count);
        }

        [Test]
        public void MockEditVehicle()
        {
            var repo = new InMemoryRepository();

            var allVehicles = repo.GetAllVehicles();

            var addThis = new Vehicle()
            {
                VehicleId = 3,
                VinNumber = "2222222222222222",
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
                ImageFileLink = null,
            };

            repo.AddVehicle(addThis);

            Assert.AreEqual(3, allVehicles.Count);

            var editThis = allVehicles.SingleOrDefault(v => v.VinNumber == "2222222222222222");

            editThis.Mileage = 200000;
            editThis.IsAutomatic = false;

            repo.EditVehicle(editThis);

            Assert.AreEqual(false, editThis.IsAutomatic);
            Assert.AreEqual(200000, editThis.Mileage);
        }

        [Test]
        public void MockDeleteVehicle()
        {
            var repo = new InMemoryRepository();

            var allVehicles = repo.GetAllNewVehicles();

            repo.DeleteVehicle(2);

            Assert.AreEqual(1, allVehicles.Count);
        }

        [Test]
        public void MockGetSingleVehicle()
        {
            var repo = new InMemoryRepository();

            var thisVehicle = repo.GetSingleVehicle(1);

            Assert.AreEqual(2010, thisVehicle.VehicleYear);
        }
    }
}
