namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Redirect to this controller if error
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// To show unauthorized error message
        /// </summary>
        /// <returns></returns>
        public ActionResult Unauthorized()
        {
            return this.View();
        }

        /// <summary>
        /// To show blocked error message
        /// </summary>
        /// <returns></returns>
        public ActionResult Blocked()
        {
            return this.View();
        }

        /// <summary>
        /// To show unconfirmed error message
        /// </summary>
        /// <returns></returns>
        public ActionResult Unconfirmed()
        {
            return this.View();
        }

        /// <summary>
        /// To show register error message
        /// </summary>
        /// <returns></returns>
        public ActionResult RegisterError()
        {
            return this.View();
        }

        /// <summary>
        /// To show invalid request parameter error message
        /// </summary>
        /// <returns></returns>
        public ActionResult InvalidRequestParameter()
        {
            return this.View();
        }

        /// <summary>
        /// To show not found error message
        /// </summary>
        /// <param name="aspxerrorpath"></param>
        /// <returns></returns>
        public ActionResult NotFound(string aspxerrorpath)
        {
            return this.View();
        }

        /// <summary>
        /// To show inner exception error message
        /// </summary>
        /// <returns></returns>
        public ActionResult InnerException()
        {
            return this.View();
        }
    }
}