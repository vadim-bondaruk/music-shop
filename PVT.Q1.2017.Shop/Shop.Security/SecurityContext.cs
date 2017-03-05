namespace PVT.Q1._2017.Shop.Security
{
    using System;
    using System.Data.Entity;
    using Identity;

    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// Context for user data
    /// </summary>
    public class SecurityContext : IdentityDbContext<AppUser>, IDisposable
    {
        /// <summary>
        /// C'tor
        /// </summary>
        public SecurityContext() : base("SecurityDbConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SecurityContext>());
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Create instance of the Context
        /// </summary>
        /// <returns></returns>
        public static SecurityContext Create()
        {
            return new SecurityContext();
        }
    }
}
