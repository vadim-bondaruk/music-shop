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
        /// <summary>
        /// 
        /// </summary>
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
        public CurrencyViewModel GetCurrentUserCurrency()
        {
            if (CurrentUser == null)
            {
                return null;
            }

            var factory = DependencyResolver.Current.GetService<IRepositoryFactory>();
            Currency currency;
            using (var repository = factory.GetUserDataRepository())
            {
                currency = repository.FirstOrDefault(u => u.UserId == CurrentUser.Id, u => u.UserCurrency).UserCurrency;
            }

            return ModelsMapper.GetCurrencyViewModel(currency);
        }

        /// <summary>
        /// Returns the current user price level.
        /// </summary>
        /// <returns></returns>
        public int? GetCurrentUserPriceLevel()
        {
            if (CurrentUser == null)
            {
                return null;
            }

            var factory = DependencyResolver.Current.GetService<IRepositoryFactory>();
            using (var repository = factory.GetUserDataRepository())
            {
                return repository.FirstOrDefault(u => u.UserId == CurrentUser.Id).PriceLevelId;
            }
        }
    }
}