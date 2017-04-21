namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.IO;
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
        public TracksController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory) : base(repositoryFactory, serviceFactory)
        {
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
            if (id.GetValueOrDefault() <= 0)
            {
                return this.RedirectToAction("List", "Tracks", new { area = "Content" });
            }

            TrackAlbumsListViewModel trackAlbumsViewModel = null;
            var trackService = ServiceFactory.GetTrackService();
            if (this.CurrentUser != null && CurrentUserCurrency != null)
            {
                trackAlbumsViewModel = trackService.GetAlbums(id.Value, CurrentUserCurrency.Code, CurrentUser.PriceLevelId, CurrentUser.UserProfileId);
            }
            else
            {
                trackAlbumsViewModel = trackService.GetAlbums(id.Value);
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
                return this.RedirectToAction("List", "Tracks", new { area = "Content" });
            }

            TrackDetailsViewModel trackViewModel;
            var trackService = ServiceFactory.GetTrackService();
            if (CurrentUser != null && CurrentUserCurrency != null)
            {
                trackViewModel = trackService.GetTrackDetails(id.Value, CurrentUserCurrency.Code, CurrentUser.PriceLevelId, CurrentUser.UserProfileId);
            }
            else
            {
                trackViewModel = trackService.GetTrackDetails(id.Value);
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
        public FileStreamResult GetTrackAsStream(int id)
        {
            MemoryStream stream;
            using (var repo = RepositoryFactory.GetTrackRepository())
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
        public ActionResult List(int page = 1, int pageSize = 10)
        {
            var trackService = ServiceFactory.GetTrackService();
            if (CurrentUser != null && CurrentUserCurrency != null)
            {
                return this.View(trackService.GetDetailedTracksList(page, pageSize, CurrentUserCurrency.Code, CurrentUser.PriceLevelId, CurrentUser.UserProfileId));
            }

            return this.View(trackService.GetDetailedTracksList(page, pageSize));
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult PurchasedList()
        {
            var trackService = ServiceFactory.GetTrackService();
            var purchasedTracks = trackService.GetPurchasedTracks(this.CurrentUser.Id);

            if (purchasedTracks == null)
            {
                return this.HttpNotFound("У вас нет приобретенных товаров");
            }

            return this.View(purchasedTracks);
        }
    }
}