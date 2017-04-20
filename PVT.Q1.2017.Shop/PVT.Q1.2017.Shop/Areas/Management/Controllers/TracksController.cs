namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using global::Shop.BLL.Services.Infrastructure;
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
        /// Initializes a new instance of the <see cref="TracksController"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        /// <param name="serviceFactory">
        /// The service factory.
        /// </param>
        public TracksController(IRepositoryFactory repositoryFactory, IServiceFactory serviceFactory)
            : base(repositoryFactory, serviceFactory)
        {
            this.RepositoriesFactory = repositoryFactory;
            this.ServicesFactory = serviceFactory;
        }

        /// <summary>
        ///     Gets or sets the repository factory.
        /// </summary>
        public IRepositoryFactory RepositoriesFactory { get; set; }

        /// <summary>
        /// Gets or sets the service factory.
        /// </summary>
        public IServiceFactory ServicesFactory { get; set; }

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
            Track track;
            using (var repository = this.RepositoryFactory.GetTrackRepository())
            {
                track = repository.GetById(id);
            }

            var trackManagementViewModel = ManagementMapper.GetTrackManagementViewModel(track);

            using (var repository = this.RepositoryFactory.GetGenreRepository())
            {
                var genres = repository.GetAll();
                trackManagementViewModel.Genres = genres;
            }

            return this.View(trackManagementViewModel);
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

            if (viewModel.PostedImage == null && viewModel.Image == null)
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
            using (var repo = this.RepositoryFactory.GetGenreRepository())
            {
                genres = repo.GetAll();
            }

            using (var repository = this.RepositoryFactory.GetArtistRepository())
            {
                var artist = repository.GetById(id);
                return this.View(new TrackManagementViewModel { Artist = artist, Genres = genres });
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
    }
}