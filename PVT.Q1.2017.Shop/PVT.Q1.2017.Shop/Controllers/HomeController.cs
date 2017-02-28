namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure.Repositories;

    /// <summary>
    /// Controller of Home page
    /// </summary>
    public class HomeController : Controller
    {
        #region Fields

        /// <summary>
        /// The repository factory.
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        public HomeController(IRepositoryFactory repositoryFactory)
        {
            this._repositoryFactory = repositoryFactory;
        }

        #endregion //Constructors

        #region Actions

        /// <summary>
        /// Default start page
        /// </summary>
        /// <returns>View of index page</returns>
        public ActionResult Index()
        {
            using (var repository = this._repositoryFactory.CreateRepository<Track>())
            {
                return this.View(repository.GetAll());
            }
        }

        #endregion //Actions
    }
}