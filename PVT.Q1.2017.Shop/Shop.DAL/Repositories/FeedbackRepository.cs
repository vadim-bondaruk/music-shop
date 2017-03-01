namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using Common.Models;

    /// <summary>
    /// The feedback repository.
    /// </summary>
    public class FeedbackRepository : Repository<Feedback>
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

        #region Protected Methods

        /// <summary>
        /// Loads additional references.
        /// </summary>
        /// <param name="queryResult">
        /// The query result.
        /// </param>
        /// <returns>
        /// </returns>
        protected override IQueryable<Feedback> LoadAdditionalInfo(IQueryable<Feedback> queryResult)
        {
            return base.LoadAdditionalInfo(queryResult).Include(f => f.Track).Include(f => f.User);
        }

        #endregion //Protected Methods
    }
}