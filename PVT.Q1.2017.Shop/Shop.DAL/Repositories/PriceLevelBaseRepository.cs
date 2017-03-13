namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The price level repository.
    /// </summary>
    public class PriceLevelBaseRepository : BaseRepository<PriceLevel>, IPriceLevelRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PriceLevelBaseRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public PriceLevelBaseRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}