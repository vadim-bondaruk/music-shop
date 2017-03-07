namespace PVT.Q1._2017.Shop.Controllers
{
    #region

    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.DAL.Infrastruture;

    #endregion

    /// <summary>
    /// The track controller
    /// </summary>
    public class TrackController : Controller
    {
        #region Fields

        /// <summary>
        /// The repository factory.
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// The track service.
        /// </summary>
        private readonly ITrackService _trackService;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackController"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        /// <param name="trackService">
        ///     The track service.
        /// </param>
        public TrackController(IRepositoryFactory repositoryFactory, ITrackService trackService)
        {
            this._repositoryFactory = repositoryFactory;
            this._trackService = trackService;
        }

        #endregion //Constructors

        #region Actions

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public virtual ActionResult AlbumList()
        {
            using (var repository = this._repositoryFactory.GetAlbumRepository())
            {
                return this.View(repository.GetAll());
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <returns>
        /// </returns>
        public virtual ActionResult AlbumList(int artistId)
        {
            using (var repository = this._repositoryFactory.GetAlbumRepository())
            {
                return this.View(repository.GetAll(a => a.ArtistId.Equals(artistId)));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// </returns>
        public virtual ActionResult AlbumTracks(int id)
        {
            using (var repository = this._repositoryFactory.GetTrackRepository())
            {
                return this.View(repository.GetAll(t => t.AlbumId.Equals(id)));
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public virtual ActionResult ArtistList()
        {
            using (var repository = this._repositoryFactory.GetArtistRepository())
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
            return this.View(this._trackService.GetTracksWithPriceConfigured());
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int id)
        {
            return this.View(this._trackService.GetTrackInfo(id));
        }

        /// <summary>
        /// Diplays tracks list.
        /// </summary>
        /// <returns>
        /// The view that renders tracks list.
        /// </returns>
        public virtual ActionResult TrackList()
        {
            return this.View(this._trackService.GetTracksWithPriceConfigured());
        }

        #endregion //Actions
    }
}