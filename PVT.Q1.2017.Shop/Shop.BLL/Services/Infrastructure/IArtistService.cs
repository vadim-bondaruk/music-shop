namespace Shop.BLL.Services.Infrastructure
{
    using Shop.Common.Models;

    /// <summary>
    ///     The Artist service.
    /// </summary>
    public interface IArtistService
    {
        /// <summary>
        /// </summary>
        /// <param name="artistId">
        /// The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        Artist GetArtist(int artistId);

        /// <summary>
        ///     Returns the Artist with the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The Artist id.</param>
        /// <returns>
        ///     The Artist with the specified <paramref name="id" /> or <b>null</b> if Artist doesn't exist.
        /// </returns>
        ArtistDetailsViewModel GetArtistDetailsViewModel(int id);
    }
}