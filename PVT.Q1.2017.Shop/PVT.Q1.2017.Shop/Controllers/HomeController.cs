namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Controller of Home page
    /// </summary>
    public class HomeController : Controller
    {
        // GET: Home

        /// <summary>
        /// Default start page
        /// </summary>
        /// <returns>View of index page</returns>
        public ActionResult Index()
        {
            ////dummy code
            return this.Content("Hello");
        }
    }
}