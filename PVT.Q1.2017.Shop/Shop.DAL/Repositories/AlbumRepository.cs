namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The album repository
    /// </summary>
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public AlbumRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}