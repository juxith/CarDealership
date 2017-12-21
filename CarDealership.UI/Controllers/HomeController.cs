using CarDealership.Models.Identity;
using CarDealership.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using CarDealership.Data;
using CarDealership.Models.Tables;

namespace CarDealership.UI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var factory = RepositoryFactory.Create();

            var model = factory.GetAllSpecials();

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<User>>();
            var authManager = HttpContext.GetOwinContext().Authentication;

            // attempt to load the user with this password
            User user = userManager.Find(model.UserName, model.Password);

            // user will be null if the password or user name is bad
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");

                return View(model);
            }
            else
            {
                // successful login, set up their cookies and send them on their way
                var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, identity);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index");
            }
        }

        public ActionResult LogOff()
        {
            var authMgr = Request.GetOwinContext().Authentication;

            authMgr.SignOut("ApplicationCookie");

            return RedirectToAction("Index", "Home");
        }

        public ActionResult NewInventory()
        {
            return View();
        }

        public ActionResult UsedInventory()
        {
            return View();
        }

        public ActionResult Contact(string id)
        {
            var model = new ContactUs();
            model.Message = id;

            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(ContactUs model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var repo = RepositoryFactory.Create();
            repo.CreateContactUs(model);

            return RedirectToAction("Index");
        }

        public ActionResult Specials()
        {
            return View();
        }
    }
}