namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.Common.Models;
    using global::Shop.Common.Models.ViewModels;
    using global::Shop.DAL.Infrastruture;

    /// <summary>
    ///     The artist controller.
    /// </summary>
    public class ArtistsController : Controller
    {
        /// <summary>
        ///     The repository factory.
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArtistsController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        public ArtistsController(IRepositoryFactory repositoryFactory)
        {
            this._repositoryFactory = repositoryFactory;
        }

        /// <summary>
        ///     Shows all artists.
        /// </summary>
        /// <returns>
        ///     All artists view.
        /// </returns>
        public ActionResult Index()
        {
            using (var repository = this._repositoryFactory.GetArtistRepository())
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
        public ActionResult New(ArtistManagementViewModel model)
        {
            Mapper.Initialize(
                cfg =>
                    {
                        cfg.CreateMap<ArtistManagementViewModel, Artist>()
                            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));
                    });
            var artist = Mapper.Map<Artist>(model);
            using (var repository = this._repositoryFactory.GetArtistRepository())
            {
                repository.AddOrUpdate(artist);
                repository.SaveChanges();
                return this.View();
            }
        }
    }
}