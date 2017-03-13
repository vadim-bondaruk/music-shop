namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The track repository.
    /// </summary>
    public class TrackBaseRepository : BaseRepository<Track>, ITrackRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackBaseRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The Db context.
        /// </param>
        public TrackBaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Adds the specified <paramref name="track"/> into Db.
        /// </summary>
        /// <param name="track">
        /// The track to add.
        /// </param>
        protected override void Add(Track track)
        {
            EntityState artistEntryState;
            EntityState genreEntryState;

            if (track.ArtistId == null && track.Artist != null)
            {
                track.ArtistId = track.Artist.Id;
            }

            if (track.GenreId == null && track.Genre != null)
            {
                track.GenreId = track.Genre.Id;
            }

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The TrackBaseRepository should be SOLID, should only add information about track! Not about artist, album or genre!
            this.DetachNavigationProperty(track.Artist, out artistEntryState);
            this.DetachNavigationProperty(track.Genre, out genreEntryState);

            track.Artist = null;
            track.Genre = null;

            // adding the track into Db
            base.Add(track);
        }
    }
}