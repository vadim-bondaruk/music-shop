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
        public AlbumController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory) : base(repositoryFactory, serviceFactory)
        {
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
                using (var repository = RepositoryFactory.GetArtistRepository())
                {
                    artist = ModelsMapper.GetArtistViewModel(repository.GetById(artistId.Value));
                }

                if (artist != null)
                {
                    var album = new AlbumManagementViewModel
                    {
                        ArtistId = artist.Id,
                        ArtistName = artist.Name
                    };

                    return View(album);
                }
            }

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,Name,ReleaseDate,ArtistId,PostedCover")]
            AlbumManagementViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var album = ManagementMapper.GetAlbumModel(model);
                using (var repository = RepositoryFactory.GetAlbumRepository())
                {
                    if (CurrentUser != null && !CurrentUser.IsInRole(UserRoles.Admin.ToString()))
                    {
                        album.OwnerId = CurrentUser.Id;
                    }

                    repository.AddOrUpdate(album);
                    repository.SaveChanges();
                }

                return RedirectToAction("Details", "Album", new { id = album.Id, area = "Content" });
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List", "Album", new { area = "Content" });
            }

            var albumService = ServiceFactory.GetAlbumService();
            var album = ManagementMapper.GetAlbumManagementViewModel(albumService.GetAlbumDetails(id.Value));
            if (album == null)
            {
                return HttpNotFound("Альбом с указанным id не найден");
            }

            return View(album);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,Name,ReleaseDate,ArtistId,PostedCover")]
            AlbumManagementViewModel album)
        {
            if (album != null && ModelState.IsValid)
            {
                using (var repository = RepositoryFactory.GetAlbumRepository())
                {
                    var currentAlbum = repository.GetById(album.Id);
                    if (currentAlbum == null)
                    {
                        return HttpNotFound($"Альбом с id = { album.Id } не найден.");
                    }

                    int? ownerId = currentAlbum.OwnerId;
                    var albumDto = ManagementMapper.GetAlbumModel(album);
                    albumDto.OwnerId = ownerId;

                    repository.AddOrUpdate(albumDto);
                    repository.SaveChanges();
                }

                return RedirectToAction("Details", "Album", new { id = album.Id, area = "Content" });
            }

            return View(album);
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
                return RedirectToAction("List", "Album", new { area = "Content" });
            }

            var albumService = ServiceFactory.GetAlbumService();
            var album = ManagementMapper.GetAlbumManagementViewModel(albumService.GetAlbumDetails(id.Value));
            return View(album);
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
                using (var repository = RepositoryFactory.GetAlbumRepository())
                {
                    repository.Delete(ManagementMapper.GetAlbumModel(album));
                    repository.SaveChanges();
                }
            }

            return RedirectToAction("List", "Album", new { area = "Content" });
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
        public ActionResult AddTracksToAlbum(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List", "Track", new { area = "Content" });
            }

            AlbumTracksListViewModel albumTracksViewModel;

            var albumService = ServiceFactory.GetAlbumService();
            if (CurrentUser != null && CurrentUserCurrency != null)
            {
                albumTracksViewModel = albumService.GetTracksToAdd(id.Value, CurrentUserCurrency.Code, CurrentUser.PriceLevelId);
            }
            else
            {
                albumTracksViewModel = albumService.GetTracksToAdd(id.Value);
            }

            if (albumTracksViewModel == null)
            {
                return HttpNotFound($"Альбом с id = { id.Value } не найден");
            }

            return View(albumTracksViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddTrackToAlbum(int id, int trackId)
        {
            using (var repository = RepositoryFactory.GetAlbumTrackRelationRepository())
            {
                repository.AddOrUpdate(new AlbumTrackRelation
                {
                    AlbumId = id,
                    TrackId = trackId
                });
                repository.SaveChanges();
            }

            return RedirectToAction("AddTracksToAlbum", "Album", new { id = id, area = "Management" });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RemoveTrack(int id, int trackId)
        {
            using (var repository = RepositoryFactory.GetAlbumTrackRelationRepository())
            {
                var albumTrackRelation = repository.FirstOrDefault(r => r.AlbumId == id && r.TrackId == trackId);
                if (albumTrackRelation != null)
                {
                    repository.Delete(albumTrackRelation);
                    repository.SaveChanges();
                }
            }

            return RedirectToAction("Details", "Album", new { id = id, area = "Content" });
        }
    }
}