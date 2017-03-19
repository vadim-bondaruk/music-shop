namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.Validators;
    using global::Shop.DAL.Infrastruture;
    using ViewModels;

    /// <summary>
    /// Currency controller
    /// </summary>
    public class CurrencyController : Controller
    {
        /// <summary>
        /// for compilation
        /// </summary>
        private readonly ICurrencyRateService _rateService;

        /// <summary>
        /// for compilation
        /// </summary>
        private readonly ICurrencyService _currenycService;

        /// <summary>
        /// for compilation
        /// </summary>
        private readonly IRepositoryFactory _factory;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="rateService"></param>
        /// <param name="currenycService"></param>
        public CurrencyController(
            ICurrencyRateService rateService,
            ICurrencyService currenycService,
            IRepositoryFactory factory)
        {
            this._rateService = rateService;
            this._currenycService = currenycService;
            this._factory = factory;
        }

        /// <summary>
        /// Start page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Return all currencies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult All()
        {
            using (var repo = this._factory.GetCurrencyRepository())
            {
                return this.View(repo.GetAll());
            }
        }

        /// <summary>
        /// Return list or rates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Rates()
        {
            using (var curRepo = this._factory.GetCurrencyRepository())
            using (var rateRepo = this._factory.GetCurrencyRateRepository())
            {
                return this.View(rateRepo.GetAll().Select(r => new RateViewModel
                {
                    Id = r.Id,
                    FromId = r.CurrencyId,
                    ToId = r.TargetCurrencyId,
                    Date = r.Date,
                    Value = r.CrossCourse,
                    From = curRepo.GetAll(),
                    To = curRepo.GetAll()
                }).ToList());
            }
        }

        /// <summary>
        /// Add or update currency
        /// </summary>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddOrUpdateCurrency(Currency currency)
        {
            if (new CurrencyValidator().Validate(currency).IsValid)
            {
                using (var repo = this._factory.GetCurrencyRepository())
                {
                    repo.AddOrUpdate(currency);
                    repo.SaveChanges();
                    return this.RedirectToAction("All");
                }
            }

            return this.View("EditCurrency", currency);
        }

        /// <summary>
        /// Currenyc editor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CurrencyEditor(int? id)
        {
            if (id.HasValue)
            {
                using (var repo = this._factory.GetCurrencyRepository())
                {
                    return this.View("EditCurrency", repo.GetById(id.Value));
                }
            }

            return this.View("CreateCurrency", new Currency());
        }

        /// <summary>
        /// Delete currency
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteCurrency(int id)
        {
            using (var repo = this._factory.GetCurrencyRepository())
            {
                repo.Delete(id);
                repo.SaveChanges();
                return this.RedirectToAction("All");
            }
        }

        /// <summary>
        /// Add rate
        /// </summary>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddRate(RateViewModel rate)
        {
            if (ModelState.IsValid)
            {
                using (var repo = this._factory.GetCurrencyRateRepository())
                {
                    repo.AddOrUpdate(new CurrencyRate
                    {
                        CurrencyId = rate.FromId,
                        TargetCurrencyId = rate.ToId,
                        CrossCourse = rate.Value,
                        Date = rate.Date
                    });
                    repo.SaveChanges();
                    return this.RedirectToAction("Rates");
                }
            }

            using (var repo = this._factory.GetCurrencyRepository())
            {
                rate.From = repo.GetAll();
                rate.To = repo.GetAll();
            }

            return this.View("AddRate", rate);
        }

        /// <summary>
        /// Add new rate
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddRate()
        {
            using (var repo = this._factory.GetCurrencyRepository())
            {
                return this.View(new RateViewModel
                {
                    Date = DateTime.Now.Date,
                    From = repo.GetAll(),
                    To = repo.GetAll()
                });
            }
        }

        /// <summary>
        /// Remove rate
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteRate(int id)
        {
            using (var repo = this._factory.GetCurrencyRateRepository())
            {
                repo.Delete(id);
                repo.SaveChanges();
                return this.RedirectToAction("Rates");
            }
        }
    }
}