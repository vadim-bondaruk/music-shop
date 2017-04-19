namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Helpers;
    using global::Shop.Common.Models;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.BLL.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class BaseController : Controller
    {
        protected readonly IRepositoryFactory RepositoryFactory;
        protected readonly IServiceFactory ServiceFactory;

        public BaseController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory)
        {
            RepositoryFactory = repositoryFactory;
            ServiceFactory = serviceFactory;
        }

        private const string CURRENT_USER_DATA_ID_KEY = "UserDataId";

        public CurrentUser CurrentUser
        {
            get
            {
                return this.HttpContext.User as CurrentUser;
            }
        }

        /// <summary>
        /// Returns the current user currency.
        /// </summary>
        /// <returns>
        /// The current user currency or <b>null</b> if the there is an ananimous user.
        /// </returns>
        protected CurrencyViewModel GetCurrentUserCurrency()
        {
            if (CurrentUser == null)
            {
                return null;
            }

            var factory = DependencyResolver.Current.GetService<IRepositoryFactory>();
            Currency currency = null;
            using (var repository = factory.GetUserDataRepository())
            {
                var userData = repository.FirstOrDefault(u => u.UserId == CurrentUser.Id, u => u.UserCurrency);
                if (userData != null)
                {
                    currency = userData.UserCurrency;
                }  
            }

            return ModelsMapper.GetCurrencyViewModel(currency);
        }

        /// <summary>
        /// Returns the current user price level.
        /// </summary>
        /// <returns></returns>
        protected int? GetCurrentUserPriceLevel()
        {
            if (CurrentUser == null)
            {
                return null;
            }

            var factory = DependencyResolver.Current.GetService<IRepositoryFactory>();
            using (var repository = factory.GetUserDataRepository())
            {
                var userData = repository.FirstOrDefault(u => u.UserId == CurrentUser.Id);
                if (userData != null)
                {
                    return userData.PriceLevelId;
                }
            }

            return null;
        }

        protected int? GetUserDataId()
        {
            if (CurrentUser == null)
            {
                return null;
            }

            if (Session[CURRENT_USER_DATA_ID_KEY] != null)
            {
                return (int)Session[CURRENT_USER_DATA_ID_KEY];
            }

            var factory = DependencyResolver.Current.GetService<IRepositoryFactory>();
            using (var repository = factory.GetUserDataRepository())
            {
                var userData = repository.FirstOrDefault(u => u.UserId == CurrentUser.Id);
                if (userData != null)
                {
                    Session[CURRENT_USER_DATA_ID_KEY] = userData.Id;
                    return userData.Id;
                }
            }

            return null;
        }
    }
}