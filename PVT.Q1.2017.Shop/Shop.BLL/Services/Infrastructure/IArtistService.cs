namespace Shop.BLL.Services.Infrastructure
{
    using System.Collections.Generic;

    using Shop.Common.ViewModels;

    /// <summary>
    ///     The artist service.
    /// </summary>
    public interface IArtistService
    {
        /// <summary>
        ///     Returns all registered albums for the specified artist using the specified currency and price level for album
        ///     price.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <param name="currencyCode">
        ///     The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All registered albums with price for the specified artist.
        /// </returns>
        ArtistAlbumsListViewModel GetAlbumsList(int artistId, int? currencyCode = null, int? priceLevel = null);

        /// <summary>
        ///     Returns all albums for the specified artist without price.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <returns>
        ///     All albums for the specified artist without price.
        /// </returns>
        ArtistAlbumsListViewModel GetAlbumsWithoutPrice(int artistId);

        /// <summary>
        ///     Returns all albums for the specified artist with price specified using the specified currency and price level for
        ///     album price.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <param name="currencyCode">
        ///     The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All albums for the specified artist without price specified.
        /// </returns>
        ArtistAlbumsListViewModel GetAlbumsWithPrice(int artistId, int? currencyCode = null, int? priceLevel = null);

        /// <summary>
        ///     Returns the artist details.
        /// </summary>
        /// <param name="id">The artist id.</param>
        /// <returns>
        ///     The information about artist with the specified <paramref name="id" /> or <b>null</b> if artist doesn't exist.
        /// </returns>
        ArtistDetailsViewModel GetArtistDetails(int id);

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// </returns>
        ArtistDetailsViewModel GetArtistDetailsViewModel(int id);

        /// <summary>
        ///     Returns all registered artists.
        /// </summary>
        /// <returns>
        ///     All registered artists.
        /// </returns>
        ICollection<ArtistViewModel> GetArtistsList();

        /// <summary>
        ///     Returns all registered tracks for the specified artist using the specified currency and price level for track
        ///     price.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <param name="currencyCode">
        ///     The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All registered tracks with price for the specified artist.
        /// </returns>
        ArtistTracksListViewModel GetTracksList(int artistId, int? currencyCode = null, int? priceLevel = null);

        /// <summary>
        ///     Returns all tracks for the specified artist without price.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <returns>
        ///     All tracks for the specified artist without price.
        /// </returns>
        ArtistTracksListViewModel GetTracksWithoutPrice(int artistId);

        /// <summary>
        ///     Returns all tracks for the specified artist with price specified using the specified currency and price level for
        ///     track price.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <param name="currencyCode">
        ///     The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All tracks for the specified artist without price specified.
        /// </returns>
        ArtistTracksListViewModel GetTracksWithPrice(int artistId, int? currencyCode = null, int? priceLevel = null);
    }
}