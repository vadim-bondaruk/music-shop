using Ship.Infrastructure.Models;

namespace Ship.Infrastructure.Services
{
    /// <summary>
    /// Base service contract (dummy)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IService<TEntity> where TEntity : BaseEntity, new()
    {

    }
}