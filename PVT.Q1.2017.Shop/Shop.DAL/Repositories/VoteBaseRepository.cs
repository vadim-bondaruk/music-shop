namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
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