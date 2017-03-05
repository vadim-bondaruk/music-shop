namespace PVT.Q1._2017.Shop.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Shop.ViewModels;

    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class CurrencyController : Controller
    {
        #region Fields

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currencyViewModel"></param>
        /// <returns></returns>
        public ActionResult Add(CurrencyViewModel currencyViewModel)
        {
            return this.View();
        }
    }
}