namespace Shop.BLL.Services.Infrastructure
{
    using Common.Models;

    /// <summary>
    /// </summary>
    public interface IAlbumPriceService
    {
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
        AlbumPrice GeAlbumPrice(Album album, PriceLevel priceLevel, Currency currency);

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
        void AddAlbumPrice(Album album, decimal price, Currency currency, PriceLevel priceLevel);

        /// <summary>
        /// Returns the album price with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The album price id.</param>
        /// <returns>
        /// The album price with the specified <paramref name="id"/> or <b>null</b> if album price doesn't exist.
        /// </returns>
        AlbumPrice GetAlbumPriceInfo(int id);
    }
}