namespace Shop.BLL.Services.Infrastructure
{
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
    }
}