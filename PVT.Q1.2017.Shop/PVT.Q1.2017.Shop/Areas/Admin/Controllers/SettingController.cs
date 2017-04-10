namespace PVT.Q1._2017.Shop.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;

    /// <summary>
    /// 
    /// </summary>
    public class SettingController : Controller
    {
        /// <summary>
        /// The service setting
        /// </summary>
        private readonly ISettingService _settingService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingService"></param>
        public SettingController(ISettingService settingService)
        {
            this._settingService = settingService;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpGet]
        public ActionResult Index()
        {
            var setting = this._settingService.GetSettingViewModel();
            return this.View(setting);
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="defaultCurrencyId"></param>
        /// <returns>
        /// </returns>
        [HttpGet]
        public JsonResult Save(int defaultCurrencyId)
        {
            this._settingService.SaveSettingViewModel(defaultCurrencyId);
            return this.Json("Saved", JsonRequestBehavior.AllowGet);
        }
    }
}