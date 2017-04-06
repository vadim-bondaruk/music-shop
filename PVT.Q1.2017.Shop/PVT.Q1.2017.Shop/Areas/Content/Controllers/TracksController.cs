﻿namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;

    /// <summary>
    ///     The track controller
    /// </summary>
    public class TracksController : Controller
    {
        /// <summary>
        ///     The track service.
        /// </summary>
        private ITrackRepository trackRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TracksController" /> class.
        /// </summary>
        /// <param name="trackService">
        ///     The track service.
        /// </param>
        /// <param name="trackRepository"></param>
        public TracksController(ITrackService trackService, ITrackRepository trackRepository)
        {
            this.trackRepository = trackRepository;
            this.TrackService = trackService;
        }

        /// <summary>
        /// Gets or sets the _track service.
        /// </summary>
        public ITrackService TrackService { get; set; }

        /// <summary>
        ///     Shows all albums where the specified track is exist.
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        ///     All albums where the specified track is exist.
        /// </returns>
        public virtual ActionResult AlbumsList(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            return null; // this.View(this._trackService.GetAlbumsList(new Track { Id = id }));
        }

        /// <summary>
        ///     Shows track info.
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        ///     Track info view.
        /// </returns>
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            return this.View(this.TrackService.GetTrackDetails(id.Value));
        }

        /// <summary>
        ///     Shows all tracks.
        /// </summary>
        /// <returns>
        ///     All tracks view.
        /// </returns>
        public ActionResult List()
        {
            var tracks = this.TrackService.GetTrackDetailsViewModels();
            return this.View(tracks);
        }
    }
}