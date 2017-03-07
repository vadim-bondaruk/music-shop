using Shop.DAL.Infrastruture;

namespace PVT.Q1._2017.Shop.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using global::Shop.Common.Models;
    using global::Shop.Infrastructure;
    using global::Shop.Infrastructure.Services;

    /// <summary>
    /// The track controller
    /// </summary>
    [Authorize]
    public class TrackController : Controller
    {
        #region Fields

        /// <summary>
        /// The tracks service.
        /// </summary>
        private readonly IService<Track> _trackService;

        /// <summary>
        /// The repository factory.
        /// </summary>
        private readonly IFactory _repositoryFactory;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackController"/> class.
        /// </summary>
        /// <param name="trackService">
        /// The track service.
        /// </param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public TrackController(IService<Track> trackService, IFactory repositoryFactory)
        {
            this._trackService = trackService;
            this._repositoryFactory = repositoryFactory;
        }

        #endregion //Constructors

        #region Actions

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult Index()
        {
            using (var repository = this._repositoryFactory.Create<ITrackRepository>())
            {
                return this.View(repository.GetAll());
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult NewTrack()
        {
            return this.View();
        }

        /// <summary>
        /// </summary>
        /// <param name="newTrack">
        /// The new track.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public ActionResult NewTrack(Track newTrack)
        {
            if (this.ModelState.IsValid && this._trackService.IsValid(newTrack))
            {
                this._trackService.Register(newTrack);
                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        #endregion //Actions
    }
}