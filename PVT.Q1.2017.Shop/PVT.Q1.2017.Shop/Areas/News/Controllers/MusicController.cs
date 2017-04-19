namespace PVT.Q1._2017.Shop.Areas.News.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// </summary>
    public class NewsController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Article()
        {
            this.ViewBag.Title = "Новости музыки";
            return this.View("_NewAlbumOfDorn");
        }
    }
}