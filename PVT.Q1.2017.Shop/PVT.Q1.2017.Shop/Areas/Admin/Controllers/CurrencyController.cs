namespace PVT.Q1._2017.Shop.Areas.Admin.Controllers
{
    using System;
    using System.Data;
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.ViewModels.Admin;
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
            var currencies = this._curencyService.GetCurrenciesList();
            return this.View(currencies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var currency = this._curencyService.GetCurrencyInfo(id);
            return this.View(currency);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(CurrencyViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<CurrencyViewModel, Currency>());
                Currency currency = Mapper.Map<CurrencyViewModel, Currency>(model);
                this._currencyRepository.AddOrUpdate(currency);
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            this._currencyRepository.Delete(id);
            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       [HttpGet]
        public ActionResult Create()
        {
            return this.View(new CurrencyViewModel());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CurrencyViewModel model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<CurrencyViewModel, Currency>());
                    Currency currency = Mapper.Map<CurrencyViewModel, Currency>(model);
                    this._currencyRepository.AddOrUpdate(currency);
                   
                    return this.RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", "Unable to save changes. Try again, and if the problem persists see your system administrator");
            }

            return this.RedirectToAction("Index");
        }

        #endregion
    }
}