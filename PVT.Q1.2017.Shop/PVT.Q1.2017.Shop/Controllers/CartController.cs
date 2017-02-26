namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Controller of Cart page
    /// </summary>
    public class CartController : Controller
    {
        /// <summary>
        /// Default start page of Cart
        /// </summary>
        /// <returns>View of index page</returns>
        public ActionResult Index()
        {
            return this.View();
        }
    }
}