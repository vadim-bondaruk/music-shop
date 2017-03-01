namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using Common.Models;

    /// <summary>
    /// The currency rate repository.
    /// </summary>
    public class CurrencyRateRepository : Repository<CurrencyRate>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyRateRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public CurrencyRateRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors

        #region Protected Methods

        /// <summary>
        /// Loads additional references.
        /// </summary>
        /// <param name="queryResult">
        /// The query result.
        /// </param>
        /// <returns>
        /// </returns>
        protected override IQueryable<CurrencyRate> LoadAdditionalInfo(IQueryable<CurrencyRate> queryResult)
        {
            return base.LoadAdditionalInfo(queryResult).Include(r => r.Currency).Include(r => r.TargetCurrency);
        }

        #endregion //Protected Methods
    }
}