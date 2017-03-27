namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    using PVT.Q1._2017.Shop.Areas.Management.Helpers;
    using PVT.Q1._2017.Shop.Areas.Management.ViewModels;

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
        /// </summary>
        private readonly IArtistService artistService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TracksController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        /// <param name="artistService"></param>
        public TracksController(IRepositoryFactory repositoryFactory, IArtistService artistService)
        {
            this.RepositoryFactory = repositoryFactory;
            this.artistService = artistService;
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
        /// <param name="artistService"></param>
        public TracksController(
            IRepositoryFactory repositoryFactory,
            ITrackService trackService,
            IArtistService artistService)
        {
            this.RepositoryFactory = repositoryFactory;
            this.trackService = trackService;
            this.artistService = artistService;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        /// <returns>
        /// </returns>
        public virtual ActionResult Edit()
        {
            return this.View();
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(int id)
        {
            return this.View();
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult New(int id)
        {
            var artist = this.artistService.GetArtist(id);
            //artist.IsCreation = true;
            return this.View(new TrackManagementViewModel() { Artist = artist });
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel">
        ///     The view model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult New(TrackManagementViewModel viewModel)
        {
            var track = ManagementMapper.GetTrackModel(viewModel);
            using (var repository = this.RepositoryFactory.GetTrackRepository())
            {
                repository.AddOrUpdate(track);
                repository.SaveChanges();
            }

            return this.View();
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