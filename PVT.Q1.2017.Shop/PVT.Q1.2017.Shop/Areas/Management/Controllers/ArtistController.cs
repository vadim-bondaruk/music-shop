namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    using PVT.Q1._2017.Shop.Areas.Management.Models;

    /// <summary>
    ///     The track controller
    /// </summary>
    public class ArtistsController : Controller
    {
        /// <summary>
        ///     The track service.
        /// </summary>
        private readonly IArtistRepository artistRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TracksController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        public ArtistsController(IRepositoryFactory repositoryFactory, IArtistRepository artistRepository)
        {
            this.RepositoryFactory = repositoryFactory;
            this.artistRepository = artistRepository;
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
        public ArtistsController(
            IRepositoryFactory repositoryFactory,
            ITrackService trackService,
            IArtistRepository artistRepository)
        {
            this.RepositoryFactory = repositoryFactory;
            this.artistRepository = artistRepository;
            Mapper.Initialize(cfg => cfg.CreateMap<TrackManagmentViewModel, Track>());
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
        public virtual ActionResult Delete(ArtistManagmentViewModel model)
        {
            var artistModel = Mapper.Map<ArtistManagmentViewModel, Artist>(model);
            using (var repository = this.RepositoryFactory.GetArtistRepository())
            {
                repository.Delete(artistModel);
                repository.SaveChanges();
            }

            return this.View("ArtistManage");
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="artistId"></param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int artistId)
        {
            var artist = this.artistRepository.GetById(artistId);
            Mapper.Map<ArtistManagmentViewModel>(artist);
            return this.View("ArtistManage");
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
            return this.View("ArtistManage");
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
        public virtual ActionResult New(
            [Bind(Include = "Name, Biography, Birthday, Photo")] ArtistManagmentViewModel viewModel)
        {
            var track = Mapper.Map<Artist>(viewModel);
            this.artistRepository.AddOrUpdate(track);
            this.artistRepository.SaveChanges();
            return this.View("ArtistManage");
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public virtual ActionResult Update(TrackManagmentViewModel model)
        {
            var trackRepo = this.RepositoryFactory.GetTrackRepository();
            var track = Mapper.Map<TrackManagmentViewModel, Track>(model);
            trackRepo.AddOrUpdate(track);
            return this.View();
        }
    }
}