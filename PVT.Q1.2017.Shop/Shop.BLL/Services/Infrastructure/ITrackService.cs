namespace Shop.BLL.Services.Infrastructure
{
    using System.Collections.Generic;
    using Common.ViewModels;

    /// <summary>
    /// The track service
    /// </summary>
    public interface ITrackService
    {
        /// <summary>
        /// Returns the track details using the specified currency and price level for track price.
        /// </summary>
        /// <param name="currencyCode">
        /// The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        /// The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="id">The track id.</param>
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// The information about track with the specified <paramref name="id"/> or <b>null</b> if track doesn't exist.
        /// </returns>
        TrackDetailsViewModel GetTrackDetails(int id, int? currencyCode = null, int? priceLevel = null, int? userId = null);

        /// <summary>
        /// Returns all registered tracks using the specified currency and price level for track price.
        /// </summary>
        /// <param name="currencyCode">
        /// The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        /// The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// All registered tracks.
        /// </returns>
        ICollection<TrackViewModel> GetTracksList(int? currencyCode = null, int? priceLevel = null, int? userId = null);

        /// <summary>
        /// Returns all tracks which don't have price.
        /// </summary>
        /// <returns>
        /// All tracks without price configured.
        /// </returns>
        ICollection<TrackViewModel> GetTracksWithoutPrice();

        /// <summary>
        /// Returns all tracks with price specified using the specified currency and price level for track price.
        /// </summary>
        /// <param name="currencyCode">
        /// The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        /// The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// All tracks with price specified.
        /// </returns>
        ICollection<TrackViewModel> GetTracksWithPrice(int? currencyCode = null, int? priceLevel = null, int? userId = null);

        /// <summary>
        /// Returns all albums which contain the specified track using the specified currency and price level.
        /// </summary>
        /// <param name="trackId">The track id.</param>
        /// <param name="currencyCode">
        /// The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevelId">
        /// The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <param name="userId">
        /// The current user id.
        /// </param>
        /// <returns>
        /// All albums which contain the specified track.
        /// </returns>
        TrackAlbumsListViewModel GetAlbumsList(int trackId, int? currencyCode = null, int? priceLevelId = null, int? userId = null);

        /// <summary>
        /// Return all tracks that the specified user have bought.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// All tracks that the specified user have bought.
        /// </returns>
        ICollection<PurchasedTrackViewModel> GetPurchasedTracks(int userId);
    }
}