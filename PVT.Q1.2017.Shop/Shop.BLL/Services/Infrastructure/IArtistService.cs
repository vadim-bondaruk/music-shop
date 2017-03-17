namespace Shop.BLL.Services.Infrastructure
{
    using System.Collections.Generic;

    using Shop.Common.Models;
    using Shop.Common.Models.ViewModels;

    /// <summary>
    ///     The Artist service.
    /// </summary>
    public interface IArtistService
    {
        /// <summary>
        ///     Returns the Artist with the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The Artist id.</param>
        /// <returns>
        ///     The Artist with the specified <paramref name="id" /> or <b>null</b> if Artist doesn't exist.
        /// </returns>
        ArtistDetailsViewModel GetArtistInfo(int id);

        /// <summary>
        ///     Returns all registered Artists.
        /// </summary>
        /// <returns>
        ///     All registered Artists.
        /// </returns>
        ICollection<Artist> GetArtistsList();

        /// <summary>
        /// </summary>
        /// <param name="artistId">
        /// The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        ICollection<Track> GetTracksList(int artistId);
    }
}