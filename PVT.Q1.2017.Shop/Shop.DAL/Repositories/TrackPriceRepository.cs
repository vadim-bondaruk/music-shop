namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The track price repository.
    /// </summary>
    public class TrackPriceRepository : Repository<TrackPrice>, ITrackPriceRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackPriceRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public TrackPriceRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors

        #region Protected Methods

        /// <summary>
        /// Adds the specified <paramref name="trackPrice"/> into Db.
        /// </summary>
        /// <param name="trackPrice">
        /// The track price to add.
        /// </param>
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

            // adding the track price into Db
            base.Add(trackPrice);

            // restoring navigation properties states
            this.RestoreNavigationPropertyState(trackPrice.Track, trackEntryState);
            this.RestoreNavigationPropertyState(trackPrice.Currency, currencyEntryState);
            this.RestoreNavigationPropertyState(trackPrice.PriceLevel, priceLevelEntryState);
        }

        #endregion //Protected Methods
    }
}