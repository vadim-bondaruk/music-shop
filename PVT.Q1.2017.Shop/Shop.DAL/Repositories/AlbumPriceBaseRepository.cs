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
    ///     The album price repository
    /// </summary>
    public class AlbumPriceBaseRepository : BaseRepository<AlbumPrice>, IAlbumPriceRepository
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AlbumPriceBaseRepository" /> class.
        /// </summary>
        /// <param name="dbContext">
        ///     The db context.
        /// </param>
        public AlbumPriceBaseRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion //Constructors

        #region Protected Methods

        /// <summary>
        ///     Adds the specified <paramref name="albumPrice" /> into Db.
        /// </summary>
        /// <param name="albumPrice">
        ///     The album price to add.
        /// </param>
        protected override void Add(AlbumPrice albumPrice)
        {
            EntityState albumEntryState;
            EntityState currencyEntryState;
            EntityState priceLevelEntryState;

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The AlbumPriceBaseRepository should be SOLID, should only add information about album price! Not about album, currency or price level!
            this.DetachNavigationProperty(albumPrice.Album, out albumEntryState);
            this.DetachNavigationProperty(albumPrice.Currency, out currencyEntryState);
            this.DetachNavigationProperty(albumPrice.PriceLevel, out priceLevelEntryState);

            albumPrice.Album = null;
            albumPrice.Currency = null;
            albumPrice.PriceLevel = null;

            // adding the album price into Db
            base.Add(albumPrice);
        }

        #endregion //Protected Methods
    }
}