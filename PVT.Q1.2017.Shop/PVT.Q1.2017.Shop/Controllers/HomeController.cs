// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="PurpleTeam">
//   PurpleTeam
// </copyright>
// <summary>
//   Controller of Home page
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PVT.Q1._2017.Shop.Controllers
{
    #region

    using System.Web.Mvc;

    #endregion

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