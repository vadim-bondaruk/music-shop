namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;
    using App_Start;
    using global::Shop.Infrastructure.Enums;

    /// <summary>
    /// Authorization Testing
    /// </summary>
    public class AdminTestController : BaseController
    {
        /// <summary>
        /// Only for user
        /// </summary>
        /// <returns></returns>
        [ShopAuthorize(UserRoles.Customer)]
        public ActionResult UserTest()
        {
            ViewBag.Id = this.CurrentUser.Id;
            return this.View();
        }

        /// <summary>
        /// Only for admin
        /// </summary>
        /// <returns></returns>
        [ShopAuthorize(UserRoles.Admin)]
        public ActionResult AdminTest()
        {
            ViewBag.Id = this.CurrentUser.Id;
            return this.View();
        }
    }
}