namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;
    using Shop.Controllers;

    /// <summary>
    /// The artist controller.
    /// </summary>
    public class ArtistController : BaseController
    {
        public ArtistController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory) : base(repositoryFactory, serviceFactory)
        {
        }

        /// <summary>
        /// Shows all artists.
        /// </summary>
        /// <returns>
        /// All artists view.
        /// </returns>
        public ActionResult List()
        {
            var artistService = ServiceFactory.GetArtistService();
            return View(artistService.GetArtists());
        }

        /// <summary>
        /// Show artist info.
        /// </summary>
        /// <param name="id">
        /// The artist id.
        /// </param>
        /// <returns>
        /// Artist view.
        /// </returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List", "Artist", new { area = "Content" });
            }

            ArtistContentViewModel artistViewModel;

            var artistService = ServiceFactory.GetArtistService();
            if (CurrentUser != null && CurrentUserCurrency != null)
            {
                artistViewModel = artistService.GetContent(id.Value, CurrentUserCurrency.Code, CurrentUser.PriceLevelId, CurrentUser.UserProfileId);
            }
            else
            {
                artistViewModel = artistService.GetContent(id.Value);
            }

            if (artistViewModel == null)
            {
                return HttpNotFound($"Артист с id = { id.Value } не найден");
            }

            return View(artistViewModel);
        }
    }
}