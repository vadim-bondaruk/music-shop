namespace Shop.BLL.Services.Infrastructure
{
    using System.Collections.Generic;
    using Common.Models;
    using Shop.Infrastructure.Services;

    /// <summary>
    /// The artist service.
    /// </summary>
    public interface IArtistService : IService<Artist>
    {
        /// <summary>
        /// Returns the artist with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The artist id.</param>
        /// <returns>
        /// The artist with the specified <paramref name="id"/> or <b>null</b> if artist doesn't exist.
        /// </returns>
        Artist GetArtistInfo(int id);

        /// <summary>
        /// Returns all registered albums for the specified <paramref name="artist"/>.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>
        /// All registered albums for the specified <paramref name="artist"/>.
        /// </returns>
        ICollection<Album> GetAlbumsList(Artist artist);

        /// <summary>
        /// Returns all registered tracks for the specified <paramref name="artist"/>.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>
        /// All registered tracks for the specified <paramref name="artist"/>.
        /// </returns>
        ICollection<Track> GetTracksList(Artist artist);

        /// <summary>
        /// Returns all tracks for the specified <paramref name="artist"/> without price configured.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>
        /// All tracks for the specified <paramref name="artist"/> without price configured.
        /// </returns>
        ICollection<Track> GetTracksWithoutPrice(Artist artist);

        /// <summary>
        /// Returns all tracks for the specified <paramref name="artist"/> with price specified.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>
        /// All tracks for the specified <paramref name="artist"/> with price specified.
        /// </returns>
        ICollection<Track> GetTracksWithPrice(Artist artist);

        /// <summary>
        /// Returns all albums for the specified <paramref name="artist"/> without price configured.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>
        /// All albums for the specified <paramref name="artist"/> without price configured.
        /// </returns>
        ICollection<Album> GetAlbumsWithoutPrice(Artist artist);

        /// <summary>
        /// Returns all albums for the specified <paramref name="artist"/> with price specified.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>
        /// All albums for the specified <paramref name="artist"/> with price specified.
        /// </returns>
        ICollection<Album> GetAlbumsWithPrice(Artist artist);
    }
}