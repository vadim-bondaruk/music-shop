namespace Shop.BLL.Services.Infrastructure
{
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
        ArtistDetailsViewModel GetArtistDetails(int id);
    }
}