using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using CarDealership.Data;
using CarDealership.Models;
using CarDealership.Models.Identity;

namespace CarDealership.UI.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new CarDealershipEntities());

            app.CreatePerOwinContext<UserManager<User>>((options, context) =>
                new UserManager<User>(
                    new UserStore<User>(context.Get<CarDealershipEntities>())));

            app.CreatePerOwinContext<RoleManager<IdentityRole>>((options, context) =>
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context.Get<CarDealershipEntities>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });
        }
    }
}