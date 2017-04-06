namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;

    using PVT.Q1._2017.Shop.Controllers;

    /// <summary>
    ///     The track controller
    /// </summary>
    public class TrackController : BaseController
    {
        /// <summary>
        ///     The track service.
        /// </summary>
        private readonly ITrackService _trackService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TrackController" /> class.
        /// </summary>
        /// <param name="trackService">
        ///     The track service.
        /// </param>
        public TrackController(ITrackService trackService)
        {
            this._trackService = trackService;
        }

        /// <summary>
        ///     Shows all albums where the specified track is exist.
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        ///     All albums where the specified track is exist.
        /// </returns>
        public ActionResult AlbumsList(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            var currency = this.GetCurrentUserCurrency();
            TrackAlbumsListViewModel trackAlbumsViewModel;

            if (currency != null)
            {
                var priceLevel = this.GetCurrentUserPriceLevel();
                trackAlbumsViewModel = this._trackService.GetAlbumsList(id.Value, currency.Code, priceLevel);
            }
            else
            {
                trackAlbumsViewModel = this._trackService.GetAlbumsList(id.Value);
            }

            if (trackAlbumsViewModel == null)
            {
                return this.HttpNotFound($"Трек с id = {id.Value} не найден");
            }

            return this.View(trackAlbumsViewModel);
        }

        /// <summary>
        ///     Shows track info.
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        ///     Track info view.
        /// </returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            var currency = this.GetCurrentUserCurrency();
            TrackDetailsViewModel trackViewModel;

            if (currency != null)
            {
                var priceLevel = this.GetCurrentUserPriceLevel();
                trackViewModel = this._trackService.GetTrackDetails(id.Value, currency.Code, priceLevel);
            }
            else
            {
                trackViewModel = this._trackService.GetTrackDetails(id.Value);
            }

            if (trackViewModel == null)
            {
                return this.HttpNotFound($"Трек с id = {id.Value} не найден");
            }

            return this.View(trackViewModel);
        }

        /// <summary>
        ///     Shows all tracks.
        /// </summary>
        /// <returns>
        ///     All tracks view.
        /// </returns>
        public ActionResult List()
        {
            var currency = this.GetCurrentUserCurrency();

            if (currency != null)
            {
                var priceLevel = this.GetCurrentUserPriceLevel();
                return this.View(this._trackService.GetTracksList(currency.Code, priceLevel));
            }

            return this.View(this._trackService.GetTracksList());
        }

        /// <summary>
        ///     Loads the sample of the specified track.
        /// </summary>
        /// <param name="id">
        ///     The track id.
        /// </param>
        /// <returns>
        ///     The track sample file.
        /// </returns>
        public ActionResult LoadSample(int? id)
        {
            if (id == null)
            {
                return this.HttpNotFound();
            }

            var trackViewModel = this._trackService.GetTrackDetails(id.Value);
            if (trackViewModel == null)
            {
                return this.HttpNotFound($"Трек с id = {id.Value} не найден");
            }

            return this.File(trackViewModel.TrackFile, "audio/mp3");
        }
    }
}