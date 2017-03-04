namespace PVT.Q1._2017.Shop.Controllers
{
    #region

    using System.Web.Mvc;
    using Infrastructure;
    using Infrastructure.Extensions;
    using ViewModels;

    #endregion

    /// <summary>
    /// Controller of Home page
    /// </summary>
    public partial class HomeController : BaseController
    {
        /// <summary>
        /// Default start page
        /// </summary>
        /// <returns>View of index page</returns>
        public virtual ActionResult Index()
        {
            ////dummy code
            return this.View(new MainViewModel(User.Identity.IdentityToUserDataModel()));
        }
    }
}