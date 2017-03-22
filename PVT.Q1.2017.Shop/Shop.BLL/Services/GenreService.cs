﻿namespace Shop.BLL.Services
{
    using System.Collections.Generic;

    using Shop.BLL.Services.Infrastructure;
    using Shop.Common.Models;
    using Shop.DAL.Infrastruture;

    /// <summary>
    ///     The Genre service
    /// </summary>
    public class GenreService : BaseService, IGenreService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenreService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        public GenreService(IRepositoryFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// </returns>
        public Genre GetGenre(int id)
        {
            using (var repository = this.Factory.GetGenreRepository())
            {
                return null;

                // repository.GetById(id, t => t.Artist, t => t.Genre);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ICollection<Genre> GetGenresList()
        {
            using (var repository = this.Factory.GetGenreRepository())
            {
                return null;

                // repository.GetAll(t => t.Artist, t => t.Genre);
            }
        }
    }
}