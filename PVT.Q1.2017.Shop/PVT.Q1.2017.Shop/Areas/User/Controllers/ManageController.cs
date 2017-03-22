namespace PVT.Q1._2017.Shop.Areas.User.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// 
    /// </summary>
    public class ManageController : Controller
    {
        /// <summary>
        /// GET: User/Manage/Index
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Редактировать аккаунт";
            return this.View();
        }
    }
}