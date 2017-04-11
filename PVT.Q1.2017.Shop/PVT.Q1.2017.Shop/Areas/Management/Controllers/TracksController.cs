namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    using PVT.Q1._2017.Shop.Areas.Management.Helpers;
    using PVT.Q1._2017.Shop.Areas.Management.ViewModels;

    /// <summary>
    ///     The track controller
    /// </summary>
    public class TracksController : Controller
    {
        /// <summary>
        /// </summary>
        private readonly IAlbumRepository albumRepository;

        /// <summary>
        /// </summary>
        private readonly IArtistRepository artistRepository;

        /// <summary>
        /// </summary>
        private readonly IArtistService artistService;

        /// <summary>
        /// </summary>
        private readonly IGenreRepository genreRepository;

        /// <summary>
        ///     The track service.
        /// </summary>
        private readonly ITrackService trackService;

        /// <summary>
        /// </summary>
        private ITrackRepository TrackRepository;

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
            this.artistService = artistService;
            this.artistRepository = artistRepository;
            this.genreRepository = genreRepository;
            this.albumRepository = albumRepository;
            this.TrackRepository = trackRepository;
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
            this.trackService = trackService;
            this.artistService = artistService;
            this.artistRepository = artistRepository;
            this.genreRepository = genreRepository;
            this.albumRepository = albumRepository;
            this.TrackRepository = trackRepository;
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
            var track = this.TrackRepository.GetById(id);
            var trackManagementViewModel = ManagementMapper.GetTrackManagementViewModel(track);
            var genres = this.genreRepository.GetAll();
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
            using (var repo = this.genreRepository)
            {
                genres = repo.GetAll();
            }

            var artist = this.artistRepository.GetById(id);
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
            using (var repository = this.RepositoryFactory.GetTrackRepository())
            {
                repository.AddOrUpdate(track);
                repository.SaveChanges();
            }

            return this.RedirectToAction("Details", "Tracks", new { id = track.Id, area = "Content" });
        }
    }
}