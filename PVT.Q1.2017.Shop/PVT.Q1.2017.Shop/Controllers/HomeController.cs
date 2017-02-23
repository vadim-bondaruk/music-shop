namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// GET: Home
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ////dummy code
            return this.Content("Hello");
        }
    }
}