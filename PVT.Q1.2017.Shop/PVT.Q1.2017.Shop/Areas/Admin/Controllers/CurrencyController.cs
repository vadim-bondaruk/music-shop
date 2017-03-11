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
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Mapper.Initialize(x => x.CreateMap<Currency, IndexCurrencyViewModel>());
            var currency = Mapper.Map<ICollection<Currency>, List<IndexCurrencyViewModel>>(this._curencyService.GetCurrenciesList());
            return this.View(currency);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            Mapper.Initialize(x => x.CreateMap<Currency, EditCurrencyViewModel>());
            var currency = Mapper.Map<Currency, EditCurrencyViewModel>(this._currencyRepository.GetById(id));
            return this.View(currency);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(EditCurrencyViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                Mapper.Initialize(x => x.CreateMap<EditCurrencyViewModel, Currency>());
                Currency currency = Mapper.Map<EditCurrencyViewModel, Currency>(model);
                this._currencyRepository.AddOrUpdate(currency);
                this._currencyRepository.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(model);
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
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CreateCurrencyViewModel model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<CreateCurrencyViewModel, Currency>());
                    var currency = Mapper.Map<CreateCurrencyViewModel, Currency>(model);
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