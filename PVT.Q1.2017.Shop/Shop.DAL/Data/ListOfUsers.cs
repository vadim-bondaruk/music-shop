namespace Shop.DAL.Data
{
    using System.Collections.Generic;
    using Shop.Common.Enums;
    using Common.Models;

    /// <summary>
    /// Demo storage for entities of users
    /// </summary>
    public class ListOfUsers
    {
        /// <summary>
        /// Contain short list of users (demo!)
        /// </summary>
        public ListOfUsers()
        {
            this.Users = new List<User>();
            this.Users.Add(new User
            {
                Id = 0,
                FirstName = "John",
                LastName = "Gates",
                Login = "kasper",
                UserRole = UserRoles.User,
                Email = "KasperKasper@gmail.com",
            });

            this.Users.Add(new User
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "McNeal",
                Login = "alisochka",
                UserRole = UserRoles.User,
                Email = "alice1@gmail.com",
            });
        }

        /// <summary>
        /// list of users
        /// </summary>
        public List<User> Users { get; set; }
    }
}
