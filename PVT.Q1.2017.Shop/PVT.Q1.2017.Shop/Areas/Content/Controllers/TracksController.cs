namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    using global::Shop.BLL.Helpers;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;

    using PVT.Q1._2017.Shop.Controllers;

    /// <summary>
    ///     The track controller
    /// </summary>
    public class TracksController : BaseController
    {
        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// </summary>
        private readonly ITrackService _trackService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TracksController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        /// <param name="trackService">
        ///     The track service.
        /// </param>
        public TracksController(IRepositoryFactory repositoryFactory, ITrackService trackService)
        {
            this._repositoryFactory = repositoryFactory;
            this._trackService = trackService;
        }

        /// <summary>
        ///     Shows all albums where the specified track is exist.
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        ///     All albums where the specified track is exist.
        /// </returns>
        public virtual ActionResult AlbumsList(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List", "Tracks", new { area = "Content" });
            }

            TrackAlbumsListViewModel trackAlbumsViewModel = null;
            if (this.CurrentUser != null)
            {
                var currency = this.GetCurrentUserCurrency();
                if (currency == null)
                {
                    var priceLevel = this.GetCurrentUserPriceLevel();
                    trackAlbumsViewModel = this._trackService.GetAlbumsList(
                        id.Value,
                        currency.Code,
                        priceLevel,
                        this.GetUserDataId());
                }
            }

            if (trackAlbumsViewModel == null)
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
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List", "Tracks", new { area = "Content" });
            }

            TrackDetailsViewModel trackViewModel = null;
            if (this.CurrentUser != null)
            {
                var currency = this.GetCurrentUserCurrency();
                if (currency != null)
                {
                    var priceLevel = this.GetCurrentUserPriceLevel();
                    trackViewModel = this._trackService.GetTrackDetails(
                        id.Value,
                        currency.Code,
                        priceLevel,
                        this.GetUserDataId());
                }
            }

            if (trackViewModel == null)
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
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult GetAudio(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var factory = DependencyResolver.Current.GetService<IRepositoryFactory>();
            using (var trackRepository = factory.GetTrackRepository())
            {
                //var track = trackRepository.GetById(id.Value);
                //if (track == null)
                //{
                //    return HttpNotFound($"Трек с id = { id.Value } не найден");
                //}

                //var audioBytes = track.TrackFile;
                //return this.File(audioBytes, "audio/mp3");
            }
            return HttpNotFound();
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public FileStreamResult GetTrackAsStream(int id)
        {
            MemoryStream stream;
            using (var repo = this._repositoryFactory.GetTrackRepository())
            {
                var track = repo.GetById(id, p => p.Artist);

                if (track == null)
                {
                    return null;
                }

                stream = Mp3StreamHelper.GetAudioStream(
                    this.Response,
                    this.Request,
                    track.Name,
                    track.Artist.Name,
                    track.TrackFile);
            }

            return stream == null ? null : new FileStreamResult(stream, this.Response.ContentType);
        }

        /// <summary>
        ///     Shows all tracks.
        /// </summary>
        /// <returns>
        ///     All tracks view.
        /// </returns>
        public ActionResult List()
        {
            if (this.CurrentUser != null)
            {
                var currency = this.GetCurrentUserCurrency();
                if (currency != null)
                {
                    var priceLevel = this.GetCurrentUserPriceLevel();
                    return
                        this.View(
                            this._trackService.GetDetailedTracksList(currency.Code, priceLevel, this.GetUserDataId()));
                }
            }

            return this.View(this._trackService.GetDetailedTracksList());
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult PurchasedList()
        {
            var purchasedTracks = this._trackService.GetPurchasedTracks(this.CurrentUser.Id);

            if (purchasedTracks == null)
            {
                return this.HttpNotFound("У вас нет приобретенных товаров");
            }

            return this.View(purchasedTracks);
        }
    }
}