using Shop.DAL.Infrastruture;

namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;

    /// <summary>
    /// The currency repository
    /// </summary>
    public class CurrencyBaseRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyBaseRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public CurrencyBaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}