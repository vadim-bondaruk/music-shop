namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using Common.Models;

    /// <summary>
    /// </summary>
    public class AlbumRepositry : Repository<Album>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumRepositry"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public AlbumRepositry(DbContext dbContext) : base(dbContext)
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
        protected override IQueryable<Album> LoadAdditionalInfo(IQueryable<Album> queryResult)
        {
            return base.LoadAdditionalInfo(queryResult).Include(a => a.Artist);
        }

        #endregion //Protected Methods
    }
}