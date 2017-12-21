using CarDealership.Data;
using CarDealership.Models.Identity;
using CarDealership.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    [Authorize(Roles ="admin,sales")]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> ChangePassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var ctx = new CarDealershipEntities();
            UserStore<User> store = new UserStore<User>(ctx);
            UserManager<User> userMgr = new UserManager<User>(store);

            var userId = User.Identity.GetUserId();

            var loggedUser = userMgr.FindById(userId);

            if(model.UserName != loggedUser.UserName)
            {
                return View(model);
            }

            string hashedNewPassword = userMgr.PasswordHasher.HashPassword(model.ConfirmPassword);
            await store.SetPasswordHashAsync(loggedUser, hashedNewPassword);
            await store.UpdateAsync(loggedUser);

            return RedirectToAction("SalesIndex", "Sales");
        }
    }
}