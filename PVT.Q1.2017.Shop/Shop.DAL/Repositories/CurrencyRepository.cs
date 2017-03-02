namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The currency repository
    /// </summary>
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public CurrencyRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}