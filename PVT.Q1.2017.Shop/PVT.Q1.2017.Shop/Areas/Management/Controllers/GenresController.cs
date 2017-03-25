namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.Models.ViewModels;
    using global::Shop.DAL.Infrastruture;

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
        /// Initializes a new instance of the <see cref="GenresController"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <param name="genreService">
        /// The genre service.
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
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(GenreManagementViewModel model)
        {
            var genreModel = Mapper.Map<GenreManagementViewModel, Genre>(model);
            using (var repository = this.RepositoryFactory.GetGenreRepository())
            {
                repository.Delete(genreModel);
                repository.SaveChanges();
            }

            return this.View("New");
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
            // var Genre = this.GenreService.GetGenreInfo(GenreId);
            // var GenreViewModel = Mapper.Map<Genre, GenreManageViewModel>(Genre);
            return this.View();
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
        public virtual ActionResult New([Bind(Include = "Genre")] GenreManagementViewModel viewModel)
        {
            var genre = Mapper.Map<GenreManagementViewModel, Genre>(viewModel);
            using (var repository = this.RepositoryFactory.GetGenreRepository())
            {
                repository.AddOrUpdate(genre);
                repository.SaveChanges();
            }

            return this.View("New");
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public virtual ActionResult Update(GenreManagementViewModel model)
        {
            var genreRepo = this.RepositoryFactory.GetGenreRepository();
            var genre = Mapper.Map<GenreManagementViewModel, Genre>(model);
            genreRepo.AddOrUpdate(genre);
            return this.RedirectToAction("Details");
        }
    }
}