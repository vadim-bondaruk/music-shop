namespace Shop.BLL.Services
{
    using System.Linq;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;

    /// <summary>
    /// The album price service.
    /// </summary>
    public class AlbumPriceService : BaseService, IAlbumPriceService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumPriceService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The repository factory.
        /// </param>
        public AlbumPriceService(IRepositoryFactory factory) : base(factory)
        {
        }

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
            using (var repository = this.Factory.GetAlbumPriceRepository())
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
        /// Returns the album price with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The album price id.</param>
        /// <returns>
        /// The album price with the specified <paramref name="id"/> or <b>null</b> if album price doesn't exist.
        /// </returns>
        public AlbumPrice GetAlbumPriceInfo(int id)
        {
            using (var repository = this.Factory.GetAlbumPriceRepository())
            {
                return repository.GetById(id, p => p.Album, p => p.Currency, p => p.PriceLevel);
            }
        }
    }
}