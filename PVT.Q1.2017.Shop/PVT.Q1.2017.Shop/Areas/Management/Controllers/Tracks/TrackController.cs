namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    #region

    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL;
    using global::Shop.DAL.Infrastruture;

    using Ninject;

    using PVT.Q1._2017.Shop.Areas.Management.Models;
    using PVT.Q1._2017.Shop.ViewModels;

    #endregion

    /// <summary>
    ///     The track controller
    /// </summary>
    public partial class TrackController : Controller
    {
        /// <summary>
        /// </summary>
        private readonly IKernel _kernel;

        /// <summary>
        ///     The track service.
        /// </summary>
        private readonly ITrackService trackService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TrackController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        public TrackController(IRepositoryFactory repositoryFactory)
        {
            this.RepositoryFactory = repositoryFactory;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TrackController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        /// <param name="trackService">
        ///     The track service.
        /// </param>
        public TrackController(IRepositoryFactory repositoryFactory, ITrackService trackService)
        {
            this.RepositoryFactory = repositoryFactory;
            this.trackService = trackService;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TrackController" /> class.
        /// </summary>
        public TrackController()
        {
            this._kernel = new StandardKernel(new DefaultRepositoriesNinjectModule());
            this.RepositoryFactory = this._kernel.Get<IRepositoryFactory>();
            Mapper.Initialize(cfg => cfg.CreateMap<Track, NewTrackViewModel>());
        }

        /// <summary>
        ///     Gets or sets the repository factory.
        /// </summary>
        public IRepositoryFactory RepositoryFactory { get; set; }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public virtual ActionResult NewTrack()
        {
            return this.View(new NewTrackViewModel());
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public virtual ActionResult NewTrack(NewTrackViewModel model)
        {
            var trackRepo = this.RepositoryFactory.GetTrackRepository();
            var track = Mapper.Map<NewTrackViewModel, Track>(model);
            trackRepo.AddOrUpdate(track);
            return this.View();
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="trackId"></param>
        /// <returns>
        /// </returns>
        public virtual ActionResult TrackInfo(int trackId)
        {
            var track = this.trackService.GetTrackInfo(trackId);
            var trackViewModel = Mapper.Map<Track, NewTrackViewModel>(track);
            return this.View(trackViewModel);
        }
    }
}