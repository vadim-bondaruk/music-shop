namespace PVT.Q1._2017.Shop.Controllers
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure.Repositories;

    using Ninject;

    using PVT.Q1._2017.Shop.Models;

    #endregion

    /// <summary>
    ///     Controller of Home page
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// </summary>
        private readonly IDisposableRepository<Track> _tracksRepository;

        /// <summary>
        ///     <para>
        ///         Initializes a new instance of the <see cref="HomeController" />
        ///     </para>
        ///     <para>class.</para>
        /// </summary>
        public HomeController()
        {
            var kernel = new StandardKernel(new DefaultServicesNinjectModule());
            var repositoryFactory = kernel.Get<IRepositoryFactory>();
            this._tracksRepository = repositoryFactory.CreateRepository<Track>();
            Mapper.Map<IEnumerable<Track>, List<TrackViewModel>>(this._tracksRepository.GetAll(c => !c.IsDeleted));
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        /// <exception cref="Exception">
        /// EmptyCollection
        /// </exception>
        public ActionResult Index()
        {
            var tracks = this._tracksRepository.GetAll();
            if (tracks.Count == 0)
            {
                throw new Exception("EmptyCollection!");
            }

            return this.View("Index");
        }
    }
}