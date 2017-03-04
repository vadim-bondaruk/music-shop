namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;
    using Services.Infrastructure;

    /// <summary>
    /// The track price validator.
    /// </summary>
    public class TrackPriceValidator : PriceValidator<TrackPrice>
    {
        #region Fields

        /// <summary>
        /// The album service.
        /// </summary>
        private readonly ITrackService _trackService;

        #endregion //Fields

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackPriceValidator"/> class.
        /// </summary>
        /// <param name="trackService">
        /// The track price.
        /// </param>
        /// <param name="currencyService">
        /// The currency service.
        /// </param>
        /// <param name="priceLevelService">
        /// The price level service.
        /// </param>
        public TrackPriceValidator(ITrackService trackService, ICurrencyService currencyService, IPriceLevelService priceLevelService)
            : base(currencyService, priceLevelService)
        {
            if (trackService == null)
            {
                throw new ArgumentNullException(nameof(trackService));
            }

            this._trackService = trackService;
        }

        #region IValidator<TrackPrice> Members

        /// <summary>
        /// Validates the specified <paramref name="price"/>.
        /// </summary>
        /// <param name="price">
        /// The album price to validate.
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
        public override void Validate(TrackPrice price)
        {
            base.Validate(price);

            if (!this._trackService.IsRegistered(price.TrackId))
            {
                throw new EntityNotFoundException($"Track with id='{ price.Track }' doesn't exist.");
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="price"/> is valid.
        /// </summary>
        /// <param name="price">
        /// The album price to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="price"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public override bool IsValid(TrackPrice price)
        {
            if (!base.IsValid(price))
            {
                return false;
            }

            if (!this._trackService.IsRegistered(price.TrackId))
            {
                return false;
            }

            return true;
        }

        #endregion //IValidator<TrackPrice> Members
    }
}