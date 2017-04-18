namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.BLL.Utils;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Enums;

    using PVT.Q1._2017.Shop.App_Start;
    using PVT.Q1._2017.Shop.Areas.Management.Helpers;
    using PVT.Q1._2017.Shop.Areas.Management.ViewModels;
    using PVT.Q1._2017.Shop.Controllers;

    /// <summary>
    ///     The track controller
    /// </summary>
    [ShopAuthorize(UserRoles.Admin, UserRoles.Seller)]
    public class TracksController : BaseController
    {
        /// <summary>
        /// </summary>
        private readonly IAlbumRepository _albumRepository;

        /// <summary>
        /// </summary>
        private readonly IArtistRepository _artistRepository;

        /// <summary>
        /// </summary>
        private readonly IArtistService _artistService;

        /// <summary>
        /// </summary>
        private readonly IGenreRepository _genreRepository;

        /// <summary>
        ///     The track service.
        /// </summary>
        private readonly ITrackService _trackService;

        /// <summary>
        /// </summary>
        private readonly ITrackRepository _trackRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TracksController"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <param name="artistService">
        /// The artist service.
        /// </param>
        /// <param name="artistRepository">
        /// The artist repository.
        /// </param>
        /// <param name="genreRepository">
        /// The genre repository.
        /// </param>
        /// <param name="albumRepository">
        /// The album repository.
        /// </param>
        /// <param name="trackRepository">
        /// The track repository.
        /// </param>
        public TracksController(
            IRepositoryFactory repositoryFactory,
            IArtistService artistService,
            IArtistRepository artistRepository,
            IGenreRepository genreRepository,
            IAlbumRepository albumRepository,
            ITrackRepository trackRepository)
        {
            this.RepositoryFactory = repositoryFactory;
            this._artistService = artistService;
            this._artistRepository = artistRepository;
            this._genreRepository = genreRepository;
            this._albumRepository = albumRepository;
            this._trackRepository = trackRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TracksController"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <param name="trackService">
        /// The track service.
        /// </param>
        /// <param name="artistService">
        /// The artist service.
        /// </param>
        /// <param name="artistRepository">
        /// The artist repository.
        /// </param>
        /// <param name="genreRepository">
        /// The genre repository.
        /// </param>
        /// <param name="albumRepository">
        /// The album repository.
        /// </param>
        /// <param name="trackRepository">
        /// The track repository.
        /// </param>
        public TracksController(
            IRepositoryFactory repositoryFactory,
            ITrackService trackService,
            IArtistService artistService,
            IArtistRepository artistRepository,
            IGenreRepository genreRepository,
            IAlbumRepository albumRepository,
            ITrackRepository trackRepository)
        {
            this.RepositoryFactory = repositoryFactory;
            this._trackService = trackService;
            this._artistService = artistService;
            this._artistRepository = artistRepository;
            this._genreRepository = genreRepository;
            this._albumRepository = albumRepository;
            this._trackRepository = trackRepository;
            Mapper.Initialize(cfg => cfg.CreateMap<TrackManagementViewModel, Track>());
        }

        /// <summary>
        ///     Gets or sets the repository factory.
        /// </summary>
        public IRepositoryFactory RepositoryFactory { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="trackId">
        ///     The track id.
        /// </param>
        /// <param name="model"></param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(TrackManagementViewModel model)
        {
            var trackModel = Mapper.Map<TrackManagementViewModel, Track>(model);
            using (var repository = this.RepositoryFactory.GetTrackRepository())
            {
                repository.Delete(trackModel);
                repository.SaveChanges();
            }

            return this.RedirectToAction("List", "Tracks", new { area = "Content" });
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public virtual ActionResult Edit(int id)
        {
            var track = this._trackRepository.GetById(id);
            var trackManagementViewModel = ManagementMapper.GetTrackManagementViewModel(track);
            var genres = this._genreRepository.GetAll();
            trackManagementViewModel.Genres = genres;
            return this.View(trackManagementViewModel);
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
            ICollection<Genre> genres;
            using (var repo = this._genreRepository)
            {
                genres = repo.GetAll();
            }

            var artist = this._artistRepository.GetById(id);
            return this.View(new TrackManagementViewModel { Artist = artist, Genres = genres });
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
        public virtual ActionResult New(TrackManagementViewModel viewModel)
        {
            var track = ManagementMapper.GetTrackModel(viewModel);

            if (this.CurrentUser != null && this.CurrentUser.IsInRole(UserRoles.Seller))
            {
                track.OwnerId = this.CurrentUser.Id;
            }

            using (var repository = this.RepositoryFactory.GetTrackRepository())
            {
                repository.AddOrUpdate(track);
                repository.SaveChanges();
            }

            return this.RedirectToAction("Details", "Tracks", new { id = track.Id, area = "Content" });
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel">
        /// The view model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TrackManagementViewModel viewModel)
        {
            Track currentTrack;

            using (var repo = this.RepositoryFactory.GetTrackRepository())
            {
                currentTrack = repo.GetById(viewModel.Id);
            }

            if (currentTrack == null)
            {
                return this.HttpNotFound($"Трек с id = {viewModel.Id} не найден");
            }

            if (viewModel.PostedTrackFile == null && viewModel.TrackFile == null)
            {
                viewModel.TrackFile = currentTrack.TrackFile;
            }

            if ((viewModel.PostedImage == null) && (viewModel.Image == null))
            {
                viewModel.Image = currentTrack.Image;
            }

            var track = ManagementMapper.GetTrackModel(viewModel);
            track.OwnerId = currentTrack.OwnerId;
            using (var repository = this.RepositoryFactory.GetTrackRepository())
            {
                repository.AddOrUpdate(track);
                repository.SaveChanges();
            }

            return this.RedirectToAction("Details", "Tracks", new { id = track.Id, area = "Content" });
        }
    }
}