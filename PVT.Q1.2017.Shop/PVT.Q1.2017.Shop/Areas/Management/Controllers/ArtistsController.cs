namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    using PVT.Q1._2017.Shop.Areas.Management.Models;

    /// <summary>
    /// </summary>
    public class ArtistsController : Controller
    {
        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArtistsController" /> class.
        /// </summary>
        public ArtistsController(IRepositoryFactory repoFactory)
        {
            this.repositoryFactory = repoFactory;
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
        public virtual ActionResult Delete(ArtistManagmentViewModel model)
        {
            var artistModel = Mapper.Map<ArtistManagmentViewModel, Artist>(model);
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
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int artistId)
        {
            using (var artistRepository = this.repositoryFactory.GetArtistRepository())
            {
                var artist = artistRepository.GetById(artistId);
                Mapper.Map<ArtistManagmentViewModel>(artist);
                return this.View("New");
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public virtual ActionResult New()
        {
            return this.View("New");
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
        public virtual ActionResult New(
            [Bind(Include = "Name, Biography, Birthday, Photo")] ArtistManagmentViewModel viewModel)
        {
            using (var artistRepo = this.repositoryFactory.GetArtistRepository())
            {
                var artist = Mapper.Map<Artist>(viewModel);
                artistRepo.AddOrUpdate(artist);
                artistRepo.SaveChanges();
                return this.View("New");
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public virtual ActionResult Update(ArtistManagmentViewModel model)
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