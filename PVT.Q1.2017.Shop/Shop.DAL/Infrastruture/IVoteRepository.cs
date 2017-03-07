using Shop.Common.Models;
using Shop.Infrastructure.Repositories;

namespace Shop.DAL.Infrastruture
{
    /// <summary>
    /// The vote repository.
    /// </summary>
    public interface IVoteRepository : IRepository<Vote>
    {
    }
}