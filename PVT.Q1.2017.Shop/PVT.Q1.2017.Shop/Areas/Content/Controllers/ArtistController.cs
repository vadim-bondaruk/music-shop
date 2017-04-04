namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;

    /// <summary>
    /// The artist controller.
    /// </summary>
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistController"/> class.
        /// </summary>
        /// <param name="artistService">
        /// The artist service.
        /// </param>
        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        /// <summary>
        /// Shows all artists.
        /// </summary>
        /// <returns>
        /// All artists view.
        /// </returns>
        public ActionResult List()
        {
            return this.View(_artistService.GetArtistsList());
        }

        /// <summary>
        /// Show artist info.
        /// </summary>
        /// <param name="id">
        /// The artist id.
        /// </param>
        /// <returns>
        /// Artist view.
        /// </returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            var artistViewModel = _artistService.GetArtistDetails(id.Value);
            if (artistViewModel == null)
            {
                return HttpNotFound($"Артист с id = { id.Value } не найден");
            }

            return this.View(artistViewModel);
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
        public ActionResult AlbumsList(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            // TODO: передавать currency и price level из UserData текущего пользователя
            var artistAlbumsViewModel = _artistService.GetAlbumsList(id.Value);
            if (artistAlbumsViewModel == null)
            {
                return HttpNotFound($"Исполнитель с id = { id.Value } не найден");
            }

            return this.View(artistAlbumsViewModel);
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
        public ActionResult TracksList(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            // TODO: передавать currency и price level из UserData текущего пользователя
            var artistTracksViewModel = _artistService.GetTracksList(id.Value);
            if (artistTracksViewModel == null)
            {
                return HttpNotFound($"Исполнитель с id = { id.Value } не найден");
            }

            return this.View(artistTracksViewModel);
        }
    }
}