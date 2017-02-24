namespace Shop.DAL.Context
{
    using System;
    using System.Data.Entity;
    using Shop.Common.Models;

    /// <summary>
    /// 
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public UserContext() : base("UserConnection")
        {
        }

        /// <summary>
        ///  represents the collection of all users in the context
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Initializes configurations for this model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
