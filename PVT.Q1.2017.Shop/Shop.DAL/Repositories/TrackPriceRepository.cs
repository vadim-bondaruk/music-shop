using System.Data.Entity;
using System.Linq;

namespace Shop.DAL.Repositories
{
    using Common.Models;

    /// <summary>
    /// The track price repository.
    /// </summary>
    public class TrackPriceRepository : Repository<TrackPrice>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackPriceRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public TrackPriceRepository(DbContext dbContext) : base(dbContext)
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
        protected override IQueryable<TrackPrice> LoadAdditionalInfo(IQueryable<TrackPrice> queryResult)
        {
            return base.LoadAdditionalInfo(queryResult).Include(p => p.Track).Include(p => p.Currency).Include(p => p.PriceLevel);
        }

        #endregion //Protected Methods
    }
}