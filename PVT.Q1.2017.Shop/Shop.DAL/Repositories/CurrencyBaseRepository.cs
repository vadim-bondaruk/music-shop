//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Track.cs" company="PVT.Q1.2017">
//    PVT.Q1.2017
//  </copyright>
//  <summary>
//    The track.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace Shop.DAL.Repositories
{
    using System.Data.Entity;

    using Shop.Common.Models;
    using Shop.DAL.Infrastruture;

    /// <summary>
    ///     The currency repository
    /// </summary>
    public class CurrencyBaseRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CurrencyBaseRepository" /> class.
        /// </summary>
        /// <param name="dbContext">
        ///     The db context.
        /// </param>
        public CurrencyBaseRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}