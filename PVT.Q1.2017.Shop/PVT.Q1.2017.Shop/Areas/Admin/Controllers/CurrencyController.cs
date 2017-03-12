namespace PVT.Q1._2017.Shop.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
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
        /// <summary>
        /// The repository currency
        /// </summary>
        private readonly ICurrencyRepository _currencyRepository;

        /// <summary>
        /// The service currency
        /// </summary>
        private readonly ICurrencyService _curencyService;

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

        /// <summary>
        /// Index GetCurrenciesList
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            var currencies = this._curencyService.GetCurrenciesList();
            Mapper.Initialize(cfg => cfg.CreateMap<Currency, CurrencyViewModel>());
            var model = Mapper.Map<ICollection<Currency>, IEnumerable<CurrencyViewModel>>(currencies);
            return this.View(model);
        }

        /// <summary>
        /// Edit Currency
        /// </summary>
        /// <param name="id">currency id for edit</param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Edit(int id)
        {
            // var currency = this._curencyService.GetCurrencyInfo(id);
            var currency = this._currencyRepository.GetById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<Currency, CurrencyViewModel>());
            CurrencyViewModel model = Mapper.Map<Currency, CurrencyViewModel>(currency);
            return this.View(model);
        }

        /// <summary>
        /// Edit Currency
        /// </summary>
        /// <param name="model">CurrencyViewModel for edit</param>
        /// <returns></returns>
        public ActionResult Edit(CurrencyViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<CurrencyViewModel, Currency>());
                Currency currency = Mapper.Map<CurrencyViewModel, Currency>(model);
                this._currencyRepository.AddOrUpdate(currency);
                this._currencyRepository.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        /// <summary>
        /// Delete Currency
        /// </summary>
        /// <param name="id">currency id for delete</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            this._currencyRepository.Delete(id);
            this._currencyRepository.SaveChanges();
            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Create Currency
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// Create Currency by CurrencyViewModel
        /// </summary>
        /// <param name="model">CurrencyViewModel for create</param>
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
                    this._currencyRepository.SaveChanges();
                   
                    return this.RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", "Unable to save changes. Try again, and if the problem persists see your system administrator");
            }

            return this.RedirectToAction("Index");
        }
    }
}