namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.DAL.Infrastruture;

    using PVT.Q1._2017.Shop.Areas.Management.Helpers;
    using PVT.Q1._2017.Shop.Areas.Management.ViewModels;

    /// <summary>
    ///     The artist controller.
    /// </summary>
    public class ArtistsController : Controller
    {
        /// <summary>
        /// </summary>
        private readonly IArtistService artistService;

        /// <summary>
        ///     The repository factory.
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArtistsController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        /// <param name="artistService"></param>
        public ArtistsController(IRepositoryFactory repositoryFactory, IArtistService artistService)
        {
            this.repositoryFactory = repositoryFactory;
            this.artistService = artistService;
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
            using (var repo = this.repositoryFactory.GetArtistRepository())
            {
                var artist = ManagementMapper.GetArtistModel(viewModel);
                repo.Delete(artist);
                repo.SaveChanges();
            }

            return this.RedirectToAction("List", "Artists", new { area = "Content" });
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
            var artist = ManagementMapper.GetArtistManagementViewModel(this.artistService.GetArtistDetails(id));
            return this.View(artist);
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
            using (var repository = this.repositoryFactory.GetArtistRepository())
            {
                var artist = ManagementMapper.GetArtistModel(viewModel);
                repository.AddOrUpdate(artist);
                repository.SaveChanges();
                return this.RedirectToAction("Details", new { id = viewModel.Id, area = "Content" });
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
            using (var repository = this.repositoryFactory.GetArtistRepository())
            {
                return this.View(repository.GetAll());
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult New()
        {
            return this.View();
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
            using (var repository = this.repositoryFactory.GetArtistRepository())
            {
                repository.AddOrUpdate(artist);
                repository.SaveChanges();
                return this.RedirectToAction(
                    "Details",
                    new { area = "Content", Controller = "Artists", id = artist.Id });
            }
        }
    }
}