namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The feedback repository.
    /// </summary>
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public FeedbackRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}