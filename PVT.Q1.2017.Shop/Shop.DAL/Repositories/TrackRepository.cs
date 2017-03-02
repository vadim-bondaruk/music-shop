namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The track repository.
    /// </summary>
    public class TrackRepository : Repository<Track>, ITrackRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The Db context.
        /// </param>
        public TrackRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}