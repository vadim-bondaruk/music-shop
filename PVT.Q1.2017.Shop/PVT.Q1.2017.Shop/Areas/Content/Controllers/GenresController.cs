namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Web.Mvc;

    using global::Shop.BLL.Helpers;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.DAL.Infrastruture;

    /// <summary>
    /// </summary>
    public class GenresController : Controller
    {
        /// <summary>
        /// </summary>
        private readonly IGenreService genreService;

        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

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
            this.repositoryFactory = repositoryFactory;
            this.genreService = genreService;
        }

        /// <summary>
        /// </summary>
        /// <param name="genreId">
        ///     The genre id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int id)
        {
            using (var repository = this.repositoryFactory.GetGenreRepository())
            {
                var model = repository.GetById(id);
                if (model == null)
                {
                    return this.View("_NotFound");
                }

                var viewModel = ModelsMapper.GetGenreDetailsViewModel(model);
                return this.View(viewModel);
            }
        }

        /// <summary>
        ///     Shows all albums.
        /// </summary>
        /// <returns>
        ///     All albums view.
        /// </returns>
        public ActionResult List()
        {
            return this.View(this.genreService.GetAllViewModels());
        }
    }
}