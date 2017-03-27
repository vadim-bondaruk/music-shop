namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    using PVT.Q1._2017.Shop.Areas.Management.Helpers;
    using PVT.Q1._2017.Shop.Areas.Management.ViewModels;

    /// <summary>
    /// </summary>
    public class AlbumsController : Controller
    {
        /// <summary>
        /// </summary>
        private readonly IAlbumRepository albumRepository;

        /// <summary>
        /// </summary>
        private readonly IAlbumService albumService;

        /// <summary>
        /// </summary>
        private readonly IArtistRepository artistRepository;

        /// <summary>
        /// </summary>
        private readonly IArtistService artistService;

        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumsController"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <param name="albumService">
        /// The album service.
        /// </param>
        /// <param name="artistService">
        /// The artist service.
        /// </param>
        /// <param name="albumRepository">
        /// The album repository.
        /// </param>
        /// <param name="artistRepository">
        /// The artist repository.
        /// </param>
        public AlbumsController(
            IRepositoryFactory repositoryFactory,
            IAlbumService albumService,
            IArtistService artistService,
            IAlbumRepository albumRepository,
            IArtistRepository artistRepository)
        {
            this.repositoryFactory = repositoryFactory;
            this.albumService = albumService;
            this.artistService = artistService;
            this.albumRepository = albumRepository;
            this.artistRepository = artistRepository;
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
        /// <param name="id">
        ///     The artist id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Edit(int id)
        {
            using (var albumRepo = this.repositoryFactory.GetAlbumRepository())
            {
                var album = albumRepo.GetById(id, a => a.Artist);
                if (album != null)
                {
                    var viewModel = ManagementMapper.GetAlbumManagementViewModel(album);
                    if (album.Artist != null)
                    {
                        viewModel.ArtistName = album.Artist.Name;
                    }
                    return this.View(viewModel);
                }
            }

            return this.View();
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
            using (var albumRepo = this.repositoryFactory.GetAlbumRepository())
            {
                var album = ManagementMapper.GetAlbumModel(viewModel);

                using (var artistRepo = this.artistRepository)
                {
                    var artist = artistRepo.GetById(viewModel.Artist.Id);
                    if (artist != null)
                    {
                        artist.Name = viewModel.ArtistName;
                        artistRepo.AddOrUpdate(artist);
                        artistRepo.SaveChanges();
                        album.Artist = artist;
                    }
                }

                albumRepo.AddOrUpdate(album);
                albumRepo.SaveChanges();
                return this.RedirectToAction("Details", new { id = album.Id, area = "Content", Controller = "Albums" });
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
            var artist = this.artistService.GetArtist(id);
            return this.View(new AlbumManagementViewModel { Artist = artist });
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
            Album album;
            using (var albumRepo = this.repositoryFactory.GetAlbumRepository())
            {
                var artist = this.artistService.GetArtist(viewModel.Artist.Id);

                if (artist != null)
                {
                    viewModel.Artist = artist;
                }

                album = ManagementMapper.GetAlbumModel(viewModel);
                albumRepo.AddOrUpdate(album);
                albumRepo.SaveChanges();
            }

            return this.RedirectToAction("Details", new { area = "Content", Controller = "Albums", id = album.Id });
        }
    }
}