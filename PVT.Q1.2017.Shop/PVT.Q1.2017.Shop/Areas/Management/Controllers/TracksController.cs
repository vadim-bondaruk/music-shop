namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.Models.ViewModels;
    using global::Shop.DAL.Infrastruture;


    /// <summary>
    ///     The track controller
    /// </summary>
    public class TracksController : Controller
    {
        /// <summary>
        ///     The track service.
        /// </summary>
        private readonly ITrackService trackService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TracksController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        public TracksController(IRepositoryFactory repositoryFactory)
        {
            this.RepositoryFactory = repositoryFactory;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TracksController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        /// <param name="trackService">
        ///     The track service.
        /// </param>
        public TracksController(IRepositoryFactory repositoryFactory, ITrackService trackService)
        {
            this.RepositoryFactory = repositoryFactory;
            this.trackService = trackService;
            Mapper.Initialize(cfg => cfg.CreateMap<TrackManagementViewModel, Track>());
        }

        /// <summary>
        ///     Gets or sets the repository factory.
        /// </summary>
        public IRepositoryFactory RepositoryFactory { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="trackId">
        ///     The track id.
        /// </param>
        /// <param name="model"></param>
        /// <returns>
        /// </returns>
        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Delete(TrackManagementViewModel model)
        {
            var trackModel = Mapper.Map<TrackManagementViewModel, Track>(model);
            using (var repository = this.RepositoryFactory.GetTrackRepository())
            {
                repository.Delete(trackModel);
                repository.SaveChanges();
            }

            return this.View("New");
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="trackId"></param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int trackId)
        {
            //var track = this.trackService.GetTrackInfo(trackId);
            //var trackViewModel = Mapper.Map<Track, TrackManagementViewModel>(track);
            return this.View();
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult New()
        {
            return this.View("New");
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel">
        /// The view model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult New(
            [Bind(Include = "Artist, Name, Album, Genre, Duration, ReleaseDate")] TrackManagementViewModel viewModel)
        {
            var track = Mapper.Map<Track>(viewModel);
            using (var repository = this.RepositoryFactory.GetTrackRepository())
            {
                repository.AddOrUpdate(track);
                repository.SaveChanges();
            }

            return this.View("New");
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public virtual ActionResult Update(TrackManagementViewModel model)
        {
            var trackRepo = this.RepositoryFactory.GetTrackRepository();
            var track = Mapper.Map<TrackManagementViewModel, Track>(model);
            trackRepo.AddOrUpdate(track);
            return this.View();
        }
    }
}