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
        /// </summary>
        /// <returns>View of index page</returns>
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Message = "Ваш логин: " + User.Identity.Name;
            }
            else
            {
                ViewBag.Message = "Вы не авторизованы";
            }

            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}