namespace Shop.DAL.Infrastruture
{
    using Common.Models;
    using Infrastructure.Repositories;

    /// <summary>
    /// The album price repository.
    /// </summary>
    public interface ICartRepository : IRepository<Cart>
    {
    }
}