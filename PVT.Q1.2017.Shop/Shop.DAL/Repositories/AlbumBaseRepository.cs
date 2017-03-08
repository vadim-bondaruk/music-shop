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
    ///     The album repository
    /// </summary>
    public class AlbumBaseRepository : BaseRepository<Album>, IAlbumRepository
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AlbumBaseRepository" /> class.
        /// </summary>
        /// <param name="dbContext">
        ///     The db context.
        /// </param>
        public AlbumBaseRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion //Constructors

        #region Protected Methods

        /// <summary>
        ///     Adds the specified <paramref name="album" /> into Db.
        /// </summary>
        /// <param name="album">
        ///     The album to add.
        /// </param>
        protected override void Add(Album album)
        {
            EntityState artistEntryState;

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The AlbumBaseRepository should be SOLID, should only add information about album! Not about artist!
            this.DetachNavigationProperty(album.Artist, out artistEntryState);

            album.Artist = null;

            // adding the album into Db.
            base.Add(album);
        }

        #endregion //Protected Methods
    }
}