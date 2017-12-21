using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using CarDealership.Data;
using CarDealership.Models.Identity;
using CarDealership.Models.Tables;
using CarDealership.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminIndex()
        {
            return View();
        }



        public ActionResult AddVehicle()
        {
            var repo = RepositoryFactory.Create();

            var bodyTypes = repo.GetAllBodyTypes().ToList();
            var bodyColors = repo.GetAllBodyColor();
            var interiorColors = repo.GetAllInteriorColor();
            var makes = repo.GetAllMakes();

            var viewModel = new VehicleVM();
            viewModel.SetBodyTypeItems(bodyTypes);
            viewModel.SetBodyColorItems(bodyColors);
            viewModel.SetInteriorItems(interiorColors);
            viewModel.SetMakeItems(makes);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddVehicle(VehicleVM vm)
        {
            var repo = RepositoryFactory.Create();
            if (ModelState.IsValid)
            {
                repo.AddVehicle(vm.Vehicle);

                return View("AdminIndex");
            }
            else
            {
                var bodyTypes = repo.GetAllBodyTypes().ToList();
                var bodyColors = repo.GetAllBodyColor();
                var interiorColors = repo.GetAllInteriorColor();
                var makes = repo.GetAllMakes();

                var viewModel = new VehicleVM();
                vm.SetBodyTypeItems(bodyTypes);
                vm.SetBodyColorItems(bodyColors);
                vm.SetInteriorItems(interiorColors);
                vm.SetMakeItems(makes);

                return View(viewModel);
            }
        }

        public ActionResult EditVehicle(int id)
        {
            var repo = RepositoryFactory.Create();
            var editThisVehicle = repo.GetSingleVehicle(id);

            var bodyTypes = repo.GetAllBodyTypes().ToList();
            var bodyColors = repo.GetAllBodyColor();
            var interiorColors = repo.GetAllInteriorColor();
            var makeItems = repo.GetAllMakes();
            var modelItems = repo.GetAllModelsByMake(editThisVehicle.Model.Make.MakeId);

            var viewModel = new VehicleVM();
            viewModel.SetBodyTypeItems(bodyTypes);
            viewModel.SetBodyColorItems(bodyColors);
            viewModel.SetInteriorItems(interiorColors);
            viewModel.SetMakeItems(makeItems);
            viewModel.SetModelItems(modelItems);

            viewModel.Vehicle = editThisVehicle;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditVehicle(VehicleVM vm, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (!System.IO.File.Exists("~Images/Uploaded/" + file.FileName))
                    {
                        file.SaveAs(HttpContext.Server.MapPath("~/Images/Uploaded/") + file.FileName);
                    }

                    vm.Vehicle.ImageFileLink = file.FileName;
                }

                RepositoryFactory.Create().EditVehicle(vm.Vehicle);
                return RedirectToAction("AdminIndex");
            }
            else
            {
                var repo = RepositoryFactory.Create();

                var bodyTypes = repo.GetAllBodyTypes().ToList();
                var bodyColors = repo.GetAllBodyColor();
                var interiorColors = repo.GetAllInteriorColor();
                var makeItems = repo.GetAllMakes();
                var modelItems = repo.GetAllModelsByMake(vm.Vehicle.Model.Make.MakeId);

                vm.SetBodyTypeItems(bodyTypes);
                vm.SetBodyColorItems(bodyColors);
                vm.SetInteriorItems(interiorColors);
                vm.SetMakeItems(makeItems);
                vm.SetModelItems(modelItems);

                return View(vm);
            }
        }

        public ActionResult AddMake()
        {
            var model = new Make();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMake(Make model)
        {
            var repo = RepositoryFactory.Create();
            var listOfModels = repo.GetAllMakes();

            if(!ModelState.IsValid)
            {
                return View(model);
            }
            else if (listOfModels.Any(n => n.MakeName == model.MakeName))
            {
                ModelState.AddModelError("MakeName","This make already exisits.");
                return View(model);
            }
            
            //Need to handle user here
            model.UserId = User.Identity.GetUserId();

            repo.AddMake(model);

            return View();
        }

        public ActionResult AddModel()
        {
            var model = new Model();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddModel(Model model)
        {
            var repo = RepositoryFactory.Create();

            var listOfModelsWithMake = repo.GetAllModelsByMake(model.MakeId);
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            else if(listOfModelsWithMake.Any(n => n.ModelName == model.ModelName))
            {
                ModelState.AddModelError("MakeId", "This model already exissts");
                return View(model);
            }

            //Need to handle user
            model.UserId = User.Identity.GetUserId();

            RepositoryFactory.Create().AddModels(model);
            return View();
        }

        public ActionResult Users()
        {
           var users = RepositoryFactory.Create().GetAllUsers();
            
           var roleStore = new RoleStore<IdentityRole>(new CarDealershipEntities());
           var roleMgr = new RoleManager<IdentityRole>(roleStore);

            var roleCollection = roleMgr.Roles;

            foreach (var user in users)
            {
                foreach (var role in roleCollection)
                {
                    if (user.Roles.Any(i => i.RoleId == role.Id))
                    {
                        user.RoleName = role.Name;
                    }
                }
            }

            return View(users);
        }

        public ActionResult AddUser()
        {
            var ctx = new CarDealershipEntities();
            ViewBag.Name = new SelectList(ctx.Roles.ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(RegisterViewModel model)
        {
            var context = new CarDealershipEntities();
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User { UserName = model.UserName, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
            UserStore<User> Store = new UserStore<User>(new CarDealershipEntities());
            UserManager<User> userManager = new UserManager<User>(Store);
            var result = userManager.Create(user, model.Password);
          
            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, model.UserRoles);
                return RedirectToAction("Users", "Admin");
            }

            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");

            return View();

        }

        public ActionResult EditUser(string id)
        {
            var vm = new RegisterViewModel();
            var ctx = new CarDealershipEntities();

            UserStore<User> store = new UserStore<User>(ctx);
            UserManager<User> userMgr = new UserManager<User>(store);

            var thisUser = userMgr.FindById(id);

            //set model sent back to view 
            vm.UserId = thisUser.Id;
            vm.Email = thisUser.Email;
            vm.FirstName = thisUser.FirstName;
            vm.LastName = thisUser.LastName;
            vm.UserName = thisUser.UserName;
      
            ViewBag.Name = new SelectList(ctx.Roles.ToList(), "Name", "Name");

            return View(vm);
        }

        [HttpPost]
        public ActionResult EditUser(RegisterViewModel vm)
        {
            var ctx = new CarDealershipEntities();

            if (!ModelState.IsValid)
            {
                ViewBag.Name = new SelectList(ctx.Roles.ToList(), "Name", "Name");
                return View(vm);
            }


            UserStore<User> store = new UserStore<User>(ctx);
            UserManager<User> userMgr = new UserManager<User>(store);

            var toEditUser = userMgr.FindById(vm.UserId);

            toEditUser.Email = vm.Email;
            toEditUser.FirstName = vm.FirstName;
            toEditUser.LastName = vm.LastName;
            toEditUser.UserName = vm.UserName;
        
            if(vm.ConfirmPassword != null)
            {
                string hashedNewPassword = userMgr.PasswordHasher.HashPassword(vm.Password);
                store.SetPasswordHashAsync(toEditUser, hashedNewPassword);
                store.UpdateAsync(toEditUser);
            }

            var result = userMgr.Update(toEditUser);

            if(result.Succeeded)
            {
                toEditUser.Roles.Clear();
                userMgr.AddToRole(toEditUser.Id, vm.UserRoles);
            }

            return RedirectToAction("Users", "Admin");
        }

        public ActionResult Specials()
        {
            var model = new Special();
            return View(model);
        }

        [HttpPost]
        public ActionResult Specials(Special model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("Form", "Form incomplete");
                return View(model);
            }

            RepositoryFactory.Create().AddSpecial(model);
            return RedirectToAction("Specials");
        }
    }
}