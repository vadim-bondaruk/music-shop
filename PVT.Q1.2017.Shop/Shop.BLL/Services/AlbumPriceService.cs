namespace Shop.BLL.Services
{
    using Common.Models;
    using Infrastructure;

    /// <summary>
    /// The album price service.
    /// </summary>
    public class AlbumPriceService : BaseService, IAlbumPriceService
    {
        #region Constructors

        public AlbumPriceService(IRepositoryFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumPriceService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>


        #endregion //Constructors

        #region IAlbumPriceService Members

        /// <summary>
        /// Returns the album price in the specified <paramref name="currency"/> for the specified  <paramref name="priceLevel"/>.
        /// </summary>
        /// <param name="album"></param>
        /// <param name="priceLevel">
        ///     The price level.
        /// </param>
        /// <param name="currency">
        ///     The currency.
        /// </param>
        /// <returns>
        /// The album price in the specified <paramref name="currency"/> for the specified  <paramref name="priceLevel"/> or <b>null</b>.
        /// </returns>
        public AlbumPrice GeAlbumPrice(Album album, PriceLevel priceLevel, Currency currency)
        {
            using (var repository = _factory.CreateNeededRepository)
            {
                return repository.GetAll(
                                         p => p.AlbumId == album.Id &&
                                              p.PriceLevelId == priceLevel.Id &&
                                              p.CurrencyId == currency.Id,
                                         p => p.Album,
                                         p => p.Currency,
                                         p => p.PriceLevel).FirstOrDefault();
            }
        }

        /// <summary>
        /// Adds the <paramref name="album"/> price.
        /// </summary>
        /// <param name="album">
        /// The album.
        /// </param>
        /// <param name="price">
        ///     The <paramref name="album"/> price.
        /// </param>
        /// <param name="currency">
        ///     The currency.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level.
        /// </param>
        public void AddAlbumPrice(Album album, decimal price, Currency currency, PriceLevel priceLevel)
        {
            ValidatorHelper.CheckAlbumForNull(album);
            ValidatorHelper.CheckCurrencyForNull(currency);
            ValidatorHelper.CheckPriceLevelForNull(priceLevel);

            var albumPrice = new AlbumPrice
            {
                Price = price,
                PriceLevelId = priceLevel.Id,
                CurrencyId = currency.Id,
                AlbumId = album.Id
            };

            this.Register(albumPrice);
        }

        /// <summary>
        /// Returns the album price with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The album price id.</param>
        /// <returns>
        /// The album price with the specified <paramref name="id"/> or <b>null</b> if album price doesn't exist.
        /// </returns>
        public AlbumPrice GetAlbumPriceInfo(int id)
        {
            using (var repository = this.CreateRepository())
            {
                return repository.GetById(id, p => p.Album, p => p.Currency, p => p.PriceLevel);
            }
        }

        #endregion //IAlbumPriceService Members
    }
}