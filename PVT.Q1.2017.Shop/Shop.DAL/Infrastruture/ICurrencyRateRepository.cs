using Shop.Common.Models;
using Shop.Infrastructure.Repositories;

namespace Shop.DAL.Infrastruture
{
    /// <summary>
    /// The currency rate repository.
    /// </summary>
    public interface ICurrencyRateRepository : IRepository<CurrencyRate>
    {
    }
}