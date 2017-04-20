namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Enums;

    using PVT.Q1._2017.Shop.App_Start;

    using Shop.Controllers;

    /// <summary>
    ///     The artist controller.
    /// </summary>
    [ShopAuthorize(UserRoles.Buyer, UserRoles.Admin, UserRoles.Seller)]
    public class ArtistsController : BaseController
    {
        public ArtistsController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory) : base(repositoryFactory, serviceFactory)
        {
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
                return RedirectToAction("List", "Artists", new { area = "Content" });
            }

            ArtistAlbumsListViewModel artistAlbumsViewModel;
            var artistService = ServiceFactory.GetArtistService();
            if (CurrentUser != null && CurrentUserCurrency != null)
            {
                artistAlbumsViewModel = artistService.GetAlbums(id.Value, CurrentUserCurrency.Code, CurrentUser.PriceLevelId, CurrentUser.UserProfileId);
            }
            else
            {
                artistAlbumsViewModel = artistService.GetAlbums(id.Value);
            }

            if (artistAlbumsViewModel == null)
            {
                return HttpNotFound($"Исполнитель с id = { id.Value } не найден");
            }

            return View(artistAlbumsViewModel);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult List()
        {
            var artistService = ServiceFactory.GetArtistService();
            return View(artistService.GetDetailedArtistsList());
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
                return RedirectToAction("List", "Artists", new { area = "Content" });
            }

            var artistService = ServiceFactory.GetArtistService();
            var artistViewModel = artistService.GetArtistDetails(id.Value);
            if (artistViewModel == null)
            {
                return HttpNotFound($"Артист с id = { id.Value } не найден");
            }

            return View(artistViewModel);
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
                return RedirectToAction("List", "Artists", new { area = "Content" });
            }

            ArtistTracksListViewModel artistTracksViewModel;
            var artistService = ServiceFactory.GetArtistService();
            if (CurrentUser != null && CurrentUserCurrency != null)
            {
                artistTracksViewModel = artistService.GetTracks(id.Value, CurrentUserCurrency.Code, CurrentUser.PriceLevelId, CurrentUser.UserProfileId);
            }
            else
            {
                artistTracksViewModel = artistService.GetTracks(id.Value);
            }

            if (artistTracksViewModel == null)
            {
                return HttpNotFound($"Исполнитель с id = { id.Value } не найден");
            }

            return View(artistTracksViewModel);
        }
    }
}