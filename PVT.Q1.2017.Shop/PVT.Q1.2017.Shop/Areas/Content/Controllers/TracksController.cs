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
        public TracksController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory) : base(repositoryFactory, serviceFactory)
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
                trackViewModel = trackService.GetAlbums(id.Value, CurrentUserCurrency.Code, CurrentUser.PriceLevelId, CurrentUser.UserProfileId);
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
                return this.View(trackService.GetTracks(page, pageSize, CurrentUserCurrency.Code, CurrentUser.PriceLevelId, CurrentUser.UserProfileId));
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

            var trackService = ServiceFactory.GetTrackService();
            var trackViewModel = trackService.GetTrackDetails(id.Value);
            if (trackViewModel == null)
            {
                return HttpNotFound($"Трек с id = { id.Value } не найден");
            }

            return File(trackViewModel.TrackSample, "audio/mp3");
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
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

            MemoryStream stream = null;

            if (CurrentUser.IsInRole(UserRoles.Admin))
            {
                // мы можем дать скачать и послушать все треки админу
                Track track;
                using (var repository = RepositoryFactory.GetTrackRepository())
                {
                    track = repository.GetById(id, t => t.Artist);
                }

                if (track != null)
                {
                    stream = Mp3StreamHelper.GetAudioStream(
                                                            this.Response,
                                                            this.Request,
                                                            track.Name,
                                                            track.Artist.Name,
                                                            track.TrackFile);
                }
            }
            else if (CurrentUser.IsInRole(UserRoles.Seller))
            {
                // мы можем дать скачать и послушать свои треки продавцу
                Track track;
                using (var repository = RepositoryFactory.GetTrackRepository())
                {
                    track = repository.FirstOrDefault(t => t.Id == id && (t.OwnerId == CurrentUser.UserProfileId || t.OwnerId == null), t => t.Artist);
                }

                if (track != null)
                {
                    stream = Mp3StreamHelper.GetAudioStream(
                                                            this.Response,
                                                            this.Request,
                                                            track.Name,
                                                            track.Artist.Name,
                                                            track.TrackFile);
                }
            }
            else
            {
                var trackService = ServiceFactory.GetTrackService();

                // мы можем дать скачать и послушать только купленные треки для обычных пользователей
                var purchasedTrack = trackService.GetPurchasedTrack(id, CurrentUser.UserProfileId);
                if (purchasedTrack != null)
                {
                    stream = Mp3StreamHelper.GetAudioStream(
                                                            this.Response,
                                                            this.Request,
                                                            purchasedTrack.Name,
                                                            purchasedTrack.Artist.Name,
                                                            purchasedTrack.TrackFile);
                }
            }
            

            return stream == null ? null : new FileStreamResult(stream, this.Response.ContentType);
        }
    }
}