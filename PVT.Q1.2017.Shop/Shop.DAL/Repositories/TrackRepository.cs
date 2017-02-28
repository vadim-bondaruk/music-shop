using System.Data.Entity;
using System.Linq;
using Shop.Common.Models;

namespace Shop.DAL.Repositories
{
    /// <summary>
    /// The track repository.
    /// </summary>
    public class TrackRepository : Repository<Track>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The Db context.
        /// </param>
        public TrackRepository(DbContext dbContext) : base(dbContext)
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
        protected override IQueryable<Track> LoadAdditionalInfo(IQueryable<Track> queryResult)
        {
            return base.LoadAdditionalInfo(queryResult).Include(t => t.Artist).Include(t => t.Album).Include(t => t.Genre);
        }

        #endregion //Protected Methods
    }
}