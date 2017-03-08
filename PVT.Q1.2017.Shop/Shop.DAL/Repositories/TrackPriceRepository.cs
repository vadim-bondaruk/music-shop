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
    ///     The track price repository.
    /// </summary>
    public class TrackPriceRepository : Repository<TrackPrice>, ITrackPriceRepository
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="TrackPriceRepository" /> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public TrackPriceRepository(DbContext dbContext)
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
        public new ICollection<TrackPrice> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="filter">
        ///     The filter.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// ***
        /// </exception>
        public new ICollection<TrackPrice> GetAll(Expression<Func<TrackPrice, bool>> filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Adds the specified <paramref name="trackPrice" /> into Db.
        /// </summary>
        /// <param name="trackPrice">The track price to add.</param>
        protected override void Add(TrackPrice trackPrice)
        {
            EntityState trackEntryState;
            EntityState currencyEntryState;
            EntityState priceLevelEntryState;

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The TrackPriceRepository should be SOLID, should only add information about track! Not about track, currency or price level!
            this.DetachNavigationProperty(trackPrice.Track, out trackEntryState);
            this.DetachNavigationProperty(trackPrice.Currency, out currencyEntryState);
            this.DetachNavigationProperty(trackPrice.PriceLevel, out priceLevelEntryState);

            trackPrice.Track = null;
            trackPrice.Currency = null;
            trackPrice.PriceLevel = null;

            // adding the track price into Db
            base.Add(trackPrice);
        }
    }
}