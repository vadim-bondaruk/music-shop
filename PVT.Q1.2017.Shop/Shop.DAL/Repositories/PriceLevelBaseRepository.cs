using Shop.DAL.Infrastruture;

namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;

    /// <summary>
    /// The price level repository.
    /// </summary>
    public class PriceLevelBaseRepository : BaseRepository<PriceLevel>, IPriceLevelRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PriceLevelBaseRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public PriceLevelBaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}