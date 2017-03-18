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
        /// Returns the album price with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The album price id.</param>
        /// <returns>
        /// The album price with the specified <paramref name="id"/> or <b>null</b> if album price doesn't exist.
        /// </returns>
        AlbumPrice GetAlbumPrice(int id);
    }
}