namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System;
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    using PVT.Q1._2017.Shop.Areas.Management.Helpers;
    using PVT.Q1._2017.Shop.Areas.Management.ViewModels;

    /// <summary>
    /// </summary>
    public class GenresController : Controller
    {
        /// <summary>
        /// </summary>
        private readonly IGenreService genreService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenresController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        public GenresController(IRepositoryFactory repositoryFactory)
        {
            this.RepositoryFactory = repositoryFactory;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenresController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        /// <param name="genreService">
        ///     The genre service.
        /// </param>
        public GenresController(IRepositoryFactory repositoryFactory, IGenreService genreService)
        {
            this.RepositoryFactory = repositoryFactory;
            this.genreService = genreService;
            Mapper.Initialize(cfg => cfg.CreateMap<GenreManagementViewModel, Genre>());
        }

        /// <summary>
        ///     Gets or sets the repository factory.
        /// </summary>
        public IRepositoryFactory RepositoryFactory { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(int id)
        {
            using (var repository = this.RepositoryFactory.GetGenreRepository())
            {
                repository.Delete(id);
                repository.SaveChanges();
            }

            return this.RedirectToAction("List", "Genres", new { Area = "Content" });
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult Edit(int id)
        {
            var genreManagementViewModel =
                ManagementMapper.GetGenreManagementViewModel(this.genreService.GetGenreDetailsViewModel(id));
            return this.View(genreManagementViewModel);
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
        public ActionResult Edit(GenreManagementViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("List", "Genres", new { Area = "Content" });
            }

            var genreModel = ManagementMapper.GetGenreModel(viewModel);
            using (var repo = this.RepositoryFactory.GetGenreRepository())
            {
                repo.AddOrUpdate(genreModel);
                repo.SaveChanges();
            }

            return this.RedirectToAction("Details", "Genres", new { Area = "Content", id = viewModel.Id });
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
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
        public virtual ActionResult New(GenreManagementViewModel viewModel)
        {
            var genre = ManagementMapper.GetGenreModel(viewModel);
            using (var repository = this.RepositoryFactory.GetGenreRepository())
            {
                repository.AddOrUpdate(genre);
                repository.SaveChanges();
            }

            return this.RedirectToAction("Details", new { id = viewModel.Id, area = "Content" });
        }
    }
}