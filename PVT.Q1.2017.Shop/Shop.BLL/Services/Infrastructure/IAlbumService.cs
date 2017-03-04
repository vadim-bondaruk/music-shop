namespace Shop.BLL.Services.Infrastructure
{
    using System.Collections.Generic;
    using Common.Models;
    using Shop.Infrastructure.Services;

    /// <summary>
    /// The album service.
    /// </summary>
    public interface IAlbumService : IService<Album>
    {
        /// <summary>
        /// Adds a new album with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="artist">
        /// The artist.
        /// </param>
        /// <param name="name">
        /// The album name.
        /// </param>
        void AddAlbum(Artist artist, string name);

        /// <summary>
        /// Returns the album with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>
        /// The album with the specified <paramref name="id"/> or <b>null</b> if album doesn't exist.
        /// </returns>
        Album GetAlbumInfo(int id);

        /// <summary>
        /// Returns all registered tracks for the specified <paramref name="album"/>.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns>
        /// All registered tracks for the specified <paramref name="album"/>.
        /// </returns>
        ICollection<Track> GetTracksList(Album album);

        /// <summary>
        /// Returns all tracks for the specified <paramref name="album"/> without price configured.
        /// </summary>
        /// <returns>
        /// All tracks for the specified <paramref name="album"/> without price configured.
        /// </returns>
        ICollection<Track> GetTracksWithoutPrice(Album album);

        /// <summary>
        /// Returns all tracks for the specified <paramref name="album"/> with price specified.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns>
        /// All tracks for the specified <paramref name="album"/> without price specified.
        /// </returns>
        ICollection<Track> GetTracksWithPrice(Album album);

        /// <summary>
        /// Returns all registered tracks.
        /// </summary>
        /// <returns>
        /// All registered tracks.
        /// </returns>
        ICollection<Album> GetAlbumsList();

        /// <summary>
        /// Returns all tracks without price configured.
        /// </summary>
        /// <returns>
        /// All tracks without price configured.
        /// </returns>
        ICollection<Album> GetAlbumsWithoutPrice();

        /// <summary>
        /// Returns all tracks with price specified.
        /// </summary>
        /// <returns>
        /// All tracks without price specified.
        /// </returns>
        ICollection<Album> GetAlbumsWithPrice();
        
        /// <summary>
        /// Returns all <paramref name="album"/> prices for the specified  <paramref name="priceLevel"/>.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="priceLevel">The price level.</param>
        /// <returns>All <paramref name="album"/> prices for the specified  <paramref name="priceLevel"/>.</returns>
        ICollection<AlbumPrice> GetAlbumPrices(Album album, PriceLevel priceLevel);

        /// <summary>
        /// Returns all <paramref name="album"/> prices.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns>All <paramref name="album"/> prices>.</returns>
        ICollection<AlbumPrice> GetAlbumPrices(Album album);
    }
}