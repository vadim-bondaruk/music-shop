namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Helpers;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.ViewModels;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Enums;
    using Helpers;
    using Shop.Controllers;
    using ViewModels;

    /// <summary>
    /// The album controller.
    /// </summary>
    public class AlbumController : BaseController
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
        /// Creates a new album in the system.
        /// </summary>
        /// <param name="artistId">
        /// The artist for which a new album will be created.
        /// </param>
        /// <returns>
        /// The view which renders a page for adding new albums.
        /// </returns>
        public ActionResult Create(int? artistId)
        {
            if (artistId != null)
            {
                ArtistViewModel artist;
                using (var repository = _repositoryFactory.GetArtistRepository())
                {
                    artist = ModelsMapper.GetArtistViewModel(repository.GetById(artistId.Value));
                }
                if (artist != null)
                {
                    var album = new AlbumManagementViewModel
                    {
                        Artist = artist
                    };

                    return this.View(album);
                }
            }

            return this.View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,Name,ReleaseDate,Artist.Id,Cover")]
            AlbumManagementViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var album = ManagementMapper.GetAlbumModel(model);
                using (var repository = this._repositoryFactory.GetAlbumRepository())
                {
                    if (CurrentUser != null && !CurrentUser.IsInRole(UserRoles.Admin.ToString()))
                    {
                        album.OwnerId = CurrentUser.Id;
                    }

                    repository.AddOrUpdate(album);
                    repository.SaveChanges();
                }

                return this.RedirectToAction("Details", "Album", new { id = album.Id, area = "Content" });
            }

            return this.View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List", "Album", new { area = "Content" });
            }

            var album = ManagementMapper.GetAlbumManagementViewModel(this._albumService.GetAlbumDetails(id.Value));
            if (album == null)
            {
                return HttpNotFound("Альбок с указанным id не найден");
            }

            return this.View(album);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(
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
        public ActionResult Delete([Bind(Include = "Id,Name")] AlbumManagementViewModel album)
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
        public ActionResult AddTracks(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List", "Track", new { area = "Content" });
            }

            var currency = GetCurrentUserCurrency();
            AlbumTracksListViewModel albumTracksViewModel;

            if (currency != null)
            {
                var priceLevel = GetCurrentUserPriceLevel();
                albumTracksViewModel = _albumService.GetTracksToAdd(id.Value, currency.Code, priceLevel);
            }
            else
            {
                albumTracksViewModel = _albumService.GetTracksToAdd(id.Value);
            }

            if (albumTracksViewModel == null)
            {
                return HttpNotFound($"Альбом с id = { id.Value } не найден");
            }

            return this.View(albumTracksViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddTrack(int id, int trackId)
        {
            using (var repository = _repositoryFactory.GetAlbumTrackRelationRepository())
            {
                repository.AddOrUpdate(new AlbumTrackRelation
                {
                    AlbumId = id,
                    TrackId = trackId
                });
                repository.SaveChanges();
            }

            return RedirectToAction("AddTracks", "Album", new { id = id, area = "Management" });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RemoveTrack(int id, int trackId)
        {
            using (var repository = _repositoryFactory.GetAlbumTrackRelationRepository())
            {
                var albumTrackRelation = repository.FirstOrDefault(r => r.AlbumId == id && r.TrackId == trackId);
                if (albumTrackRelation != null)
                {
                    repository.Delete(albumTrackRelation);
                    repository.SaveChanges();
                }
            }

            return RedirectToAction("TracksList", "Album", new { id = id, area = "Content" });
        }
    }
}