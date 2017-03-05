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
    /// The price level repository.
    /// </summary>
    public class PriceLevelRepository : Repository<PriceLevel>, IPriceLevelRepository
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PriceLevelRepository" /> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public PriceLevelRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public new ICollection<PriceLevel> GetAll()
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
        /// </exception>
        public new ICollection<PriceLevel> GetAll(Expression<Func<PriceLevel, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}