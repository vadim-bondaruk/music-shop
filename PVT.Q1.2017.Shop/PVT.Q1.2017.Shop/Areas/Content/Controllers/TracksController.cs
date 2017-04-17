namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Web.Mvc;

    using global::Shop.BLL.Helpers;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;

    using Shop.Controllers;

    /// <summary>
    ///     The track controller
    /// </summary>
    public class TracksController : BaseController
    {
        /// <summary>
        /// </summary>
        private readonly ITrackService _trackService;

        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TracksController"/> class.
        /// </summary>
        /// <param name="trackServiceo">
        /// The track serviceo.
        /// </param>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        public TracksController(IRepositoryFactory repositoryFactory)
        {
            this._repositoryFactory = repositoryFactory;
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
                return this.RedirectToAction("List");
            }

            TrackAlbumsListViewModel trackAlbumsViewModel = null;
            if (CurrentUser != null)
            {
                var currency = GetCurrentUserCurrency();
                if (currency == null)
                {
                    var priceLevel = GetCurrentUserPriceLevel();
                    trackAlbumsViewModel = this._trackService.GetAlbumsList(
                        id.Value,
                        currency.Code,
                        priceLevel,
                        GetUserDataId());
                }
            }

            if (trackAlbumsViewModel == null)
            {
                trackAlbumsViewModel = this._trackService.GetAlbumsList(id.Value);
            }

            if (trackAlbumsViewModel == null)
            {
                return HttpNotFound($"Трек с id = {id.Value} не найден");
            }

            return this.View(trackAlbumsViewModel);
        }

        /// <summary>
        ///     Shows all tracks.
        /// </summary>
        /// <returns>
        ///     All tracks view.
        /// </returns>
        public ActionResult List()
        {
            if (CurrentUser != null)
            {
                var currency = GetCurrentUserCurrency();
                if (currency != null)
                {
                    var priceLevel = GetCurrentUserPriceLevel();
                    return this.View(
                        this._trackService.GetDetailedTracksList(currency.Code, priceLevel, GetUserDataId()));
                }
            }

            return this.View(this._trackService.GetDetailedTracksList());
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
                return this.RedirectToAction("List");
            }

            TrackDetailsViewModel trackViewModel = null;
            if (CurrentUser != null)
            {
                var currency = GetCurrentUserCurrency();
                if (currency != null)
                {
                    var priceLevel = GetCurrentUserPriceLevel();
                    trackViewModel = this._trackService.GetTrackDetails(
                        id.Value,
                        currency.Code,
                        priceLevel,
                        GetUserDataId());
                }
            }

            if (trackViewModel == null)
            {
                trackViewModel = this._trackService.GetTrackDetails(id.Value);
            }

            if (trackViewModel == null)
            {
                return HttpNotFound($"Трек с id = {id.Value} не найден");
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
            var factory = DependencyResolver.Current.GetService<IRepositoryFactory>();
            using (var trackRepository = factory.GetTrackRepository())
            {
                var audioBytes = trackRepository.GetById(id).TrackFile;
                return this.File(audioBytes, "audio/mp3");
            }
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
    }
}