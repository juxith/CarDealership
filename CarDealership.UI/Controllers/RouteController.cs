using CarDealership.Data;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CarDealership.UI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RouteController : ApiController
    {
        [AllowAnonymous]
        [Route("NewInventory/{isNew}/{searchTerm}/{minPrice}/{maxPrice}/{minYear}/{maxYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetNewVehiclesBySearch(bool isNew, string searchTerm, decimal minPrice, decimal maxPrice, int minYear, int maxYear)
        {
            var vehicles = RepositoryFactory.Create().GetVehiclesBySearch(isNew, searchTerm, minPrice, maxPrice, minYear, maxYear);

            if(vehicles == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(vehicles);
            }
        }

        [AllowAnonymous]
        [Route("NewInventory/{vehicleId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetNewVehicleDetails(int vehicleId)
        {
            var vehicles = RepositoryFactory.Create().GetSingleVehicle(vehicleId);

            if (vehicles == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(vehicles);
            }
        }

        [AllowAnonymous]
        [Route("UsedInventory/{isNew}/{searchTerm}/{minPrice}/{maxPrice}/{minYear}/{maxYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetUsedVehiclesBySearch(bool isNew, string searchTerm, decimal minPrice, decimal maxPrice, int minYear, int maxYear)
        {
            var vehicles = RepositoryFactory.Create().GetVehiclesBySearch(isNew, searchTerm, minPrice, maxPrice, minYear, maxYear);

            if (vehicles == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(vehicles);
            }
        }

        [AllowAnonymous]
        [Route("UsedInventory/{vehicleId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetUsedVehicleDetails(int vehicleId)
        {
            var vehicles = RepositoryFactory.Create().GetSingleVehicle(vehicleId);

            if (vehicles == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(vehicles);
            }
        }

        [Authorize(Roles = "admin,sales")]
        [Route("Sales/null/{searchTerm}/{minPrice}/{maxPrice}/{minYear}/{maxYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllNonPurchasedVehiclesBySearchForSales(string searchTerm, decimal minPrice, decimal maxPrice, int minYear, int maxYear)
        {
            var vehicles = RepositoryFactory.Create().GetVehiclesBySearch(null, searchTerm, minPrice, maxPrice, minYear, maxYear);

            if (vehicles == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(vehicles);
            }
        }
 
        [Authorize(Roles = "admin")]
        [Route("Admin/null/{searchTerm}/{minPrice}/{maxPrice}/{minYear}/{maxYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllNonPurchasedVehiclesBySearchForAdmin(string searchTerm, decimal minPrice, decimal maxPrice, int minYear, int maxYear)
        {
            var vehicles = RepositoryFactory.Create().GetVehiclesBySearch(null, searchTerm, minPrice, maxPrice, minYear, maxYear);

            if (vehicles == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(vehicles);
            }
        }

        [Authorize(Roles = "admin")]
        [Route("Admin/AddMake/Makes")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllMakes()
        {
            var makes = RepositoryFactory.Create().GetAllMakes();

            if (makes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(makes);
            }
        }

        [Authorize(Roles = "admin")]
        [Route("Admin/AddModel/Makes")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllMakesToAddModel()
        {
            var makes = RepositoryFactory.Create().GetAllMakes();

            if (makes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(makes);
            }
        }

        [Authorize(Roles = "admin")]
        [Route("Admin/AddModel/Models")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllModels()
        {
            var makes = RepositoryFactory.Create().GetAllModels();

            if (makes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(makes);
            }
        }

        [Authorize(Roles = "admin")]
        [Route("Admin/AdminIndex/{makeId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllModelsByMake(int makeId)
        {
            var makes = RepositoryFactory.Create().GetAllModelsByMake(makeId);

            if (makes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(makes);
            }
        }

        [AllowAnonymous]
        [Route("Home/Specials/Load")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllSpecials()
        {
            var makes = RepositoryFactory.Create().GetAllSpecials();

            if (makes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(makes);
            }
        }

        [Authorize(Roles = "admin")]
        [Route("Admin/Edit/{id}/Delete")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteVehicle(int id)
        {
            RepositoryFactory.Create().DeleteVehicle(id);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [Route("Admin/Specials/Delete/{id}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteSpecial(int id)
        {
            RepositoryFactory.Create().DeleteSpecial(id);
            return Ok();
        }

        [AllowAnonymous]
        [Route("Home/Index/IsFeatured")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllFeaturedVehicles()
        {
            var vehicles = RepositoryFactory.Create().GetAllFeatured();
            return Ok(vehicles);
        }

        //[Authorize(Roles = "admin")]
        [Route("Reports/Sales/{userId}/{min}/{max}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetSalesReport(string userId, string min, string max)
        {
            var repo = RepositoryFactory.Create();
            var minDate = new DateTime();
            var maxDate = new DateTime();
            
            //DateTime.TryParse(min, out maxDate);

            if(DateTime.TryParse(min, out minDate) && DateTime.TryParse(max, out maxDate))
            {
                var report = repo.GetSaleReportsByUserAndDate(userId, minDate, maxDate);
                return Ok(report);
            }
            else
            {
                return NotFound();
            }
        }


        //[Authorize(Roles ="admin")]
        [Route("Reports/Sales/Users")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetUsers()
        {
            var users = RepositoryFactory.Create().GetAllUsers();

            if (users == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(users);
            }
        }
    }
}
