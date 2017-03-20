namespace PVT.Q1._2017.Shop.Areas.Management.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using ViewModels;

    /// <summary>
    /// The track controller
    /// </summary>
    public class TrackController : Controller
    {
        /// <summary>
        /// The tracks service.
        /// </summary>
        private readonly ITrackService _trackService;

        /// <summary>
        /// The repository factory.
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackController"/> class.
        /// </summary>
        /// <param name="trackService">
        /// The track service.
        /// </param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public TrackController(ITrackService trackService, IRepositoryFactory repositoryFactory)
        {
            this._trackService = trackService;
            this._repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Displays the page for adding and editing tracks.
        /// </summary>
        /// <returns>
        /// The view which generates page for adding and editing tracks.
        /// </returns>
        public ActionResult AddOrUpdate(int? id)
        {
            if (id == null)
            {
                return this.View();
            }

            var track = Mapper.Map<TrackManagementViewModel>(this._trackService.GetTrackDetails(id.Value));
            return this.View(track);
        }

        /// <summary>
        /// Adds the new track in the system or edit existing track.
        /// </summary>
        /// <param name="track">
        /// The track to add or edit.
        /// </param>
        /// <returns>
        /// Redirects to view which displays track details in case if success;
        /// otherwise returns the view whitch displays the currnet track with error.
        /// </returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate(
            [Bind(Include = "Id,Name,Duration,ReleaseDate,Duration,Artist.Id,Genre.Id,TrackFile,Image")]
            TrackManagementViewModel track)
        {
            if (track != null && ModelState.IsValid)
            {
                using (var repository = this._repositoryFactory.GetTrackRepository())
                {
                    repository.AddOrUpdate(Mapper.Map<Track>(track));
                    repository.SaveChanges();
                }

                return this.RedirectToAction("Details", "Track", new { id = track.Id, area = "Content" });
            }

            return this.View(track);
        }

        /// <summary>
        /// Deletes the track with the specified <paramref name="id"/> from the system.
        /// </summary>
        /// <param name="id">
        /// The track id.
        /// </param>
        /// <returns>
        /// The view which generates page for deleting tracks in case if <paramref name="id"/> was specified;
        /// otherwise redirects to the list of tracks.
        /// </returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List", "Track", new { area = "Content" });
            }

            var track = Mapper.Map<TrackManagementViewModel>(this._trackService.GetTrackDetails(id.Value));
            return this.View(track);
        }

        /// <summary>
        /// Deletes the specified <paramref name="track"/> from the system.
        /// </summary>
        /// <param name="track">
        /// The track to delete.
        /// </param>
        /// <returns>
        /// Redirects to the view which generates page with tracks list.
        /// </returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id")] TrackManagementViewModel track)
        {
            if (track != null && ModelState.IsValid)
            {
                using (var repository = this._repositoryFactory.GetTrackRepository())
                {
                    repository.Delete(Mapper.Map<Track>(track));
                    repository.SaveChanges();
                }
            }

            return this.RedirectToAction("List", "Track", new { area = "Content" });
        }
    }
}