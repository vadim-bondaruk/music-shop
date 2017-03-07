namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using DAL.Repositories.Infrastruture;
    using Infrastructure;
    using Shop.Infrastructure;
    using Shop.Infrastructure.Validators;
    using Validators;

    /// <summary>
    /// The currency service.
    /// </summary>
    public class CurrencyService : Service<ICurrencyRepository, Currency>, ICurrencyService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>
        public CurrencyService(IFactory factory, IValidator<Currency> validator) : base(factory, validator)
        {
        }

        #endregion //Constructors

        #region Public Methods

        /// <summary>
        /// Registers the specified <paramref name="currency"/> in the system.
        /// </summary>
        /// <param name="currency">
        /// The currency.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// When the <paramref name="currency"/> with such numeric code already exists.
        /// </exception>
        public override void Register(Currency currency)
        {
            Validator.Validate(currency);

            if (this.CurrencyExists(currency.Code))
            {
                throw new InvalidOperationException($"The currency with the numeric code '{ currency.Code }' already exists");
            }

            if (this.CurrencyExists(currency.Name))
            {
                throw new InvalidOperationException($"The currency with the name '{ currency.Name }' already exists");
            }

            currency.Name = currency.Name.ToUpper();
            base.Register(currency);
        }

        #endregion //Public Methods

        #region ICurrencyService Members

        /// <summary>
        /// Adds the currency into Db.
        /// </summary>
        /// <param name="name">
        /// The currency name (see ISO 421).
        /// </param>
        /// <param name="code">
        /// The currency code number (see ISO 421).
        /// </param>
        /// <param name="fullName">
        /// The full name.
        /// </param>
        public void AddCurrency(string name, int code, string fullName = null)
        {
            var currency = new Currency
            {
                Name = name,
                Code = code,
                FullName = fullName
            };
            this.Register(currency);
        }

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
            if (!CurrencyValidator.IsCurrencyCodeValid(code))
            {
                return null;
            }

            using (var repositry = this.CreateRepository())
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
            if (!CurrencyValidator.IsCurrencyNameValid(name))
            {
                return null;
            }

            using (var repositry = this.CreateRepository())
            {
                return repositry.GetAll(c => c.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
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
            using (var repositry = this.CreateRepository())
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
            if (!CurrencyValidator.IsCurrencyNameValid(name))
            {
                return false;
            }

            using (var repositry = this.CreateRepository())
            {
                return repositry.GetAll(c => c.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase)).Any();
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
            if (!CurrencyValidator.IsCurrencyCodeValid(code))
            {
                return false;
            }

            using (var repositry = this.CreateRepository())
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

            if (!CurrencyValidator.IsCurrencyCodeValid(currency.Code))
            {
                return false;
            }

            if (!CurrencyValidator.IsCurrencyNameValid(currency.Name))
            {
                return false;
            }

            using (var repositry = this.CreateRepository())
            {
                return repositry.GetAll(c => c.Id == currency.Id ||
                                             c.Name.Equals(currency.Name.Trim(), StringComparison.OrdinalIgnoreCase)).Any();
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
            using (var repositry = this.CreateRepository())
            {
                return repositry.GetById(id);
            }
        }

        #endregion //ICurrencyService Members
    }
}