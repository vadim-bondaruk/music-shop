namespace Shop.DAL.Repositories
{
    using System.Data.Entity;
    using Common.Models;
    using Infrastruture;


    /// <summary>
    /// Repository of User's Shop Cart
    /// </summary>
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartRepository"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// The db context.
        /// </param>
        public CartRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Tries to find a Cart by UserID
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <returns>User's Cart</returns>
        public Cart GetByUserId(int userId)
        {
            return FirstOrDefault(x => x.UserId == userId);
        }
    }
}
