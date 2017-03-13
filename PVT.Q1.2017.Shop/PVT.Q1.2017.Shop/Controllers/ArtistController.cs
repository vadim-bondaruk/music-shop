namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;
    using global::Shop.DAL.Infrastruture;

    /// <summary>
    /// The artist controller.
    /// </summary>
    public partial class ArtistController : Controller
    {
        /// <summary>
        /// The repository factory.
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistController"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        public ArtistController(IRepositoryFactory repositoryFactory)
        {
            this._repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Shows all artists.
        /// </summary>
        /// <returns>
        /// All artists view.
        /// </returns>
        public virtual ActionResult Index()
        {
            using (var repository = this._repositoryFactory.GetArtistRepository())
            {
                return this.View(repository.GetAll());
            }
        }

        /// <summary>
        /// Show artist info.
        /// </summary>
        /// <param name="id">
        /// The artist id.
        /// </param>
        /// <returns>
        /// Artist info view.
        /// </returns>
        public virtual ActionResult Details(int id)
        {
            using (var repository = this._repositoryFactory.GetArtistRepository())
            {
                return this.View(repository.GetById(id));
            }
        }

        /// <summary>
        /// Shows all artist albums
        /// </summary>
        /// <param name="id">
        /// The artist id.
        /// </param>
        /// <returns>
        /// All artist albums view.
        /// </returns>
        public virtual ActionResult AlbumsList(int id)
        {
            using (var repository = this._repositoryFactory.GetAlbumRepository())
            {
                return this.View(repository.GetAll(a => a.ArtistId == id));
            }
        }

        /// <summary>
        /// Shows all artist tracks.
        /// </summary>
        /// <param name="id">
        /// The artist id.
        /// </param>
        /// <returns>
        /// All artist tracks view.
        /// </returns>
        public virtual ActionResult TracksList(int id)
        {
            using (var repository = this._repositoryFactory.GetTrackRepository())
            {
                return this.View(repository.GetAll(t => t.ArtistId == id));
            }
        }
    }
}