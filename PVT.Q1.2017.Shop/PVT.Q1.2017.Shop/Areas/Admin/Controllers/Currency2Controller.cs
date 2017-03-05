namespace PVT.Q1._2017.Shop.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using ViewModels;

    /// <summary>
    /// The currency2 controller
    /// </summary>
    public class Currency2Controller : Controller
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
        public Currency2Controller(ICurrencyService currencyService)
        {
            if (CurrenciesList == null)
            {
                CurrenciesList = new List<CurrencyViewModel>();
            }

            if (CurrenciesList.Count == 0)
            {
                CurrenciesList.Add(new CurrencyViewModel()
                {
                    Id = 1,
                    FullName = "Euro",
                    Code = 978,
                    Symbol = "e"
                });

                CurrenciesList.Add(new CurrencyViewModel()
                {
                    Id = 2,
                    FullName = "USD",
                    Code = 840,
                    Symbol = "$"
                });

                CurrenciesList.Add(new CurrencyViewModel()
                {
                    Id = 3,
                    FullName = "RUR",
                    Code = 643,
                    Symbol = "r"
                });
            }

            this._currencyService = currencyService;
        }
        #endregion

        /// <summary>
        /// CurrenciesList
        /// </summary>
        public static List<CurrencyViewModel> CurrenciesList { get; set; }

        /// <summary>
        /// List of all currencies
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            // var currency = this._currencyService.GetCurrenciesList();
            return this.View(CurrenciesList);
        }

        /// <summary>
        /// Editing entries in the currency table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // var currency = this._currencyService.GetCurrencyInfo(currencyId);
            var currency = CurrenciesList.FirstOrDefault(x => x.Id == id);

            return this.View(currency);
        }

        /// <summary>
        /// Editing entries in the currency table(overloaded action)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(CurrencyViewModel currency)
        {
            if (ModelState.IsValid)
            {
                // this._currencyService.Update(currency);
                var oldCurrency = CurrenciesList.FirstOrDefault(x => x.Id == currency.Id);
                CurrenciesList.Remove(oldCurrency);
                CurrenciesList.Add(currency);
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Remove entries in the currency table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return this.HttpNotFound();
            }

            // this._currencyService.Unregister(id);
            var currencyForDelete = CurrenciesList.FirstOrDefault(x => x.Id == id);
            CurrenciesList.Remove(currencyForDelete);
            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Create page
        /// </summary>
        /// <param name="currencyViewModel"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return this.View(new CurrencyViewModel());
        }

        /// <summary>
        /// Writing in the currency table
        /// </summary>
        /// <param name="currencyViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CurrencyViewModel currency)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newId = CurrenciesList.Max(x => x.Id) + 1;
                    currency.Id = newId;
                    CurrenciesList.Add(currency);

                    // this._currencyService.Register(currency);
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