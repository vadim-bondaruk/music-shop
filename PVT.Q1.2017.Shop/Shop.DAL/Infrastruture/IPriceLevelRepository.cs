//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Track.cs" company="PVT.Q1.2017">
//    PVT.Q1.2017
//  </copyright>
//  <summary>
//    The track.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace Shop.DAL.Infrastruture
{
    using Shop.Common.Models;
    using Shop.Infrastructure.Repositories;

    /// <summary>
    ///     The price level repository.
    /// </summary>
    public interface IPriceLevelRepository : IRepository<PriceLevel>
    {
        /// <summary>
        /// Returns the default price level.
        /// </summary>
        /// <returns>
        /// The default price level.
        /// </returns>
        PriceLevel GetDefaultPriceLevel();
    }
}