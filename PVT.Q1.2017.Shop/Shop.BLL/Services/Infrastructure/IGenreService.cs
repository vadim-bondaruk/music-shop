namespace Shop.BLL.Services.Infrastructure
{
    using System.Collections.Generic;
    using Common.Models;
    using Common.Models.ViewModels;

    /// <summary>
    /// The Genre service
    /// </summary>
    public interface IGenreService
    {
        /// <summary>
        /// Returns all registered Genres.
        /// </summary>
        /// <returns>
        /// All registered Genres.
        /// </returns>
        ICollection<Genre> GetGenresList();

        /// <summary>
        /// Returns the Genre with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The Genre id.</param>
        /// <returns>
        /// The Genre with the specified <paramref name="id"/> or <b>null</b> if Genre doesn't exist.
        /// </returns>
        Genre GetGenre(int id);
    }
}