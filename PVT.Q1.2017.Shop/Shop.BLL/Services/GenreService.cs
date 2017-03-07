using Shop.DAL.Infrastruture;

namespace Shop.BLL.Services
{
    using Common.Models;
    using Infrastructure;

    /// <summary>
    /// The genre service.
    /// </summary>
    public class GenreService : Service<IGenreRepository, Genre>, IGenreService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenreService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="validator">
        /// The validator.
        /// </param>
        public GenreService(IFactory factory, IValidator<Genre> validator) : base(factory, validator)
        {
        }

        #endregion //Constructors

        #region IGenreService Members

        /// <summary>
        /// Adds the genre with the specified <paramref name="name"/>
        /// </summary>
        /// <param name="name">
        /// The genre name.
        /// </param>
        public void AddGenre(string name)
        {
            var genre = new Genre
            {
                Name = name
            };

            this.Register(genre);
        }

        /// <summary>
        /// Returns the genre with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">
        /// The genre id.
        /// </param>
        /// <returns>
        /// The genre with the specified <paramref name="id"/> or <b>null</b> if genre doesn't exist.
        /// </returns>
        public Genre GetGenreInfo(int id)
        {
            using (var repository = this.CreateRepository())
            {
                return repository.GetById(id);
            }
        }

        #endregion //IGenreService Members
    }
}