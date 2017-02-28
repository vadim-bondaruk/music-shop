namespace Shop.DAL.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using Shop.Common.Models;
    using Shop.DAL.Context;
    using Shop.Infrastructure.Enums;

    /// <summary>
    /// Adds data to the context for seeding. A storage for entities of users
    /// </summary>
    public class UserInitializer : DropCreateDatabaseIfModelChanges<UserContext>
    {
        /// <summary>
        /// adds data 
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(UserContext context)
        {
            var _users = new List<User>
            {
                new User
                {
                FirstName = "John",
                LastName = "Gates",
                Login = "kasper",
                Sex = "M",
                UserRole = UserRoles.User,
                Email = "KasperKasper@gmail.com",
                Password = "СлаваКПСС!11"
                },

                new User
                {
                FirstName = "Alice",
                LastName = "McNeal",
                Login = "alisochka",
                Sex = "W",
                UserRole = UserRoles.User,
                Email = "alice1@gmail.com",
                Password = "100VK500"
                },

                new User
                {
                FirstName = "Artur",
                LastName = "Li",
                Login = "arturio",
                Sex = "M",
                UserRole = UserRoles.Admin,
                Email = "artII@mail.ru",
                Password = "nobodyKnowMyP-d1"
                },

                new User
                {
                FirstName = "Abziz",
                LastName = "Anand",
                Login = "OilMagnat",
                Sex = "W",
                UserRole = UserRoles.VIPUser,
                Email = "blablabla@gmail.com",
                Password = "12345"
                }
            };

            _users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }
}
