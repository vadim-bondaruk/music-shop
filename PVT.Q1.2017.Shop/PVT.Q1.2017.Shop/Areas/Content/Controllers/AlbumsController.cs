namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models.ViewModels;

    /// <summary>
    ///     The album controller.
    /// </summary>
    public class AlbumsController : Controller
    {
        /// <summary>
        ///     The album service.
        /// </summary>
        private readonly IAlbumService _albumService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AlbumsController" /> class.
        /// </summary>
        /// <param name="albumService">
        ///     The album service.
        /// </param>
        public AlbumsController(IAlbumService albumService)
        {
            this._albumService = albumService;
        }

        /// <summary>
        /// </summary>
        /// <param name="albumId">
        /// The album id.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult Details(int? albumId)
        {
            if (albumId == null)
            {
                return this.RedirectToAction("List");
            }

            return this.View(this._albumService.GetAlbum(albumId.Value));
        }

        /// <summary>
        ///     Shows all albums.
        /// </summary>
        /// <returns>
        ///     All albums view.
        /// </returns>
        public ActionResult List()
        {
            return this.View(this._albumService.GetAlbumsList());
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel">
        ///     The view model.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult New(AlbumManageViewModel viewModel)
        {
            var id = this._albumService.SaveNewAlbum(viewModel);
            return this.RedirectToAction("Details", new { albumId = id });
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

            return this.View(this._albumService.GetTracksList(id.Value));
        }
    }
}