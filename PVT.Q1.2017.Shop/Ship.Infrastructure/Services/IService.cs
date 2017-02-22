using Shop.Infrastructure.Models;

namespace Shop.Infrastructure.Services
{
    /// <summary>
    /// Base service contract (dummy)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IService<TEntity> where TEntity : BaseEntity, new()
    {

    }
}