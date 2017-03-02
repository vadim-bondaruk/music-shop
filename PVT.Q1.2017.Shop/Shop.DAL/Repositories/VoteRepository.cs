namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// </summary>
    public class VoteRepository : Repository<Vote>, IVoteRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoteRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public VoteRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}