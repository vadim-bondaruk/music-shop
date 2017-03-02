namespace Shop.DAL.Context
{
    using System;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity;
    using System.Reflection;
    using Migrations;

    /// <summary>
    /// Music shop Db
    /// </summary>
    public class ShopContext : DbContext 
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopContext"/> class.
        /// </summary>
        public ShopContext() : this("ShopConnection")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopContext"/> class.
        /// </summary>
        /// <param name="connectionStringOrName">
        /// The connection string or Db name.
        /// </param>
        public ShopContext(string connectionStringOrName) : base(connectionStringOrName)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShopContext, Configuration>());

            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }

        #endregion //Constructors

        #region Protected Methods

        /// <summary>
        /// The Db configuration.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
               .Where(type => !string.IsNullOrEmpty(type.Namespace))
               .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                   && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var configurationInstance in typesToRegister.Select(Activator.CreateInstance))
            {
                modelBuilder.Configurations.Add((dynamic)configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        #endregion //Protected Methods
    }
}