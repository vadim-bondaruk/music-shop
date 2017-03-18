namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.Models.ViewModels;
    using global::Shop.DAL.Infrastruture;

    /// <summary>
    /// </summary>
    public class ArtistsController : Controller
    {
        /// <summary>
        /// </summary>
        private readonly IArtistService artistService;

        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArtistsController" /> class.
        /// </summary>
        /// <param name="artistService">
        ///     The artist service.
        /// </param>
        /// <param name="repositoryFactory"></param>
        public ArtistsController(IArtistService artistService, IRepositoryFactory repositoryFactory)
        {
            this.artistService = artistService;
            this.repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(ArtistDetailsViewModel model)
        {
            var artistModel = Mapper.Map<ArtistDetailsViewModel, Artist>(model);
            using (var repository = this.repositoryFactory.GetArtistRepository())
            {
                repository.Delete(artistModel);
                repository.SaveChanges();
            }

            return this.View("New");
        }

        /// <summary>
        /// </summary>
        /// <param name="artistId">
        /// The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int artistId = 0)
        {
            var artist = this.artistService.GetArtistViewModel(artistId);
            return this.View(artist);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public virtual ActionResult New()
        {
            return this.View();
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
        public virtual ActionResult New([Bind(Exclude = "Artists, Albums")] ArtistDetailsViewModel viewModel)
        {
            var id = this.artistService.SaveNewArtist(viewModel);
            return this.RedirectToAction("Details", new { artistId = id });
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public virtual ActionResult Update(ArtistDetailsViewModel model)
        {
            using (var artistRepo = this.repositoryFactory.GetArtistRepository())
            {
                var artist = Mapper.Map<Artist>(model);
                artistRepo.AddOrUpdate(artist);
            }

            return this.View("Details");
        }
    }
}