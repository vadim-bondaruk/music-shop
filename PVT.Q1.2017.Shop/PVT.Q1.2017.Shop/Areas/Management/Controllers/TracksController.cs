﻿namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using Models;

    /// <summary>
    ///     The track controller
    /// </summary>
    public class TracksController : Controller
    {
        /// <summary>
        ///     The track service.
        /// </summary>
        private readonly ITrackService trackService;

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
        public virtual ActionResult Delete(TrackManagmentViewModel model)
        {
            var trackModel = Mapper.Map<TrackManagmentViewModel, Track>(model);
            using (var repository = this.RepositoryFactory.GetTrackRepository())
            {
                repository.Delete(trackModel);
                repository.SaveChanges();
            }

            return this.View("TrackManage");
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="trackId"></param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int trackId)
        {
            var track = this.trackService.GetTrackInfo(trackId);
            var trackViewModel = Mapper.Map<Track, TrackManagmentViewModel>(track);
            return this.View(trackViewModel);
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
            return this.View(
                "TrackManage",
                new TrackManagmentViewModel
                    {
                        Artist = new Artist { Name = "SomeArtist" },
                        Track = new Track { Name = "SomeTrack" }
                    });
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns />
        [HttpPost]
        public virtual ActionResult New(TrackManagmentViewModel model)
        {
            var trackRepo = this.RepositoryFactory.GetTrackRepository();
            var track = Mapper.Map<TrackManagmentViewModel, Track>(model);
            trackRepo.AddOrUpdate(track);
            return this.View("TrackManage");
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        ///     The model.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public virtual ActionResult Update(TrackManagmentViewModel model)
        {
            var trackRepo = this.RepositoryFactory.GetTrackRepository();
            var track = Mapper.Map<TrackManagmentViewModel, Track>(model);
            trackRepo.AddOrUpdate(track);
            return this.View();
        }
    }
}