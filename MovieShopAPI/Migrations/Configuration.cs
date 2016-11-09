using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieShopAPI.Models;

namespace MovieShopAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MovieShopAPI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MovieShopAPI.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            string username = "restapi";
            string password = "DANKMovieShop2016";
            string roleName = "Admin";

            //Create user to store.
            var user = new ApplicationUser() { UserName = username };
            var role = new IdentityRole(roleName);

            //Create role.
            var result = roleManager.Create(role);

            if (result.Succeeded)
            {
                //Create user.
                result = userManager.Create(user, password);

                if (result.Succeeded)
                {
                    result = userManager.AddToRole(userManager.FindByName(username).Id, roleName);

                    if (!result.Succeeded)
                    {
                        foreach (string resultError in result.Errors)
                        {
                            throw new Exception($"Possible Error: {resultError}");
                        }
                    }
                }
                else
                {
                    foreach (string resultError in result.Errors)
                    {
                        throw new Exception($"Possible Error: {resultError}");
                    }
                }
            }
            else
            {
                foreach (string resultError in result.Errors)
                {
                    throw new Exception($"Possible Error: {resultError}");
                }
            }

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
        }
    }
}
