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
    public class AlbumsController : Controller
    {
        /// <summary>
        /// </summary>
        private readonly IAlbumService albumService;

        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AlbumsController" /> class.
        /// </summary>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        /// <param name="albumService"></param>
        public AlbumsController(IRepositoryFactory repositoryFactory, IAlbumService albumService)
        {
            this.repositoryFactory = repositoryFactory;
            this.albumService = albumService;
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
        public virtual ActionResult Delete(AlbumManagementViewModel viewModel)
        {
            var albumModel = Mapper.Map<AlbumManagementViewModel, Album>(viewModel);
            using (var repository = this.repositoryFactory.GetAlbumRepository())
            {
                repository.Delete(albumModel);
                repository.SaveChanges();
            }

            return this.View();
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
        public virtual ActionResult New(
            [Bind(Include = "ArtistName, Name, ReleaseDate, UploadedImage")] AlbumManagementViewModel viewModel)
        {
            //this.albumService.SaveNewAlbum(viewModel);

            return this.View("New");
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public virtual ActionResult Update(AlbumManagementViewModel viewModel)
        {
            var albumRepo = this.repositoryFactory.GetAlbumRepository();
            var album = Mapper.Map<AlbumManagementViewModel, Album>(viewModel);
            albumRepo.AddOrUpdate(album);
            return this.View();
        }
    }
}