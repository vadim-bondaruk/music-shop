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
        /// Adds the specified <paramref name="currencyRate"/> into Db.
        /// </summary>
        /// <param name="currencyRate">
        /// The currency rate to add.
        /// </param>
        protected override void Add(CurrencyRate currencyRate)
        {
            EntityState currencyEntryState;
            EntityState targetCurrencyEntryState;

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The CurrencyRateRepository should be SOLID, should only add information about currency rate! Not about currencies!
            this.DetachNavigationProperty(currencyRate.Currency, out currencyEntryState);
            this.DetachNavigationProperty(currencyRate.TargetCurrency, out targetCurrencyEntryState);

            // adding the currency rate into Db.
            base.Add(currencyRate);

            // restoring navigation properties states
            this.RestoreNavigationPropertyState(currencyRate.Currency, currencyEntryState);
            this.RestoreNavigationPropertyState(currencyRate.TargetCurrency, targetCurrencyEntryState);
        }

        #endregion //Protected Methods
    }
}