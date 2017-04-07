namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The track repository.
    /// </summary>
    public class TrackRepository : BaseRepository<Track>, ITrackRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The Db context.
        /// </param>
        public TrackRepository(DbContext dbContext) : base(dbContext)
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
            EntityState ownerEntryState;

            if (track.ArtistId <= 0 && track.Artist != null)
            {
                track.ArtistId = track.Artist.Id;
            }

            if (track.GenreId <= 0 && track.Genre != null)
            {
                track.GenreId = track.Genre.Id;
            }

            if (track.OwnerId == null && track.Owner != null)
            {
                track.OwnerId = track.Owner.Id;
            }

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The TrackBaseRepository should be SOLID, should only add information about track! Not about artist, album or genre!
            this.DetachNavigationProperty(track.Artist, out artistEntryState);
            this.DetachNavigationProperty(track.Genre, out genreEntryState);
            this.DetachNavigationProperty(track.Owner, out ownerEntryState);

            track.Artist = null;
            track.Genre = null;
            track.Owner = null;

            // adding the track into Db
            base.Add(track);
        }
    }
}