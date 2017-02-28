﻿namespace PVT.Q1._2017.Shop.Controllers
{
    #region

    using System.Web.Mvc;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure.Repositories;
    using global::Shop.Infrastructure.Services;
    using PVT.Q1._2017.Shop.Models;

    #endregion

    /// <summary>
    /// Controller of Home page
    /// </summary>
    public partial class HomeController : Controller
    {
        #region Fields

        /// <summary>
        /// The repository factory.
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// The tracks service.
        /// </summary>
        private readonly IService<Track> _trackService;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="trackService">
        /// The track service.
        /// </param>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        public HomeController(IService<Track> trackService, IRepositoryFactory repositoryFactory)
        {
            this._trackService = trackService;
            this._repositoryFactory = repositoryFactory;
        }

        #endregion //Constructors

        #region Actions

        /// <summary>
        /// </summary>
        /// <returns>View of index page</returns>
        public virtual ActionResult Index()
        {
            using (var repository = this._repositoryFactory.CreateRepository<Track>())
            {
                return this.View(repository.GetAll());
            }
        }

        #endregion //Actions
    }
}