namespace Shop.DAL.Context
{
    using System;
    using System.Data.Entity;
    using Shop.DAL.Migrations;
    using Shop.Common.Models;

    /// <summary>
    /// 
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public UserContext() : base ("UserConnection")
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
