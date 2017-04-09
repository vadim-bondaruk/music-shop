namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    [Authorize]
    public class CurrencyController : BaseController
    {
        private readonly ICurrencyService _currencyService;
        private readonly IRepositoryFactory _repositoryFactory;

        public CurrencyController(ICurrencyService currencyService, IRepositoryFactory repositoryFactory)
        {
            _currencyService = currencyService;
            _repositoryFactory = repositoryFactory;
        }

        [ChildActionOnly]
        public ActionResult UserCurrency()
        {
            var currenctCurrency = GetCurrentUserCurrency();
            if (currenctCurrency != null)
            {
                ViewBag.SelectedCurrency = GetCurrentUserCurrency().Code;
                return PartialView("_UserCurrency", _currencyService.GetCurrenciesList());
            }

            return PartialView("_UserCurrency");
        }

        [HttpPost]
        public ActionResult UpdateUserCurrency(int? currencyCode)
        {
            if (CurrentUser != null)
            {
                Currency currency;
                using (var repository = _repositoryFactory.GetCurrencyRepository())
                {
                    currency = repository.FirstOrDefault(c => c.Code == currencyCode);
                }

                if (currency == null)
                {
                    return HttpNotFound("Валюта с указанным кодом не найдена");
                }

                using (var repository = _repositoryFactory.GetUserDataRepository())
                {
                    var userData = repository.FirstOrDefault(u => u.UserId == CurrentUser.Id);
                    if (userData == null)
                    {
                        return HttpNotFound("Данные пользователя не найдены");
                    }

                    userData.CurrencyId = currency.Id;
                    repository.AddOrUpdate(userData);
                    repository.SaveChanges();
                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }
    }
}