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
    ///     The artist repsitory.
    /// </summary>
    public class ArtistBaseRepository : BaseRepository<Artist>, IArtistRepository
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArtistBaseRepository" /> class.
        /// </summary>
        /// <param name="dbContext">
        ///     The db context.
        /// </param>
        public ArtistBaseRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}