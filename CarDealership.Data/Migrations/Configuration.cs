namespace CarDealership.Data.Migrations
{
    using CarDealership.Models.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarDealership.Data.CarDealershipEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CarDealership.Data.CarDealershipEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // Load the user and role managers with our custom models
            var userMgr = new UserManager<User>(new UserStore<User>(context));
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleMgr.RoleExists("admin"))
            {
                roleMgr.Create(new IdentityRole() { Name = "admin" });
            }

            if (!roleMgr.RoleExists("sales"))
            {
                roleMgr.Create(new IdentityRole() { Name = "sales" });
            }

            var salesMan = new User()
            {
                UserName = "MarkJohnson",
                FirstName = "Mark",
                LastName = "Johnson",
                Email = "mejo@gmail.com",
                EmailConfirmed = true
            };

            var adminPerson = new User()
            {
                UserName = "LindseyParlow",
                FirstName = "Lindsey",
                LastName = "Parlow",
                Email = "lindsey.parlow@gmail.com",
                EmailConfirmed = true
            };

            if (!userMgr.Users.Any(u => u.UserName == "MarkJohnson"))
            {
                userMgr.Create(salesMan, "testing123");

                context.SaveChanges();

                userMgr.AddToRole(salesMan.Id, "sales");
            }

            if (!userMgr.Users.Any(u => u.UserName == "LindseyParlow"))
            {
                userMgr.Create(adminPerson, "testing123");

                context.SaveChanges();

                userMgr.AddToRole(adminPerson.Id, "admin");
            }

            context.SaveChanges();
        }
    }
}
