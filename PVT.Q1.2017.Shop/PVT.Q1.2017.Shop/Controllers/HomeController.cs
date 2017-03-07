namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;
    using Infrastructure;
    using Infrastructure.Extensions;
    using ViewModels;

    /// <summary>
    /// Controller of Home page
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// Default start page
        /// </summary>
        /// <returns>View of index page</returns>
        public ActionResult Index()
        {
            ////dummy code
            return this.View(new MainViewModel(User.Identity.IdentityToUserDataModel()));
        }
    }
}