namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The currency rate repository.
    /// </summary>
    public class CurrencyRateRepository : Repository<CurrencyRate>, ICurrencyRateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyRateRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public CurrencyRateRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}