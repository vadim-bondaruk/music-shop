namespace PVT.Q1._2017.Shop.Controllers
{
    using BLL.Utils;
    using System.Web.Mvc;


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