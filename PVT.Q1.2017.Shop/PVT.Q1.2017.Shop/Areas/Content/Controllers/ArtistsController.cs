﻿namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using Shop.Controllers;

    /// <summary>
    ///     The artist controller.
    /// </summary>
    public class ArtistsController : BaseController
    {
        private readonly IArtistService _artistService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistsController"/> class.
        /// </summary>
        /// <param name="artistService">
        /// The artist service.
        /// </param>
        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        /// <summary>
        ///     Shows all artist albums
        /// </summary>
        /// <param name="id">
        ///     The artist id.
        /// </param>
        /// <returns>
        ///     All artist albums view.
        /// </returns>
        public ActionResult AlbumsList(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List", "Artists", new { area = "Content" });
            }

            ArtistAlbumsListViewModel artistAlbumsViewModel = null;
            if (CurrentUser != null)
            {
                var currency = GetCurrentUserCurrency();
                if (currency != null)
                {
                    var priceLevel = GetCurrentUserPriceLevel();
                    artistAlbumsViewModel = _artistService.GetAlbumsList(id.Value, currency.Code, priceLevel, GetUserDataId());
                }
            }
            

            if (artistAlbumsViewModel == null)
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
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult List()
        {
            return this.View(_artistService.GetDetailedArtistsList());
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List", "Artists", new { area = "Content" });
            }

            var artistViewModel = _artistService.GetArtistDetails(id.Value);
            if (artistViewModel == null)
            {
                return HttpNotFound($"Артист с id = { id.Value } не найден");
            }

            return this.View(artistViewModel);
        }

        /// <summary>
        ///     Shows all artist tracks.
        /// </summary>
        /// <param name="id">
        ///     The artist id.
        /// </param>
        /// <returns>
        ///     All artist tracks view.
        /// </returns>
        public ActionResult TracksList(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List", "Artists", new { area = "Content" });
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