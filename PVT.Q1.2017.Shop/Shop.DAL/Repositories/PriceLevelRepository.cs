namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The price level repository.
    /// </summary>
    public class PriceLevelRepository : Repository<PriceLevel>, IPriceLevelRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PriceLevelRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public PriceLevelRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}