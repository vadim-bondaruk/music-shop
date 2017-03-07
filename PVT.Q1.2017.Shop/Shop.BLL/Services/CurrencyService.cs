namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;

    /// <summary>
    /// The currency service.
    /// </summary>
    public class CurrencyService : BaseService, ICurrencyService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The repository factory.
        /// </param>
        public CurrencyService(IRepositoryFactory factory) : base(factory)
        {
        }

        #endregion //Constructors

        #region ICurrencyService Members

        /// <summary>
        /// Returns the currency with the specified <paramref name="code"/>.
        /// </summary>
        /// <param name="code">
        /// The currency numeric code (see ISO 421).
        /// </param>
        /// <returns>
        /// The currency with the specified <paramref name="code"/> or <b>null</b> if currency doesn't exist.
        /// </returns>
        public Currency GetCurrencyByCode(int code)
        {
            using (var repositry = this.Factory.CreateCurrencyRepository())
            {
                return repositry.GetAll(c => c.Code == code).FirstOrDefault();
            }
        }

        /// <summary>
        /// Returns the currency with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        /// The currency name (see ISO 421).
        /// </param>
        /// <returns>
        /// The currency with the specified <paramref name="name"/> or <b>null</b> if currency doesn't exist.
        /// </returns>
        public Currency GetCurrencyByName(string name)
        {
            using (var repositry = this.Factory.CreateCurrencyRepository())
            {
                return repositry.GetAll(c => c.ShortName.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Returns all registered currencies.
        /// </summary>
        /// <returns>
        /// All registered currencies.
        /// </returns>
        public ICollection<Currency> GetCurrenciesList()
        {
            using (var repositry = this.Factory.CreateCurrencyRepository())
            {
                return repositry.GetAll();
            }
        }

        /// <summary>
        /// Determines whether a currency with the specified <paramref name="name"/> exists.
        /// </summary>
        /// <param name="name">
        /// The currency name (see ISO 421).
        /// </param>
        /// <returns>
        /// <b>true</b> if a currency with the specified <paramref name="name"/> exists;
        /// otherwise <b>false</b>.
        /// </returns>
        public bool CurrencyExists(string name)
        {
            using (var repositry = this.Factory.CreateCurrencyRepository())
            {
                return repositry.GetAll(c => c.ShortName.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase)).Any();
            }
        }

        /// <summary>
        /// Determines whether a currency with the specified numeric <paramref name="code"/> exists.
        /// </summary>
        /// <param name="code">
        /// The currency numeric code (see ISO 421).
        /// </param>
        /// <returns>
        /// <b>true</b> if a currency with the specified <paramref name="code"/> exists;
        /// otherwise <b>false</b>.
        /// </returns>
        public bool CurrencyExists(int code)
        {
            using (var repositry = this.Factory.CreateCurrencyRepository())
            {
                return repositry.GetAll(c => c.Code == code).Any();
            }
        }

        /// <summary>
        /// Determines whether the specified <paramref name="currency"/> already exists.
        /// </summary>
        /// <param name="currency">
        /// The currency.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="currency"/> already exists;
        /// otherwise <b>false</b>.
        /// </returns>
        public bool CurrencyExists(Currency currency)
        {
            if (currency == null)
            {
                return false;
            }

            using (var repositry = this.Factory.CreateCurrencyRepository())
            {
                return repositry.GetAll(c => c.Id == currency.Id ||
                                             c.ShortName.Equals(currency.ShortName.Trim(), StringComparison.OrdinalIgnoreCase)).Any();
            }
        }

        /// <summary>
        /// Returns the currency with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The currency id.</param>
        /// <returns>
        /// The currency with the specified <paramref name="id"/> or <b>null</b> if currency doesn't exist.
        /// </returns>
        public Currency GetCurrencyInfo(int id)
        {
            using (var repositry = this.Factory.CreateCurrencyRepository())
            {
                return repositry.GetById(id);
            }
        }

        #endregion //ICurrencyService Members
    }
}