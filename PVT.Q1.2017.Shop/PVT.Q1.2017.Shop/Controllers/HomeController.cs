namespace PVT.Q1._2017.Shop.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Infrastructure;
    using Infrastructure.Extensions;
    using global::Shop.BLL.Services;
    using ViewModels;

    /// <summary>
    /// Controller of Home page
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// Service
        /// </summary>
        private readonly ICurrencyService _service;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="service"></param>
        public HomeController(ICurrencyService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Default start page
        /// </summary>
        /// <returns>View of index page</returns>
        public async Task<ActionResult> Index()
        {
            var rate = await this._service.GetActualRatesAsync(DateTime.Now);

            return this.View(new MainViewModel(User.Identity.IdentityToUserDataModel()));
        }
    }
}