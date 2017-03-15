namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;

    /// <summary>
    /// The album controller.
    /// </summary>
    public class AlbumController : Controller
    {
        /// <summary>
        /// The album service.
        /// </summary>
        private readonly IAlbumService _albumService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumController"/> class.
        /// </summary>
        /// <param name="albumService">
        /// The album service.
        /// </param>
        public AlbumController(IAlbumService albumService)
        {
            this._albumService = albumService;
        }

        /// <summary>
        /// Shows all albums.
        /// </summary>
        /// <returns>
        /// All albums view.
        /// </returns>
        public ActionResult List()
        {
            return this.View(this._albumService.GetAlbumsList());
        }

        /// <summary>
        /// Shows album info.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>
        /// Album view.
        /// </returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            return this.View(this._albumService.GetAlbum(id.Value));
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

            return this.View(this._albumService.GetTracksList(new Album { Id = id.Value }));
        }
    }
}