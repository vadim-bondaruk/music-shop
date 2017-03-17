namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.Common.Models;
    using global::Shop.Common.Models.ViewModels;
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
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int artistId)
        {
            using (var artistRepository = this.repositoryFactory.GetArtistRepository())
            {
                var artist = artistRepository.GetById(artistId);
                Mapper.Map<ArtistDetailsViewModel>(artist);
                return this.View("Details");
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
            [Bind(Exclude = "Artists, Albums")] ArtistDetailsViewModel viewModel)
        {
            Artist artist;
            using (var artistRepo = this.repositoryFactory.GetArtistRepository())
            {
                var bs = new byte[viewModel.UploadedImage.ContentLength];
                using (var fs = viewModel.UploadedImage.InputStream)
                {
                    var offset = 0;
                    do
                    {
                        offset += fs.Read(bs, offset, bs.Length - offset);
                    }
                    while (offset < bs.Length);
                }

                viewModel.Photo = bs;
                artist = Mapper.Map<Artist>(viewModel);
                artistRepo.AddOrUpdate(artist);
                artistRepo.SaveChanges();
            }

            this.ViewData["Name"] = artist.Name;
            return this.RedirectToAction("Details");
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