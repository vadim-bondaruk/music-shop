namespace PVT.Q1._2017.Shop.Areas.Admin.Controllers
{
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using System.Web.Mvc;

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

        // GET: Admin/Setting
        [HttpGet]
        public ActionResult Index()
        {
            var setting = _settingService.GetSettingViewModel();
            return this.View(setting);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(SettingViewModel model)
        {
            var setting = _settingService.GetSettingViewModel();
            return View(setting);
        }
    }
}