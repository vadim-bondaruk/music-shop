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
    ///     The album price repository
    /// </summary>
    public class AlbumPriceRepository : Repository<AlbumPrice>, IAlbumPriceRepository
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AlbumPriceRepository" /> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public AlbumPriceRepository(DbContext dbContext)
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
        public new ICollection<AlbumPrice> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="filter">
        ///     The filter.
        /// </param>
        /// <param name="includes">
        ///     The includes.
        /// </param>
        /// <returns>
        /// </returns>
        public new ICollection<AlbumPrice> GetAll(Expression<Func<AlbumPrice, bool>> filter)
        {
            return null;
        }

        /// <summary>
        ///     Adds the specified <paramref name="albumPrice" /> into Db.
        /// </summary>
        /// <param name="albumPrice">The album price to add.</param>
        protected override void Add(AlbumPrice albumPrice)
        {
            EntityState albumEntryState;
            EntityState currencyEntryState;
            EntityState priceLevelEntryState;

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The AlbumPriceRepository should be SOLID, should only add information about album price! Not about album, currency or price level!
            this.DetachNavigationProperty(albumPrice.Album, out albumEntryState);
            this.DetachNavigationProperty(albumPrice.Currency, out currencyEntryState);
            this.DetachNavigationProperty(albumPrice.PriceLevel, out priceLevelEntryState);

            albumPrice.Album = null;
            albumPrice.Currency = null;
            albumPrice.PriceLevel = null;

            // adding the album price into Db
            base.Add(albumPrice);
        }
    }
}