namespace PVT.Q1._2017.Shop.Areas.Admin.Controllers
{
    using global::Shop.BLL.Services.Infrastructure;
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
        public ActionResult Index()
        {
            var setting = _settingService.GetList();
            return View(setting);
        }
    }
}