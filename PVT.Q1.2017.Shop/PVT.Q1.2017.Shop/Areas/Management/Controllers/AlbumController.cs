namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    using PVT.Q1._2017.Shop.Areas.Management.Models;

    /// <summary>
    ///     The track controller
    /// </summary>
    public class AlbumsController : Controller
    {
        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AlbumsController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        public AlbumsController(IRepositoryFactory repositoryFactory)
        {
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
        public virtual ActionResult Delete(AlbumManagmentViewModel model)
        {
            var albumModel = Mapper.Map<Album>(model);
            using (var repository = this.repositoryFactory.GetAlbumRepository())
            {
                repository.Delete(albumModel);
                repository.SaveChanges();
            }

            return this.View("AlbumManage");
        }

        /// <summary>
        /// </summary>
        /// <param name="albumId">
        ///     The album id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int albumId)
        {
            using (var repository = this.repositoryFactory.GetAlbumRepository())
            {
                var album = repository.GetById(albumId);
                Mapper.Map<Album>(album);
            }

            return this.View("AlbumManage");
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public virtual ActionResult New()
        {
            return this.View("AlbumManage");
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
        public virtual ActionResult New([Bind(Include = "Name, Cover, ReleaseDate")] AlbumManagmentViewModel viewModel)
        {
            var album = Mapper.Map<Album>(viewModel);
            using (var repository = this.repositoryFactory.GetAlbumRepository())
            {
                repository.AddOrUpdate(album);
                repository.SaveChanges();
            }

            return this.View("AlbumManage");
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Update(AlbumManagmentViewModel model)
        {
            using (var repository = this.repositoryFactory.GetAlbumRepository())
            {
                var album = Mapper.Map<Album>(model);
                repository.AddOrUpdate(album);
                repository.SaveChanges();
            }

            return this.View();
        }
    }
}