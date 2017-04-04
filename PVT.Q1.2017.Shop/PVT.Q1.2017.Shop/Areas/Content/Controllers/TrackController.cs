﻿namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using Shop.Controllers;

    /// <summary>
    /// The track controller
    /// </summary>
    public class TrackController : BaseController
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
            var currency = GetCurrentUserCurrency();

            if (currency != null)
            {
                var priceLevel = GetCurrentUserPriceLevel();
                return this.View(this._trackService.GetTracksList(currency.Code, priceLevel));
            }

            return this.View(this._trackService.GetTracksList());
        }

        /// <summary>
        /// Shows track info.
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        /// Track info view.
        /// </returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            var currency = GetCurrentUserCurrency();
            TrackDetailsViewModel trackViewModel;

            if (currency != null)
            {
                var priceLevel = GetCurrentUserPriceLevel();
                trackViewModel = _trackService.GetTrackDetails(id.Value, currency.Code, priceLevel);
            }
            else
            {
                trackViewModel = _trackService.GetTrackDetails(id.Value);
            }

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
        public ActionResult AlbumsList(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            var currency = GetCurrentUserCurrency();
            TrackAlbumsListViewModel trackAlbumsViewModel;

            if (currency != null)
            {
                var priceLevel = GetCurrentUserPriceLevel();
                trackAlbumsViewModel = _trackService.GetAlbumsList(id.Value, currency.Code, priceLevel);
            }
            else
            {
                trackAlbumsViewModel = _trackService.GetAlbumsList(id.Value);
            }

            if (trackAlbumsViewModel == null)
            {
                return HttpNotFound($"Трек с id = { id.Value } не найден");
            }

            return this.View(trackAlbumsViewModel);
        }

        /// <summary>
        /// Loads the sample of the specified track.
        /// </summary>
        /// <param name="id">
        /// The track id.
        /// </param>
        /// <returns>
        /// The track sample file.
        /// </returns>
        public ActionResult LoadSample(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var trackViewModel = _trackService.GetTrackDetails(id.Value);
            if (trackViewModel == null)
            {
                return HttpNotFound($"Трек с id = { id.Value } не найден");
            }

            return File(trackViewModel.TrackSample, "audio/mp3");
        } 
    }
}