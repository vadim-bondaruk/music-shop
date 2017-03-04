namespace Shop.BLL.Services.Infrastructure
{
    using Common.Models;
    using Shop.Infrastructure.Services;

    /// <summary>
    /// The album service.
    /// </summary>
    public interface IAlbumService : IService<Album>
    {
        /// <summary>
        /// Returns the album with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>
        /// The album with the specified <paramref name="id"/> or <b>null</b> if album doesn't exist.
        /// </returns>
        Album GetAlbumInfo(int id);
    }
}