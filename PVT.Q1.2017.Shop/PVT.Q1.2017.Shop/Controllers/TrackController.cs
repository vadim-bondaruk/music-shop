namespace PVT.Q1._2017.Shop.Controllers
{
    using System.Web.Mvc;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;

    /// <summary>
    /// The track controller
    /// </summary>
    public class TrackController : Controller
    {
        /// <summary>
        /// The track service.
        /// </summary>
        private readonly ITrackService _trackService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackController"/> class.
        /// </summary>
        /// <param name="trackService">
        ///     The track service.
        /// </param>
        public TrackController(ITrackService trackService)
        {
            this._trackService = trackService;
        }

        /// <summary>
        /// Shows all tracks.
        /// </summary>
        /// <returns>
        /// All tracks view.
        /// </returns>
        public ActionResult List()
        {
            return this.View(this._trackService.GetTracksList());
        }

        /// <summary>
        /// Shows track info.
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        /// Track info view.
        /// </returns>
        public virtual ActionResult Details(int id)
        {
            return this.View(this._trackService.GetTrackInfo(id));
        }

        /// <summary>
        /// Shows all albums where the specified track is exist.
        /// </summary>
        /// <param name="id">The track id.</param>
        /// <returns>
        /// All albums where the specified track is exist.
        /// </returns>
        public virtual ActionResult AlbumsList(int id)
        {
            return null; // this.View(this._trackService.GetAlbumsList(new Track { Id = id }));
        }
    }
}