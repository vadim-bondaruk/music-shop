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
    }
}