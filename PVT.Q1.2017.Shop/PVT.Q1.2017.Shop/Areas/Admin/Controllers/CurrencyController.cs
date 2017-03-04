namespace PVT.Q1._2017.Shop.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using System.Data.Entity;
    using System.Data;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Repositories.Infrastruture;
    using global::Shop.Infrastructure;
    using global::Shop.Infrastructure.Services;
    using global::Shop.Infrastructure.Repositories;
    using global::Shop.DAL.Context;
    using Shop.ViewModels;

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
        public CurrencyController(IRepository<Currency> currencyRepo, IRepository<CurrencyRate> currencyRateRepo )
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
            var currencyis = _currencyRepository.GetAll(x=> x.CurrencyRates);

            var curViewMOdel = currencyis.Select(x => new CurrencyViewModel()
            {
                FullName = x.FullName,
                ShortName = x.ShortName,
                CrossCurce = x.CurrencyRates.Select(y => new CurrencyViewModel.CrossCurrencyViewModel() { }).ToList()
            });

            //CurrencyViewModel cur = new CurrencyViewModel();
            //cur.FullName 


            // cyrrency and cyrrencyRate
            //var currency = DbContext.Currencies.Include(x => x.CurrencyRates);
            return View(curViewMOdel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        //{
        //    var currencyRate = DbContext.CurrencyRates
        //        .Where(x => x.CurrencyId == id)
        //        .FirstOrDefault();

        //    if(currencyRate == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    ViewBag.CurrencyId = new SelectList(DbContext.Currencies, "Id", "Name", currencyRate.CurrencyId);
        //    ViewBag.CurrencyRateId = new SelectList(DbContext.CurrencyRates, "Id", "Name", currencyRate.Id);

        //    return this.View(currencyRate);
        }

        /// <summary>
        /// Перегруженная версия
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(CurrencyRate currencyRate)
        {
            //if (ModelState.IsValid)
            //{
            //    _repository.AddOrUpdate(currencyRate);
            //    return RedirectToAction("Index");
            //}

            //return View(currencyRate);
        }


        public ActionResult Delete()
        {
            return View();
        }


    }
}