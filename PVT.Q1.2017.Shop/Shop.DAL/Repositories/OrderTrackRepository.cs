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

        /// <summary>
        /// Delete Order Track from Repository
        /// </summary>
        /// <param name="model">OrderTrack to remove</param>
        public override void Delete(OrderTrack model)
        {
            if (model == null) return;
            this.CurrentDbSet.Remove(model);
            this.SetStateChanged();
        }
    }
}
