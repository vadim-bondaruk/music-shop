//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Track.cs" company="PVT.Q1.2017">
//    PVT.Q1.2017
//  </copyright>
//  <summary>
//    The track.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace Shop.DAL.Repositories
{
    using System.Data.Entity;

    using Shop.Common.Models;
    using Shop.DAL.Infrastruture;

    /// <summary>
    ///     The currency rate repository.
    /// </summary>
    public class CurrencyRateBaseRepository : BaseRepository<CurrencyRate>, ICurrencyRateRepository
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CurrencyRateBaseRepository" /> class.
        /// </summary>
        /// <param name="dbContext">
        ///     The db context.
        /// </param>
        public CurrencyRateBaseRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion //Constructors

        #region Protected Methods

        /// <summary>
        ///     Adds the specified <paramref name="currencyRate" /> into Db.
        /// </summary>
        /// <param name="currencyRate">
        ///     The currency rate to add.
        /// </param>
        protected override void Add(CurrencyRate currencyRate)
        {
            EntityState currencyEntryState;
            EntityState targetCurrencyEntryState;

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The CurrencyRateBaseRepository should be SOLID, should only add information about currency rate! Not about currencies!
            this.DetachNavigationProperty(currencyRate.Currency, out currencyEntryState);
            this.DetachNavigationProperty(currencyRate.TargetCurrency, out targetCurrencyEntryState);

            currencyRate.Currency = null;
            currencyRate.TargetCurrency = null;

            // adding the currency rate into Db.
            base.Add(currencyRate);
        }

        #endregion //Protected Methods
    }
}