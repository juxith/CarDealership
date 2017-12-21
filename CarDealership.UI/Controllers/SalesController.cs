using CarDealership.Data;
using CarDealership.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealership.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace CarDealership.UI.Controllers
{
    [Authorize(Roles = "admin,sales")]
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult SalesIndex()
        {
              return View();
        }

        public ActionResult Purchase(int id)
        {
            var repo = RepositoryFactory.Create();
            var purchaseTypes = repo.GetAllPurchaseTypes();
            var vehicle = repo.GetSingleVehicle(id);
            var viewModel = new PurchaseVM();
            viewModel.Vehicle = vehicle;
            viewModel.SetPurchaseTypeItems(purchaseTypes);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Purchase(PurchaseVM vm)
        {
            var repo = RepositoryFactory.Create();
            var vehicle = repo.GetSingleVehicle(vm.Vehicle.VehicleId);

            if ((vm.Purchase.PurchasePrice < (vehicle.SalePrice * (decimal)0.95)) || (vm.Purchase.PurchasePrice > vehicle.MSRP) || vm.Purchase.PurchasePrice == null)
            {
                ModelState.AddModelError("Purchase.PurchasePrice","Must Enter purchase price and it can not be less than 5% of Sale Price or greater than MSRP");
            }

            if (ModelState.IsValid)
            {
               
                vm.Purchase.UserId = User.Identity.GetUserId();

                vm.Purchase.PurchaseType = repo.GetPurchaseType(vm.Purchase.PurchaseTypeId);
                vm.Purchase.Vehicle = vehicle;
                repo.AddAPurchase(vm.Purchase);
                return RedirectToAction("SalesIndex");
            }
            else
            {
                var purchaseTypes = repo.GetAllPurchaseTypes();
                vm.Vehicle = vehicle;
                vm.SetPurchaseTypeItems(purchaseTypes);
                return View(vm);
            }
        }
    }
}
