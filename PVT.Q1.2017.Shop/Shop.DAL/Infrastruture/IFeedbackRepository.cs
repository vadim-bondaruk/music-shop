using Shop.Common.Models;
using Shop.Infrastructure.Repositories;

namespace Shop.DAL.Infrastruture
{
    /// <summary>
    /// The feedback repository.
    /// </summary>
    public interface IFeedbackRepository : IRepository<Feedback>
    {
    }
}