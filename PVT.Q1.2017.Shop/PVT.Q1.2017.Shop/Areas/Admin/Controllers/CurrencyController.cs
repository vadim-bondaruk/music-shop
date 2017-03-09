namespace PVT.Q1._2017.Shop.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Data;
    using System.Web.Mvc;
    using AutoMapper;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using Shop.ViewModels;

    /// <summary>
    /// The currency controller
    /// </summary>
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
            Mapper.Initialize(x => x.CreateMap<Currency, CurrencyViewModel>());
            var currency = Mapper.Map<IEnumerable<Currency>, List<CurrencyViewModel>>(this._currencyService.GetCurrenciesList());
            return this.View(currency);
        }

        /// <summary>
        /// The information on currency
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public ViewResult Details(int currencyId)
        {
            Mapper.Initialize(x => x.CreateMap<Currency, CurrencyViewModel>());
            var currency = Mapper.Map<Currency, CurrencyViewModel>(this._currencyService.GetCurrencyInfo(currencyId));
            return this.View(currency);
        }

        /// <summary>
        /// Editing entries in the currency table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int currencyId = 0)
        {
            /*
            if(id == null)
            {
                return HttpNotFound();
            }
            */

            Mapper.Initialize(x => x.CreateMap<Currency, CurrencyViewModel>());
            var currency = Mapper.Map<Currency, CurrencyViewModel>(this._currencyService.GetCurrencyInfo(currencyId));

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
        [HttpPost]
        public ActionResult Edit(CurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(x => x.CreateMap<CurrencyViewModel, Currency>());
                Currency currency = Mapper.Map<CurrencyViewModel, Currency>(model);
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

            var currency = this._currencyService.Unregister(id);
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
        /// Writing in the currency table
        /// </summary>
        /// <param name="currencyViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CurrencyViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Mapper.Initialize(x => x.CreateMap<Currency, CurrencyViewModel>());
                    Currency currency = Mapper.Map<CurrencyViewModel, Currency>(model);
                    this._currencyService.Register(currency);
                    return this.RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("Name", "Unable to save changes. Try again, and if the problem persists see your system administrator");
            }

            return this.View(model);
        }
    }
}