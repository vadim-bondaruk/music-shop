using Shop.Common.Models;
using Shop.Infrastructure.Repositories;

namespace Shop.DAL.Infrastruture
{
    /// <summary>
    /// The genre repository.
    /// </summary>
    public interface IGenreRepository : IRepository<Genre>
    {
    }
}