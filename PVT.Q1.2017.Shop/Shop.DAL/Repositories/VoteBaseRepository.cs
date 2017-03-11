namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// </summary>
    public class VoteBaseRepository : BaseRepository<Vote>, IVoteRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoteBaseRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public VoteBaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Returns average track rating for the specified <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <returns>
        /// The average track rating for the specified <paramref name="track"/>.
        /// </returns>
        public double GetAverageTrackRating(Track track)
        {
            return CurrentDbSet.Where(v => v.TrackId == track.Id).Average(v => (int)v.Mark);
        }

        /// <summary>
        /// Adds the specified <paramref name="vote"/> into Db.
        /// </summary>
        /// <param name="vote">
        /// The vote to add.
        /// </param>
        protected override void Add(Vote vote)
        {
            EntityState trackEntryState;
            EntityState userEntryState;

            if (vote.TrackId == 0 && vote.Track != null)
            {
                vote.TrackId = vote.Track.Id;
            }

            if (vote.UserId == 0 && vote.User != null)
            {
                vote.UserId = vote.User.Id;
            }

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The VoteBaseRepository should be SOLID, should only add information about vote! Not about track or user!
            this.DetachNavigationProperty(vote.Track, out trackEntryState);
            this.DetachNavigationProperty(vote.User, out userEntryState);

            vote.Track = null;
            vote.User = null;

            // adding the vote into Db.
            base.Add(vote);
        }
    }
}