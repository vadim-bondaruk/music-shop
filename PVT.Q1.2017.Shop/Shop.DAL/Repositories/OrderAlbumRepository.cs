namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// Repository of Ordered Album
    /// </summary>
    public class OrderAlbumRepository : BaseRepository<OrderAlbum>, IOrderAlbumRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderAlbumRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public OrderAlbumRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
