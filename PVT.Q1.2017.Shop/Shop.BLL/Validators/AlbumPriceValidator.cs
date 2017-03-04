namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;
    using Services.Infrastructure;

    /// <summary>
    /// The album price validator.
    /// </summary>
    public class AlbumPriceValidator : PriceValidator<AlbumPrice>
    {
        #region Fields

        /// <summary>
        /// The album service.
        /// </summary>
        private readonly IAlbumService _albumService;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumPriceValidator"/> class.
        /// </summary>
        /// <param name="albumService">
        ///     The album service.
        /// </param>
        /// <param name="currencyService">
        ///     The currency service.
        /// </param>
        /// <param name="priceLevelService">
        ///     The price level service.
        /// </param>
        public AlbumPriceValidator(IAlbumService albumService, ICurrencyService currencyService, IPriceLevelService priceLevelService)
            : base(currencyService, priceLevelService)
        {
            if (albumService == null)
            {
                throw new ArgumentNullException(nameof(albumService));
            }

            this._albumService = albumService;
        }

        #endregion //Constructors

        #region IValidator<AlbumPrice> Members

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
        public override void Validate(AlbumPrice price)
        {
            base.Validate(price);

            if (!this._albumService.IsRegistered(price.AlbumId))
            {
                throw new EntityNotFoundException($"Album with id='{ price.AlbumId }' doesn't exist.");
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
        public override bool IsValid(AlbumPrice price)
        {
            if (!base.IsValid(price))
            {
                return false;
            }

            if (!this._albumService.IsRegistered(price.AlbumId))
            {
                return false;
            }

            return true;
        }

        #endregion //IValidator<AlbumPrice> Members
    }
}