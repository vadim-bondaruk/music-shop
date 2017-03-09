namespace PVT.Q1._2017.Shop.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Models;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.DAL.Infrastruture;

    /// <summary>
    /// The currency controller for admin
    /// </summary>
    public class CurrencyController : Controller
    {
        #region Fields

        /// <summary>
        /// The repository currency
        /// </summary>
        private readonly ICurrencyRepository _currencyRepository;

        /// <summary>
        /// The service currency
        /// </summary>
        private readonly ICurrencyService _curencyService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyController"/> class.
        /// </summary>
        /// <param name="currencyRepository">The repository currency</param>
        /// <param name="currencyService">The service currency</param>
        public CurrencyController(ICurrencyRepository currencyRepository, ICurrencyService currencyService)
        {
            this._currencyRepository = currencyRepository;
            this._curencyService = currencyService;
        }

        #endregion

        #region Action

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var currency = this._curencyService.GetCurrenciesList();
            return this.View(currency);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(CurrencyViewModel model)
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Create(CurrencyViewModel model)
        {
            return this.View();
        }

        #endregion
    }
}