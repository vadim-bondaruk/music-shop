using Shop.Common.Models;
using Shop.Infrastructure.Repositories;

namespace Shop.DAL.Infrastruture
{
    /// <summary>
    /// The artist repository
    /// </summary>
    public interface IArtistRepository : IRepository<Artist>
    {
    }
}