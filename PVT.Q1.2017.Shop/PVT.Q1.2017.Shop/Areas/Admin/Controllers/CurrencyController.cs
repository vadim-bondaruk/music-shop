namespace PVT.Q1._2017.Shop.Areas.Admin.Controllers
{
    using System.Data;
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;

    /// <summary>
    /// The currency controller
    /// </summary>
    [Authorize]
    public class CurrencyController : Controller
    {
        #region Fields
        /// <summary>
        /// The currency service
        /// </summary>
        private readonly ICurrencyService _currencyService;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyController"/> class.
        /// </summary>
        /// <param name="currencyService">The currency service</param>
        public CurrencyController(ICurrencyService currencyService)
        {
            this._currencyService = currencyService;
        }
        #endregion

        /// <summary>
        /// List of all currencies
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            var currency = this._currencyService.GetCurrenciesList();
            return this.View(currency);
        }

        /// <summary>
        /// The information on currency
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public ViewResult Details(int currencyId)
        {
            Currency currency = this._currencyService.GetCurrencyInfo(currencyId);
            return this.View(currency);
        }

        /// <summary>
        /// Editing entries in the currency table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int currencyId = 0)
        {
            /*
            if(id == null)
            {
                return HttpNotFound();
            }
            */

            var currency = this._currencyService.GetCurrencyInfo(currencyId);

            if (currency == null)
            {
                return this.HttpNotFound();
            }

            return this.View(currency);
        }

        /// <summary>
        /// Editing entries in the currency table(overloaded action)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Edit(Currency currency)
        {
            if (ModelState.IsValid)
            {
                this._currencyService.Update(currency);
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Remove entries in the currency table
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return this.HttpNotFound();
            }

            this._currencyService.Unregister(id);
            return this.View();
        }

        /// <summary>
        /// Writing in the currency table
        /// </summary>
        /// <param name="currencyViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Currency currency)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._currencyService.Register(currency);
                    return this.RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("Name", "Unable to save changes. Try again, and if the problem persists see your system administrator");
            }

            return this.View(currency);
        }
    }
}