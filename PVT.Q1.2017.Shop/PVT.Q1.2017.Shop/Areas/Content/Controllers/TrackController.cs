namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;

    /// <summary>
    /// The track controller
    /// </summary>
    public class TrackController : Controller
    {
        /// <summary>
        /// The track service.
        /// </summary>
        private readonly ITrackService _trackService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackController"/> class.
        /// </summary>
        /// <param name="trackService">
        ///     The track service.
        /// </param>
        public TrackController(ITrackService trackService)
        {
            this._trackService = trackService;
        }

        /// <summary>
        /// Shows all tracks.
        /// </summary>
        /// <returns>
        /// All tracks view.
        /// </returns>
        public ActionResult List()
        {
            // TODO: передавать currency и price level из UserData текущего пользователя
            return this.View(this._trackService.GetTracksList());
        }

        /// <summary>
        /// Shows track info.
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        /// Track info view.
        /// </returns>
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            // TODO: передавать currency и price level из UserData текущего пользователя
            var trackViewModel = _trackService.GetTrackDetails(id.Value);
            if (trackViewModel == null)
            {
                return HttpNotFound($"Трек с id = { id.Value } не найден");
            }

            return this.View(trackViewModel);
        }

        /// <summary>
        /// Shows all albums where the specified track is exist.
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        /// All albums where the specified track is exist.
        /// </returns>
        public virtual ActionResult AlbumsList(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            // TODO: передавать currency и price level из UserData текущего пользователя
            var trackAlbumsViewModel = _trackService.GetAlbumsList(id.Value);
            if (trackAlbumsViewModel == null)
            {
                return HttpNotFound($"Трек с id = { id.Value } не найден");
            }

            return this.View(trackAlbumsViewModel);
        }
    }
}