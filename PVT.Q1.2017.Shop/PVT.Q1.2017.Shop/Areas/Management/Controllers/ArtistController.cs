using System.Web.Mvc;

namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.DAL.Infrastruture;

    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistController"/> class.
        /// </summary>
        /// <param name="artistService">
        /// The artist service.
        /// </param>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        public ArtistController(IArtistService artistService, IRepositoryFactory repositoryFactory)
        {
            this._artistService = artistService;
            this._repositoryFactory = repositoryFactory;
        }

        public ActionResult Create()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Edit(int? id)
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Delete(int? id)
        {
            throw new System.NotImplementedException();
        }
    }
}