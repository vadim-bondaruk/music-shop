namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Controller of Home page
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// <returns>Default start page</returns>
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
            ViewBag.Message = "Страница описания приложения";

            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Контакты";

            return this.View();
        }
    }
}