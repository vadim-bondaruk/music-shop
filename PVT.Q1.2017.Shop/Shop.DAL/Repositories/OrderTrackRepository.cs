namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;

    /// <summary>
    /// Repository of Ordered Track
    /// </summary>
    public class OrderTrackRepository : BaseRepository<OrderTrack>, IOrderTrackRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderTrackRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public OrderTrackRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
