namespace Shop.DAL.Repositories
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq.Expressions;

    using Shop.Common.Models;
    using Shop.DAL.Repositories.Infrastruture;

    #endregion

    /// <summary>
    /// The artist repsitory.
    /// </summary>
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistRepository" />
        /// class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public ArtistRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// ***
        /// </exception>
        public new ICollection<Artist> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// ***
        /// </exception>
        public new ICollection<Artist> GetAll(Expression<Func<Artist, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}