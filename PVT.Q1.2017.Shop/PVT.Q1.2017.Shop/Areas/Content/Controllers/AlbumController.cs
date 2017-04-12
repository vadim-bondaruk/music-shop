namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using Shop.Controllers;

    /// <summary>
    /// The album controller.
    /// </summary>
    public class AlbumController : BaseController
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
            var currency = GetCurrentUserCurrency();
            if (currency != null)
            {
                var priceLevel = GetCurrentUserPriceLevel();
                return this.View(this._albumService.GetAlbumsList(currency.Code, priceLevel, GetUserDataId()));
            }

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

            var currency = GetCurrentUserCurrency();
            AlbumDetailsViewModel albumViewModel;

            if (currency != null)
            {
                var priceLevel = GetCurrentUserPriceLevel();
                albumViewModel = _albumService.GetAlbumDetails(id.Value, currency.Code, priceLevel, GetUserDataId());
            }
            else
            {
                albumViewModel = _albumService.GetAlbumDetails(id.Value);
            }

            if (albumViewModel == null)
            {
                return HttpNotFound($"Альбом с id = { id.Value } не найден");
            }

            return this.View(albumViewModel);
        }

        /// <summary>
        /// Shows all tracks from the album.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>
        /// Album with tracks view.
        /// </returns>
        public ActionResult TracksList(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            var currency = GetCurrentUserCurrency();
            AlbumTracksListViewModel albumTracksViewModel;

            if (currency != null)
            {
                var priceLevel = GetCurrentUserPriceLevel();
                albumTracksViewModel = _albumService.GetTracksList(id.Value, currency.Code, priceLevel, GetUserDataId());
            }
            else
            {
                albumTracksViewModel = _albumService.GetTracksList(id.Value);
            }

            if (albumTracksViewModel == null)
            {
                return HttpNotFound($"Альбом с id = { id.Value } не найден");
            }

            return this.View(albumTracksViewModel);
        }
    }
}