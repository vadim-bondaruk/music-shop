namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    using PVT.Q1._2017.Shop.Areas.Management.Models;

    /// <summary>
    /// </summary>
    public class GenresController : Controller
    {
        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenresController" /> class.
        /// </summary>
        /// <param name="repoFactory">
        ///     The repo factory.
        /// </param>
        public GenresController(IRepositoryFactory repoFactory)
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
        public virtual ActionResult Delete(GenreManagmentViewModel model)
        {
            var genreModel = Mapper.Map<Genre>(model);
            using (var repository = this.repositoryFactory.GetGenreRepository())
            {
                repository.Delete(genreModel);
                repository.SaveChanges();
            }

            return this.View("GenreManage");
        }

        /// <summary>
        /// </summary>
        /// <param name="genreId">
        ///     The genre id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int genreId)
        {
            using (var genreRepo = this.repositoryFactory.GetGenreRepository())
            {
                var genre = genreRepo.GetById(genreId);
                var genreViewModel = Mapper.Map<GenreManagmentViewModel>(genre);
                return this.View(genreViewModel);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public virtual ActionResult New()
        {
            return this.View("GenreManage");
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
        public virtual ActionResult New([Bind(Include = "Name, Description")] ArtistManagmentViewModel viewModel)
        {
            var genre = Mapper.Map<Genre>(viewModel);
            using (var repository = this.repositoryFactory.GetGenreRepository())
            {
                repository.AddOrUpdate(genre);
                repository.SaveChanges();
            }

            return this.View("GenreManage");
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public virtual ActionResult Update(GenreManagmentViewModel model)
        {
            using (var repository = this.repositoryFactory.GetGenreRepository())
            {
                var genre = Mapper.Map<Genre>(model);
                repository.AddOrUpdate(genre);
            }

            return this.View();
        }
    }
}