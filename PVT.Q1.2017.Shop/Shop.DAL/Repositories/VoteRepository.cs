namespace Shop.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq.Expressions;

    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// </summary>
    public class VoteRepository : Repository<Vote>, IVoteRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VoteRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public VoteRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// ***
        /// </exception>
        public new ICollection<Vote> GetAll(Expression<Func<Vote, bool>> filter)
        {
            throw new NotImplementedException();
        }

        #endregion //Constructors

        #region Protected Methods

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

            // Detaching the navigation properties in case if they are attached to prevent unexpected behaviour of the DbContext.
            // The VoteRepository should be SOLID, should only add information about vote! Not about track or user!
            this.DetachNavigationProperty(vote.Track, out trackEntryState);
            this.DetachNavigationProperty(vote.User, out userEntryState);

            vote.Track = null;
            vote.User = null;

            // adding the vote into Db.
            base.Add(vote);
        }

        #endregion //Protected Methods
    }
}