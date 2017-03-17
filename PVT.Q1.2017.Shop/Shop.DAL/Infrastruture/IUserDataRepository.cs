namespace Shop.DAL.Infrastruture
{
    using Shop.Common.Models;
    using Shop.Infrastructure.Repositories;

    /// <summary>
    ///     The user repository.
    /// </summary>
    public interface IUserDataRepository : IRepository<UserData>
    {
    }
}