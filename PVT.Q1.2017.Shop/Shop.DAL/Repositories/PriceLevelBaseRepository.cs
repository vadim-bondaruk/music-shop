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
    ///     The price level repository.
    /// </summary>
    public class PriceLevelBaseRepository : BaseRepository<PriceLevel>, IPriceLevelRepository
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PriceLevelBaseRepository" /> class.
        /// </summary>
        /// <param name="dbContext">
        ///     The db context.
        /// </param>
        public PriceLevelBaseRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}