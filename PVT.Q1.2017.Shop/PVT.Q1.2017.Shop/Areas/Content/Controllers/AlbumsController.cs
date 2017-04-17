namespace PVT.Q1._2017.Shop.Areas.Content.Controllers

{
    using System.Web.Mvc;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;

    using PVT.Q1._2017.Shop.Controllers;

    /// <summary>
    ///     The album controller.
    /// </summary>
    public class AlbumsController : BaseController
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
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List", "Albums", new { area = "Content" });
            }

            AlbumTracksListViewModel albumTracksViewModel = null;
            if (this.CurrentUser != null)
            {
                var currency = this.GetCurrentUserCurrency();
                if (currency != null)
                {
                    var priceLevel = this.GetCurrentUserPriceLevel();
                    albumTracksViewModel = this._albumService.GetTracksList(id.Value, currency.Code, priceLevel, this.GetUserDataId());
                }
            }
            
            if (albumTracksViewModel == null)
            {
                albumTracksViewModel = this._albumService.GetTracksList(id.Value);
            }

            if (albumTracksViewModel == null)
            {
                return this.HttpNotFound($"Альбом с id = {id.Value} не найден");
            }

            return this.View(albumTracksViewModel);
        }

        /// <summary>
        ///     Shows all albums.
        /// </summary>
        /// <returns>
        ///     All albums view.
        /// </returns>
        public ActionResult List()
        {
            if (this.CurrentUser != null)
            {
                var currency = this.GetCurrentUserCurrency();
                if (currency != null)
                {
                    var priceLevel = this.GetCurrentUserPriceLevel();
                    return this.View(this._albumService.GetDetailedAlbumsList(currency.Code, priceLevel, this.GetUserDataId()));
                }
            }

            return this.View(this._albumService.GetDetailedAlbumsList());
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>
        /// </returns>
        public virtual ActionResult TracksList(int? id)
        {
            return this.Details(id);
        }
    }
}