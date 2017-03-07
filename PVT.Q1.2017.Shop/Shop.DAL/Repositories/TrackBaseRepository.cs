using Shop.DAL.Infrastruture;

namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;

    /// <summary>
    /// The track repository.
    /// </summary>
    public class TrackBaseRepository : BaseRepository<Track>, ITrackRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackBaseRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The Db context.
        /// </param>
        public TrackBaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors

        #region Protected Methods

        /// <summary>
        /// Adds the specified <paramref name="track"/> into Db.
        /// </summary>
        /// <param name="track">
        /// The track to add.
        /// </param>
        protected override void Add(Track track)
        {
            EntityState artistEntryState;
            EntityState albumEntryState;
            EntityState genreEntryState;

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The TrackBaseRepository should be SOLID, should only add information about track! Not about artist, album or genre!
            this.DetachNavigationProperty(track.Artist, out artistEntryState);
            this.DetachNavigationProperty(track.Album, out albumEntryState);
            this.DetachNavigationProperty(track.Genre, out genreEntryState);

            track.Artist = null;
            track.Album = null;
            track.Genre = null;

            // adding the track into Db
            base.Add(track);
        }

        #endregion //Protected Methods
    }
}