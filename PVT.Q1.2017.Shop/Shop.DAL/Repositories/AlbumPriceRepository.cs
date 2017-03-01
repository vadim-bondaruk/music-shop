namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using Common.Models;

    /// <summary>
    /// The album price repository.
    /// </summary>
    public class AlbumPriceRepository : Repository<AlbumPrice>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumPriceRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The Db context.
        /// </param>
        public AlbumPriceRepository(DbContext dbContext) : base(dbContext)
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
        protected override IQueryable<AlbumPrice> LoadAdditionalInfo(IQueryable<AlbumPrice> queryResult)
        {
            return base.LoadAdditionalInfo(queryResult).Include(p => p.Album).Include(p => p.Currency).Include(p => p.PriceLevel);
        }

        #endregion //Protected Methods
    }
}