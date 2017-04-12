namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using Shop.Controllers;

    /// <summary>
    /// The artist controller.
    /// </summary>
    public class ArtistController : BaseController
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

            var currency = GetCurrentUserCurrency();
            ArtistAlbumsListViewModel artistAlbumsViewModel;

            if (currency != null && CurrentUser != null)
            {
                var priceLevel = GetCurrentUserPriceLevel();
                artistAlbumsViewModel = _artistService.GetAlbumsList(id.Value, currency.Code, priceLevel, GetUserDataId());
            }
            else
            {
                artistAlbumsViewModel = _artistService.GetAlbumsList(id.Value);
            }

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

            var currency = GetCurrentUserCurrency();
            ArtistTracksListViewModel artistTracksViewModel;

            if (currency != null && CurrentUser != null)
            {
                var priceLevel = GetCurrentUserPriceLevel();
                artistTracksViewModel = _artistService.GetTracksList(id.Value, currency.Code, priceLevel, GetUserDataId());
            }
            else
            {
                artistTracksViewModel = _artistService.GetTracksList(id.Value);
            }

            if (artistTracksViewModel == null)
            {
                return HttpNotFound($"Исполнитель с id = { id.Value } не найден");
            }

            return this.View(artistTracksViewModel);
        }
    }
}