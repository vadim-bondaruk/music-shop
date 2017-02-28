namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using Common.Models;

    /// <summary>
    /// The vote repository.
    /// </summary>
    public class VoteRepository : Repository<Vote>
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
        protected override IQueryable<Vote> LoadAdditionalInfo(IQueryable<Vote> queryResult)
        {
            return base.LoadAdditionalInfo(queryResult).Include(v => v.Track).Include(v => v.User);
        }

        #endregion //Protected Methods
    }
}