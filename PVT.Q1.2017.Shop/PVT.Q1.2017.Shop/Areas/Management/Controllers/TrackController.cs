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
    public class TrackController : Controller
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
            Mapper.Initialize(cfg => cfg.CreateMap<Track, TrackViewModel>());
        }

        /// <summary>
        ///     Gets or sets the repository factory.
        /// </summary>
        public IRepositoryFactory RepositoryFactory { get; set; }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult NewTrack()
        {
            return this.View(new TrackViewModel());
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public ActionResult NewTrack(TrackViewModel model)
        {
            var trackRepo = this.RepositoryFactory.GetTrackRepository();
            var track = Mapper.Map<Track>(model);
             trackRepo.AddOrUpdate(track);
            return null;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public virtual ActionResult AlbumsList()
        {
            using (var repository = this.RepositoryFactory.GetAlbumRepository())
            {
                return this.View(repository.GetAll());
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <returns>
        /// </returns>
        public virtual ActionResult AlbumsList(int artistId)
        {
            using (var repository = this.RepositoryFactory.GetAlbumRepository())
            {
                return this.View(repository.GetAll(a => a.ArtistId.Equals(artistId)));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="trackId">
        ///     The track id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult AlbumsListByTrackId(int trackId)
        {
            using (var repository = this.RepositoryFactory.GetAlbumRepository())
            {
                return this.View(repository.GetAll(a => a.TrackId.Equals(trackId)));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="artistId">
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult ArtistList(int artistId)
        {
            using (var repository = this.RepositoryFactory.GetArtistRepository())
            {
                return this.View(repository.GetAll(m => m.Id.Equals(artistId)));
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public virtual ActionResult ArtistList()
        {
            using (var repository = this.RepositoryFactory.GetArtistRepository())
            {
                return this.View(repository.GetAll());
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// </returns>
        public virtual ActionResult ArtistTracks(int id)
        {
            return this.View(this.trackService.GetTracksWithPriceConfigured());
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// </returns>
        public virtual ActionResult TrackDetails(int trackId)
        {
            return this.View(this.trackService.GetTrackInfo(trackId));
        }

        /// <summary>
        ///     Diplays tracks list.
        /// </summary>
        /// <returns>
        ///     The view that renders tracks list.
        /// </returns>
        public virtual ActionResult TrackList()
        {
            return this.View(this.trackService.GetTracksWithPriceConfigured());
        }

        /// <summary>
        /// </summary>
        /// <param name="trackId">
        ///     The track id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult TrackList(int trackId)
        {
            return this.View(this.trackService.GetTracksWithPriceConfigured());
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="albumId"></param>
        /// <returns>
        /// </returns>
        public virtual ActionResult TrackListByAlbumId(int albumId)
        {
            using (var repository = this.RepositoryFactory.GetTrackRepository())
            {
                return this.View(repository.GetAll(t => t.AlbumId.Equals(albumId)));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="artistId">
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult TrackListByAtistId(int artistId)
        {
            using (var repository = this.RepositoryFactory.GetTrackRepository())
            {
                return this.View(repository.GetAll(t => t.Artist.Id.Equals(artistId)));
            }
        }
    }
}