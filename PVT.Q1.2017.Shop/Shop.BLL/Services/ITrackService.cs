namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using Common.Models;
    using Infrastructure.Services;

    /// <summary>
    /// The track service.
    /// </summary>
    public interface ITrackService : IService<Track>
    {
        /// <summary>
        /// Returns all tracks with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        /// The track name.
        /// </param>
        /// <returns>
        /// All tracks with the specified <paramref name="name"/>.
        /// </returns>
        ICollection<Track> GetTracksByName(string name);

        /// <summary>
        /// Returns all tracks with the specified <paramref name="genre"/>.
        /// </summary>
        /// <param name="genre">
        /// The track genre.
        /// </param>
        /// <returns>
        /// All tracks with the specified <paramref name="genre"/>.
        /// </returns>
        ICollection<Track> GetTracksByGenre(Genre genre);

        /// <summary>
        /// Returns all registered tracks.
        /// </summary>
        /// <returns>
        /// All registered tracks.
        /// </returns>
        ICollection<Track> GetAllRegisteredTracks();
    }
}