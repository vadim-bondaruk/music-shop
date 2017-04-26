namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using App_Start;

    using global::Shop.BLL.Helpers;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Enums;

    using PVT.Q1._2017.Shop.Controllers;

    /// <summary>
    ///     The track controller
    /// </summary>
    public class TracksController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TracksController"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <param name="serviceFactory">
        /// The service factory.
        /// </param>
        public TracksController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory)
            : base(repositoryFactory, serviceFactory)
        {
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

            TrackAlbumsListViewModel trackViewModel;
            var trackService = ServiceFactory.GetTrackService();
            if (CurrentUser != null && CurrentUserCurrency != null)
            {
                trackViewModel = trackService.GetAlbums(
                    id.Value,
                    CurrentUserCurrency.Code,
                    CurrentUser.PriceLevelId,
                    CurrentUser.UserProfileId);
            }
            else
            {
                trackViewModel = trackService.GetAlbums(id.Value);
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
        public ActionResult List(int page = 1, int pageSize = 10)
        {
            var trackService = ServiceFactory.GetTrackService();
            if (CurrentUser != null && CurrentUserCurrency != null)
            {
                return
                    this.View(
                        trackService.GetTracks(
                            page,
                            pageSize,
                            CurrentUserCurrency.Code,
                            CurrentUser.PriceLevelId,
                            CurrentUser.UserProfileId));
            }

            return this.View(trackService.GetTracks(page, pageSize));
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        [ShopAuthorize(UserRoles.Buyer, UserRoles.Customer)]
        public async Task<ActionResult> PurchasedList()
        {
            var trackService = ServiceFactory.GetTrackService();
            var purchasedTracks = await trackService.GetPurchasedTracksAsync(this.CurrentUser.Id);

            if (purchasedTracks == null)
            {
                return this.HttpNotFound("У вас нет приобретенных товаров");
            }

            return this.View(purchasedTracks);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// </returns>
        [ShopAuthorize]
        public FileStreamResult GetTrackAsStream(int id = 0)
        {
            if (id <= 0)
            {
                return null;
            }

            MemoryStream stream;
            if (CurrentUser.IsInRole(UserRoles.Admin))
            {
                stream = GetTrackStreamForAdmin(id);
            }
            else if (CurrentUser.IsInRole(UserRoles.Seller))
            {
                stream = GetTrackStreamForSeller(id);
            }
            else
            {
                stream = GetTrackStreamForBuyer(id);
            }

            return stream == null ? null : new FileStreamResult(stream, this.Response.ContentType);
        }

        private MemoryStream GetTrackStreamForAdmin(int trackId)
        {
            // мы можем дать скачать и послушать все треки админу
            Track track;
            using (var repository = RepositoryFactory.GetTrackRepository())
            {
                track = repository.GetById(trackId, t => t.Artist);
            }

            if (track != null)
            {
                return Mp3StreamHelper.GetAudioStream(
                                                        this.Response,
                                                        this.Request,
                                                        track.Name,
                                                        track.Artist.Name,
                                                        track.TrackFile);
            }

            return null;
        }

        private MemoryStream GetTrackStreamForBuyer(int trackId)
        {
            var trackService = ServiceFactory.GetTrackService();

            // мы можем дать скачать и послушать только купленные треки для обычных пользователей
            var purchasedTrack = trackService.GetPurchasedTrack(trackId, CurrentUser.UserProfileId);
            if (purchasedTrack != null)
            {
                return Mp3StreamHelper.GetAudioStream(
                                                      this.Response,
                                                      this.Request,
                                                      purchasedTrack.Name,
                                                      purchasedTrack.Artist.Name,
                                                      purchasedTrack.TrackFile);
            }

            return GetTrackSampleAsStream(trackId);
        }

        private MemoryStream GetTrackStreamForSeller(int trackId)
        {
            // мы можем дать скачать и послушать свои треки продавцу
            Track track;
            using (var repository = RepositoryFactory.GetTrackRepository())
            {
                track =
                    repository.FirstOrDefault(
                                              t => t.Id == trackId && (t.OwnerId == CurrentUser.UserProfileId || t.OwnerId == null),
                                              t => t.Artist);
            }

            if (track != null)
            {
                return Mp3StreamHelper.GetAudioStream(
                                                      this.Response,
                                                      this.Request,
                                                      track.Name,
                                                      track.Artist.Name,
                                                      track.TrackFile);
            }

            return GetTrackSampleAsStream(trackId);
        }

        private MemoryStream GetTrackSampleAsStream(int trackId)
        {
            Track track;
            using (var repository = RepositoryFactory.GetTrackRepository())
            {
                track =
                    repository.FirstOrDefault(
                                              t => t.Id == trackId && (t.OwnerId == CurrentUser.UserProfileId || t.OwnerId == null),
                                              t => t.Artist);
            }

            if (track != null)
            {
                return Mp3StreamHelper.GetAudioStream(
                                                      this.Response,
                                                      this.Request,
                                                      track.Name,
                                                      track.Artist.Name,
                                                      track.TrackFile,
                                                      true);
            }

            return null;
        }
    }
}