namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;
    using App_Start;
    using global::Shop.Infrastructure.Enums;

    /// <summary>
    /// 
    /// </summary>
    public class AdminTestController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ShopAuthorize(UserRoles.User)]
        public ActionResult Index()
        {
            ViewBag.UserID = this.CurrentUser.Id;
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ShopAuthorize(UserRoles.Admin)]
        public ActionResult IndexAdmin()
        {
            ViewBag.UserID = this.CurrentUser.Id;
            return this.View();
        }
    }
}