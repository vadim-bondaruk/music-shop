namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

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

            return this.View(this._albumService.GetAlbumInfo(id.Value));
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
        public ActionResult AddOrUpdate([Bind(Include = "Id,Name,ReleaseDate,ArtistId")] Album album)
        {
            if (ModelState.IsValid)
            {
                using (var repository = this._repositoryFactory.GetAlbumRepository())
                {
                    repository.AddOrUpdate(album);
                    repository.SaveChanges();
                }

                return this.RedirectToAction("Details", "Album", new { id = album.Id, area = string.Empty });
            }

            return this.View(album);
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
                return this.RedirectToAction("List", "Album", new { area = string.Empty });
            }

            return this.View(this._albumService.GetAlbumInfo(id.Value));
        }

        /// <summary>
        /// Deletes the specified <paramref name="album"/> from the system.
        /// </summary>
        /// <param name="album">
        /// The album to delete.
        /// </param>
        /// <returns>
        /// Redirects to the view which generates page with albums list.
        /// </returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id")] Album album)
        {
            if (album != null && ModelState.IsValid)
            {
                using (var repository = this._repositoryFactory.GetAlbumRepository())
                {
                    repository.Delete(album);
                    repository.SaveChanges();
                }
            }

            return this.RedirectToAction("List", "Album", new { area = string.Empty });
        }
    }
}