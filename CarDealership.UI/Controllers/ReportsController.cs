using CarDealership.Data;
using CarDealership.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult ReportsIndex()
        {
            return View();
        }

        public ActionResult Sales()
        {
            return View();
        }

        public ActionResult Inventory()
        {
            var repo = RepositoryFactory.Create();
            var reports = new ReportsVM();

            reports.NewVehicles = repo.GetAllInventoryReportsForVehicles(true);
            reports.UsedVehicles = repo.GetAllInventoryReportsForVehicles(false);

            return View(reports);
        }
    }
}