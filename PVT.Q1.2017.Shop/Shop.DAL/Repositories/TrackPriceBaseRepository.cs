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
    ///     The track price repository.
    /// </summary>
    public class TrackPriceBaseRepository : BaseRepository<TrackPrice>, ITrackPriceRepository
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TrackPriceBaseRepository" /> class.
        /// </summary>
        /// <param name="dbContext">
        ///     The db context.
        /// </param>
        public TrackPriceBaseRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion //Constructors

        #region Protected Methods

        /// <summary>
        ///     Adds the specified <paramref name="trackPrice" /> into Db.
        /// </summary>
        /// <param name="trackPrice">
        ///     The track price to add.
        /// </param>
        protected override void Add(TrackPrice trackPrice)
        {
            EntityState trackEntryState;
            EntityState currencyEntryState;
            EntityState priceLevelEntryState;

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The TrackPriceBaseRepository should be SOLID, should only add information about track! Not about track, currency or price level!
            this.DetachNavigationProperty(trackPrice.Track, out trackEntryState);
            this.DetachNavigationProperty(trackPrice.Currency, out currencyEntryState);
            this.DetachNavigationProperty(trackPrice.PriceLevel, out priceLevelEntryState);

            trackPrice.Track = null;
            trackPrice.Currency = null;
            trackPrice.PriceLevel = null;

            // adding the track price into Db
            base.Add(trackPrice);
        }

        #endregion //Protected Methods
    }
}