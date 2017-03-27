namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;

    using global::Shop.BLL.Helpers;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    /// <summary>
    ///     The album controller.
    /// </summary>
    public class AlbumsController : Controller
    {
        /// <summary>
        ///     The album service.
        /// </summary>
        private readonly IAlbumService albumService;

        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AlbumsController" /> class.
        /// </summary>
        /// <param name="albumService">
        ///     The album service.
        /// </param>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        public AlbumsController(IAlbumService albumService, IRepositoryFactory repositoryFactory)
        {
            this.albumService = albumService;
            this.repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int id)
        {
            Album album;
            using (var albumRepo = this.repositoryFactory.GetAlbumRepository())
            {
                album = albumRepo.GetById(id);
            }

            var viewModel = ModelsMapper.GetAlbumDetailsViewModel(album);

            using (var artistRepo = this.repositoryFactory.GetArtistRepository())
            {
                if ((album.ArtistId == null))
                {
                    return this.View(viewModel);
                }

                var artistId = album.ArtistId ?? default(int);
                var artist = artistRepo.GetById(artistId);
                if (artist != null)
                {
                    viewModel.ArtistName = artist.Name;
                }
            }

            return this.View(viewModel);
        }

        /// <summary>
        ///     Shows all albums.
        /// </summary>
        /// <returns>
        ///     All albums view.
        /// </returns>
        public ActionResult List()
        {
            return this.View(this.albumService.GetAlbumsList());
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>
        /// </returns>
        public virtual ActionResult TracksList(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            return this.View(this.albumService.GetTracksList(id.Value));
        }
    }
}