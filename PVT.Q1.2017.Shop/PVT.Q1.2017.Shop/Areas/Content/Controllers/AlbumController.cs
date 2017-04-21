namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;
    using Shop.Controllers;

    /// <summary>
    /// The album controller.
    /// </summary>
    public class AlbumController : BaseController
    {

        public AlbumController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory) : base(repositoryFactory, serviceFactory)
        {
        }

        /// <summary>
        /// Shows all albums.
        /// </summary>
        /// <returns>
        /// All albums view.
        /// </returns>
        public ActionResult List(int page = 1, int pageSize = 10)
        {
            var albumService = ServiceFactory.GetAlbumService();
            if (CurrentUser != null && CurrentUserCurrency != null)
            {
                return View(albumService.GetAlbums(page, pageSize, CurrentUserCurrency.Code, CurrentUser.PriceLevelId, CurrentUser.UserProfileId));
            }

            return View(albumService.GetAlbums(page, pageSize));
        }

        /// <summary>
        /// Shows album info.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>
        /// Album view.
        /// </returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List", "Album", new { area = "Content" });
            }

            AlbumTracksListViewModel albumViewModel;
            var albumService = ServiceFactory.GetAlbumService();
            if (CurrentUser != null && CurrentUserCurrency != null)
            {
                albumViewModel = albumService.GetTracks(id.Value, CurrentUserCurrency.Code, CurrentUser.PriceLevelId, CurrentUser.UserProfileId);
            }
            else
            {
                albumViewModel = albumService.GetTracks(id.Value);
            }

            if (albumViewModel == null)
            {
                return HttpNotFound($"Альбом с id = { id.Value } не найден");
            }

            return View(albumViewModel);
        }
    }
}