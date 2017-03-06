namespace Shop.BLL.Services.Infrastructure
{
    using Common.Models;
    using Shop.Infrastructure.Services;

    /// <summary>
    /// The genre service.
    /// </summary>
    public interface IGenreService : IService<Genre>
    {
        /// <summary>
        /// Adds the genre with the specified <paramref name="name"/>
        /// </summary>
        /// <param name="name">
        /// The genre name.
        /// </param>
        void AddGenre(string name);

        /// <summary>
        /// Returns the genre with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">
        /// The genre id.
        /// </param>
        /// <returns>
        /// The genre with the specified <paramref name="id"/> or <b>null</b> if genre doesn't exist.
        /// </returns>
        Genre GetGenreInfo(int id);
    }
}