using System.Web.Mvc;

namespace PVT.Q1._2017.Shop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return Content("Hello");
        }
    }
}