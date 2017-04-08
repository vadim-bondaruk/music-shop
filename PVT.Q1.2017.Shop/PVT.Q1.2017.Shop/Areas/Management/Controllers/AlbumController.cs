namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;
    using Helpers;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.DAL.Infrastruture;
    using ViewModels;

    /// <summary>
    /// The album controller.
    /// </summary>
    public class AlbumController : Controller
    {
        /// <summary>
        /// The album  service.
        /// </summary>
        private readonly IAlbumService _albumService;

        /// <summary>
        /// The repository factory.
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumController"/> class.
        /// </summary>
        /// <param name="albumService">
        /// The album service.
        /// </param>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        public AlbumController(IAlbumService albumService, IRepositoryFactory repositoryFactory)
        {
            this._albumService = albumService;
            this._repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Displays the page for adding and editing albums.
        /// </summary>
        /// <returns>
        /// The view which generates page for adding and editing albums.
        /// </returns>
        public ActionResult AddOrUpdate(int? id)
        {
            if (id == null)
            {
                return this.View();
            }

            var album = ManagementMapper.GetAlbumManagementViewModel(this._albumService.GetAlbumDetails(id.Value));
            return this.View(album);
        }

        /// <summary>
        /// Adds the new album in the system or edit existing album.
        /// </summary>
        /// <param name="album">
        /// The album to add or edit.
        /// </param>
        /// <returns>
        /// Redirects to view which displays album details in case if success;
        /// otherwise returns the view whitch displays the currnet album with error.
        /// </returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate(
            [Bind(Include = "Id,Name,ReleaseDate,Artist.Id,Cover")]
            AlbumManagementViewModel album)
        {
            if (album != null && ModelState.IsValid)
            {
                using (var repository = this._repositoryFactory.GetAlbumRepository())
                {
                    repository.AddOrUpdate(ManagementMapper.GetAlbumModel(album));
                    repository.SaveChanges();
                }

                return this.RedirectToAction("Details", "Album", new { id = album.Id, area = "Content" });
            }

            return this.View(album);
        }

        public ActionResult Create(int? artistid)
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Edit(int? id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Deletes the album with the specified <paramref name="id"/> from the system.
        /// </summary>
        /// <param name="id">
        /// The album id.
        /// </param>
        /// <returns>
        /// The view which generates page for deleting albums in case if <paramref name="id"/> was specified;
        /// otherwise redirects to the list of albums.
        /// </returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List", "Album", new { area = "Content" });
            }

            var album = ManagementMapper.GetAlbumManagementViewModel(this._albumService.GetAlbumDetails(id.Value));
            return this.View(album);
        }

        /// <summary>
        /// Deletes the specified album from the system.
        /// </summary>
        /// <param name="album">
        /// The album to delete.
        /// </param>
        /// <returns>
        /// Redirects to the view which generates page with albums list.
        /// </returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id")] AlbumManagementViewModel album)
        {
            if (album != null && ModelState.IsValid)
            {
                using (var repository = this._repositoryFactory.GetAlbumRepository())
                {
                    repository.Delete(ManagementMapper.GetAlbumModel(album));
                    repository.SaveChanges();
                }
            }

            return this.RedirectToAction("List", "Album", new { area = "Content" });
        }

        /// <summary>
        /// Renders a page for adding tracks to the album.
        /// </summary>
        /// <param name="id">
        /// The album id.
        /// </param>
        /// <returns>
        /// The view for adding tracks to the album.
        /// </returns>
        public ActionResult AddTracks(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}