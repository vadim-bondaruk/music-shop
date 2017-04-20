namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Enums;

    using PVT.Q1._2017.Shop.App_Start;
    using PVT.Q1._2017.Shop.Areas.Management.Helpers;
    using PVT.Q1._2017.Shop.Areas.Management.ViewModels;
    using Shop.Controllers;

    /// <summary>
    ///     The artist controller.
    /// </summary>
    [ShopAuthorize(UserRoles.Admin, UserRoles.Seller)]
    public class ArtistsController : BaseController
    {
        public ArtistsController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory) : base(repositoryFactory, serviceFactory)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel">
        ///     The view model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete([Bind(Include = "Id")] ArtistManagementViewModel viewModel)
        {
            using (var repo = RepositoryFactory.GetArtistRepository())
            {
                var artist = ManagementMapper.GetArtistModel(viewModel);
                repo.Delete(artist);
                repo.SaveChanges();
            }

            return RedirectToAction("List", "Artists", new { area = "Content" });
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Edit(int id)
        {
            var artistService = ServiceFactory.GetArtistService();
            var artist = ManagementMapper.GetArtistManagementViewModel(artistService.GetArtistDetails(id));
            return View(artist);
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel">
        ///     The view model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(
            [Bind(Include = "Id, Name, Birthday, Biography, Photo, PostedPhoto")] ArtistManagementViewModel viewModel)
        {
            using (var repository = RepositoryFactory.GetArtistRepository())
            {
                var artist = ManagementMapper.GetArtistModel(viewModel);
                repository.AddOrUpdate(artist);
                repository.SaveChanges();
                return RedirectToAction("Details", new { id = viewModel.Id, area = "Content" });
            }
        }

        /// <summary>
        ///     Shows all artists.
        /// </summary>
        /// <returns>
        ///     All artists view.
        /// </returns>
        public ActionResult Index()
        {
            using (var repository = RepositoryFactory.GetArtistRepository())
            {
                return View(repository.GetAll());
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(ArtistManagementViewModel viewModel)
        {
            var artist = ManagementMapper.GetArtistModel(viewModel);
            using (var repository = RepositoryFactory.GetArtistRepository())
            {
                repository.AddOrUpdate(artist);
                repository.SaveChanges();
                return RedirectToAction("Details", new { area = "Content", Controller = "Artists", id = artist.Id });
            }
        }
    }
}