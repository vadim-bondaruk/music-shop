namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;
    using Infrastructure.Validators;
    using Services.Infrastructure;

    /// <summary>
    /// The common price validator.
    /// </summary>
    /// <typeparam name="TPrice">
    /// The price model type derived from <see cref="BasePriceEntity"/>
    /// </typeparam>
    public abstract class PriceValidator<TPrice> : IValidator<TPrice> where TPrice : BasePriceEntity
    {
        #region Fields

        /// <summary>
        /// The track service.
        /// </summary>
        private readonly ICurrencyService _currencyService;

        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IPriceLevelService _priceLevelService;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PriceValidator{TPrice}"/> class.
        /// </summary>
        /// <param name="currencyService">
        /// The currency service.
        /// </param>
        /// <param name="priceLevelService">
        /// The price level service.
        /// </param>
        protected PriceValidator(ICurrencyService currencyService, IPriceLevelService priceLevelService)
        {
            if (currencyService == null)
            {
                throw new ArgumentNullException(nameof(currencyService));
            }

            if (priceLevelService == null)
            {
                throw new ArgumentNullException(nameof(priceLevelService));
            }

            this._currencyService = currencyService;
            this._priceLevelService = priceLevelService;
        }

        #endregion //Constructors

        #region Public Methods

        /// <summary>
        /// Determines whether the specified <paramref name="price"/> is valid.
        /// </summary>
        /// <param name="price">
        /// The price amount.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="price"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public static bool IsPriceValid(decimal price)
        {
            return price >= 0m;
        }

        #endregion //Public Methods

        #region IValidator<TPrice> Members

        /// <summary>
        /// Validates the specified <paramref name="price"/>.
        /// </summary>
        /// <param name="price">
        /// The price to validate.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When the <paramref name="price"/> is null.
        /// </exception>
        /// <exception cref="InvalidEntityException">
        /// When the <paramref name="price"/> amount is invalid.
        /// </exception>
        /// <exception cref="EntityNotFoundException">
        /// When the currency or price level doesn't exist.
        /// </exception>
        public virtual void Validate(TPrice price)
        {
            if (price == null)
            {
                throw new ArgumentNullException(nameof(price));
            }

            if (!IsPriceValid(price.Price))
            {
                throw new InvalidEntityException("Invalid price specified");
            }

            if (!this._currencyService.IsRegistered(price.CurrencyId))
            {
                throw new EntityNotFoundException($"Currency with id='{ price.CurrencyId }' doesn't exist.");
            }

            if (!this._priceLevelService.IsRegistered(price.PriceLevelId))
            {
                throw new EntityNotFoundException($"Price level with id='{ price.PriceLevelId }' doesn't exist.");
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="price"/> is valid.
        /// </summary>
        /// <param name="price">
        /// The price to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="price"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public virtual bool IsValid(TPrice price)
        {
            if (price == null)
            {
                return false;
            }

            if (!IsPriceValid(price.Price))
            {
                return false;
            }

            if (!this._currencyService.IsRegistered(price.CurrencyId))
            {
                return false;
            }

            if (!this._priceLevelService.IsRegistered(price.PriceLevelId))
            {
                return false;
            }

            return true;
        }

        #endregion //IValidator<TPrice> Members
    }
}