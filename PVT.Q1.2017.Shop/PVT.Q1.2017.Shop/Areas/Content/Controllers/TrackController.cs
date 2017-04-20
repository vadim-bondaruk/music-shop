namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;
    using Shop.Controllers;

    /// <summary>
    /// The track controller
    /// </summary>
    public class TrackController : BaseController
    {
        public TrackController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory) : base(repositoryFactory, serviceFactory)
        {
        }

        /// <summary>
        /// Shows all tracks.
        /// </summary>
        /// <returns>
        /// All tracks view.
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
                return this.RedirectToAction("List", "Track", new { area = "Content" });
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
                return HttpNotFound($"Трек с id = { id.Value } не найден");
            }

            return this.View(trackViewModel);
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
    }
}