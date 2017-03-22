namespace Shop.BLL.Services.Infrastructure
{
    using System.Collections.Generic;

    using Shop.Common.Models;
    using Shop.Common.Models.ViewModels;

    /// <summary>
    ///     The album service.
    /// </summary>
    public interface IAlbumService
    {
        /// <summary>
        ///     Returns the album with the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>
        ///     The album with the specified <paramref name="id" /> or <b>null</b> if album doesn't exist.
        /// </returns>
        AlbumViewModel GetAlbum(int id);

        /// <summary>
        ///     Returns all album prices for the specified price level.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="priceLevelId">The price level id.</param>
        /// <returns>
        ///     All album prices for the specified price level.
        /// </returns>
        ICollection<AlbumPrice> GetAlbumPrices(int albumId, int priceLevelId);

        /// <summary>
        ///     Returns all album prices.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <returns>All album prices.</returns>
        ICollection<AlbumPrice> GetAlbumPrices(int albumId);

        /// <summary>
        ///     Returns all registered albums.
        /// </summary>
        /// <returns>
        ///     All registered albums.
        /// </returns>
        ICollection<AlbumViewModel> GetAlbumsList();

        /// <summary>
        ///     Returns all albums without price configured.
        /// </summary>
        /// <returns>
        ///     All albums without price configured.
        /// </returns>
        ICollection<Album> GetAlbumsWithoutPriceConfigured();

        /// <summary>
        ///     Returns all albums with price specified.
        /// </summary>
        /// <returns>
        ///     All albums without price specified.
        /// </returns>
        ICollection<Album> GetAlbumsWithPriceConfigured();

        /// <summary>
        ///     Returns all registered tracks for the specified album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <returns>
        ///     All registered tracks for the specified album.
        /// </returns>
        ICollection<Track> GetTracksList(int albumId);

        /// <summary>
        ///     Returns all tracks for the specified album without price configured.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <returns>
        ///     All tracks for the specified album without price configured.
        /// </returns>
        ICollection<Track> GetTracksWithoutPriceConfigured(int albumId);

        /// <summary>
        ///     Returns all tracks for the specified album with price specified.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <returns>
        ///     All tracks for the specified album without price specified.
        /// </returns>
        ICollection<Track> GetTracksWithPriceConfigured(int albumId);

        /// <summary>
        /// </summary>
        /// <param name="viewModel">
        /// The view model.
        /// </param>
        /// <returns>
        /// </returns>
        int SaveNewAlbum(AlbumManageViewModel viewModel);
    }
}