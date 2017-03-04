namespace Shop.BLL.Services.Infrastructure
{
    using Common.Models;
    using Shop.Infrastructure.Services;

    /// <summary>
    /// The price level service
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
    }
}