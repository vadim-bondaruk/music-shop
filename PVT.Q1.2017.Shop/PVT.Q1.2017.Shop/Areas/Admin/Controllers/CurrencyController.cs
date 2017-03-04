namespace PVT.Q1._2017.Shop.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure.Repositories;

    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class CurrencyController : Controller
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<Currency> _currencyRepository;

        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<CurrencyRate> _currencyRateRepository;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        public CurrencyController(IRepository<Currency> currencyRepo, IRepository<CurrencyRate> currencyRateRepo)
        {
            this._currencyRepository = currencyRepo;
            this._currencyRateRepository = currencyRateRepo;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        /// <summary>
        /// Перегруженная версия
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(CurrencyRate currencyRate)
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
    }
}