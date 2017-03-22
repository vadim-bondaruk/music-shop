namespace Shop.Security.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PVT.Q1._2017.Shop.Security.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PVT.Q1._2017.Shop.Security.SecurityContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PVT.Q1._2017.Shop.Security.SecurityContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "Founder"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                var user = new AppUser
                {
                    UserName = "Founder",
                    IsConfirmed = true,
                    EmailConfirmed = true,
                    Email = "test@test.by"
                };

                manager.Create(user, "!123456!");
                manager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
