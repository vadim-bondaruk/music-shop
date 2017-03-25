namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;

    using global::Shop.BLL.Helpers;
    using global::Shop.Common.Models.ViewModels;
    using global::Shop.DAL.Infrastruture;

    /// <summary>
    ///     The artist controller.
    /// </summary>
    public class ArtistsController : Controller
    {
        /// <summary>
        ///     The repository factory.
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArtistsController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        public ArtistsController(IRepositoryFactory repositoryFactory)
        {
            this._repositoryFactory = repositoryFactory;
        }

        /// <summary>
        ///     Shows all artist albums
        /// </summary>
        /// <param name="id">
        ///     The artist id.
        /// </param>
        /// <returns>
        ///     All artist albums view.
        /// </returns>
        public ActionResult AlbumsList(int id)
        {
            using (var repository = this._repositoryFactory.GetAlbumRepository())
            {
                return this.View(repository.GetAll(a => a.ArtistId == id));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult Details(int id)
        {
            using (var repository = this._repositoryFactory.GetArtistRepository())
            {
                var viewModel = ModelsMapper.GetArtistDetailsViewModel(repository.GetById(id));
                return this.View(viewModel);
            }
        }

        /// <summary>
        ///     Shows all artists.
        /// </summary>
        /// <returns>
        ///     All artists view.
        /// </returns>
        public ActionResult Index()
        {
            using (var repository = this._repositoryFactory.GetArtistRepository())
            {
                return this.View(repository.GetAll());
            }
        }

        /// <summary>
        ///     Shows all artist tracks.
        /// </summary>
        /// <param name="id">
        ///     The artist id.
        /// </param>
        /// <returns>
        ///     All artist tracks view.
        /// </returns>
        public ActionResult TracksList(int id)
        {
            using (var repository = this._repositoryFactory.GetTrackRepository())
            {
                return this.View(repository.GetAll(t => t.ArtistId == id));
            }
        }
    }
}