using Shop.Common.Models;
using Shop.Infrastructure.Repositories;

namespace Shop.DAL.Infrastruture
{
    /// <summary>
    /// The currency repository
    /// </summary>
    public interface ICurrencyRepository : IRepository<Currency>
    {
    }
}