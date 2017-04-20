namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Enums;

    using PVT.Q1._2017.Shop.App_Start;
    using PVT.Q1._2017.Shop.Areas.Management.Helpers;
    using PVT.Q1._2017.Shop.Areas.Management.ViewModels;
    using PVT.Q1._2017.Shop.Controllers;

    /// <summary>
    /// </summary>
    [ShopAuthorize( UserRoles.Admin, UserRoles.Seller)]
    public class AlbumsController : BaseController
    {
        public AlbumsController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory) : base(repositoryFactory, serviceFactory)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(int id)
        {
            using (var repository = RepositoryFactory.GetAlbumRepository())
            {
                repository.Delete(id);
                repository.SaveChanges();
            }

            return RedirectToAction("List", "Albums", new { area = "Content" });
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Edit(int id)
        {
            using (var albumRepo = RepositoryFactory.GetAlbumRepository())
            {
                var album = albumRepo.GetById(id, a => a.Artist);
                if (album != null)
                {
                    var viewModel = ManagementMapper.GetAlbumManagementViewModel(album);
                    if (album.Artist != null)
                    {
                        viewModel.ArtistName = album.Artist.Name;
                    }

                    using (var realtionsRepo = RepositoryFactory.GetAlbumTrackRelationRepository())
                    {
                        var tracks = realtionsRepo.GetAll(t => t.AlbumId == id, t => t.Track).Select(t => t.Track).ToList();

                        ViewBag.Tracks = tracks;
                    }

                    return View(viewModel);
                }
            }

            return View();
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
        public virtual ActionResult Edit(
            [Bind(Include = "Id, Artist, ArtistName, Name, ReleaseDate, Cover, PostedCover")] AlbumManagementViewModel
                viewModel)
        {
            using (var albumRepo = RepositoryFactory.GetAlbumRepository())
            {
                var album = ManagementMapper.GetAlbumModel(viewModel);

                using (var artistRepo = RepositoryFactory.GetArtistRepository())
                {
                    var artist = artistRepo.GetById(viewModel.Artist.Id);
                    if (artist != null)
                    {
                        artist.Name = viewModel.ArtistName;
                        artistRepo.AddOrUpdate(artist);
                        artistRepo.SaveChanges();
                        album.Artist = artist;
                        album.ArtistId = artist.Id;
                        albumRepo.AddOrUpdate(album);
                        albumRepo.SaveChanges();
                        artistRepo.AddOrUpdate(artist);
                        artistRepo.SaveChanges();
                    }
                }

                return RedirectToAction("Details", new { id = album.Id, area = "Content", Controller = "Albums" });
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult New(int id)
        {
            using (var artistRepository = RepositoryFactory.GetArtistRepository())
            {
                return View(new AlbumManagementViewModel { Artist = artistRepository.GetById(id) });
            }
            
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
            [Bind(Include = "Artist, Name, ReleaseDate, Cover, PostedCover")] AlbumManagementViewModel viewModel)
        {
            var album = ManagementMapper.GetAlbumModel(viewModel);
            using (var albumRepo = RepositoryFactory.GetAlbumRepository())
            {
                albumRepo.AddOrUpdate(album);
                albumRepo.SaveChanges();
            }

            return RedirectToAction("Details", new { area = "Content", Controller = "Albums", id = album.Id });
        }
    }
}