namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;
    using BLL.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        public CurrentUser CurrentUser
        {
            get
            {
                return this.HttpContext.User as CurrentUser;
            }
        }
    }
}