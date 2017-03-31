namespace Shop.BLL.Services.Infrastructure
{
    using System.Collections.Generic;

    using Shop.Common.Models.ViewModels;

    /// <summary>
    ///     The album service.
    /// </summary>
    public interface IAlbumService
    {
        /// <summary>
        ///     Returns the album details using the specified currency and price level for album price.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <param name="currencyCode">
        ///     The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     The information about album with the specified <paramref name="id" /> or <b>null</b> if album doesn't exist.
        /// </returns>
        AlbumDetailsViewModel GetAlbumDetails(int id, int? currencyCode = null, int? priceLevel = null);

        /// <summary>
        ///     Returns all registered albums using the specified currency and price level for album price.
        /// </summary>
        /// <param name="currencyCode">
        ///     The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All registered albums.
        /// </returns>
        ICollection<AlbumViewModel> GetAlbumsList(int? currencyCode = null, int? priceLevel = null);

        /// <summary>
        ///     Returns all albums without price configured.
        /// </summary>
        /// <returns>
        ///     All albums without price configured.
        /// </returns>
        ICollection<AlbumViewModel> GetAlbumsWithoutPrice();

        /// <summary>
        ///     Returns all albums with price specified using the specified currency and price level for album price.
        /// </summary>
        /// <param name="currencyCode">
        ///     The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All albums with price specified.
        /// </returns>
        ICollection<AlbumViewModel> GetAlbumsWithPrice(int? currencyCode = null, int? priceLevel = null);

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        ICollection<AlbumDetailsViewModel> GetAllViewModels();

        /// <summary>
        ///     Returns all registered tracks for the specified album using the specified currency and price level for track price.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="currencyCode">
        ///     The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All registered tracks with price for the specified album.
        /// </returns>
        AlbumTracksListViewModel GetTracksList(int albumId, int? currencyCode = null, int? priceLevel = null);

        /// <summary>
        ///     Returns all tracks for the specified album without price.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <returns>
        ///     All tracks for the specified album without price.
        /// </returns>
        AlbumTracksListViewModel GetTracksWithoutPrice(int albumId);

        /// <summary>
        ///     Returns all tracks for the specified album with price specified using the specified currency and price level for
        ///     track price.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="currencyCode">
        ///     The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All tracks for the specified album without price specified.
        /// </returns>
        AlbumTracksListViewModel GetTracksWithPrice(int albumId, int? currencyCode = null, int? priceLevel = null);
    }
}