namespace Shop.DAL.Infrastruture
{
    using Common.Models;
    using Infrastructure.Repositories;

    /// <summary>
    /// The album price repository.
    /// </summary>
    public interface ICartRepository : IRepository<Cart>
    {
        /// <summary>
        /// Tries to find a Cart by UserID
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <returns>User's Cart</returns>
        Cart GetByUserId(int userId);
    }
}