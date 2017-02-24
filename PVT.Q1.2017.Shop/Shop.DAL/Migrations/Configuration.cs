// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Configuration.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Defines the Configuration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.DAL.Migrations
{
    #region

    using System.Data.Entity.Migrations;

    using Shop.DAL.Context;

    #endregion

    /// <summary>
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<ShopContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected override void Seed(ShopContext context)
        {
            // This method will be called after migrating to the latest version.
        }
    }
}