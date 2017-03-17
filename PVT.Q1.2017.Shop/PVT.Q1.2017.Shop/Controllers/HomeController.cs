namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Controller of Home page
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Default start page
        /// </summary>
        /// <returns>View of index page</returns>
        public ActionResult Index()
        {
            return this.RedirectToAction("List", "Track", new { area = "Content" });
        }
    }
}