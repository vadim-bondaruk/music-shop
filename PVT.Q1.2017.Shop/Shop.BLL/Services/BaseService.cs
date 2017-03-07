using Shop.DAL.Infrastruture;

namespace Shop.BLL.Services
{
    public abstract class BaseService
    {
        protected readonly IRepositoryFactory _factory;

        protected BaseService(IRepositoryFactory factory)
        {
            _factory = factory;
        } 
    }
}