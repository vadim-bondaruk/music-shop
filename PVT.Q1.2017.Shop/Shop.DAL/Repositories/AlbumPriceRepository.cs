namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// The album price repository
    /// </summary>
    public class AlbumPriceRepository : Repository<AlbumPrice>, IAlbumPriceRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumPriceRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public AlbumPriceRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion //Constructors
    }
}