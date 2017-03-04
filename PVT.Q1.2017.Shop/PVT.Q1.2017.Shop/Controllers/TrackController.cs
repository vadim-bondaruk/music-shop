namespace PVT.Q1._2017.Shop.Controllers
{
    #region

    using System.Web.Mvc;

    using global::Shop.Common.Models;
    using global::Shop.Infrastructure.Repositories;

    #endregion

    /// <summary>
    /// The track controller
    /// </summary>
    public partial class TrackController : Controller
    {
        #region Fields

        /// <summary>
        /// The repository factory.
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackController"/> class.
        /// </summary>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        public TrackController(IRepositoryFactory repositoryFactory)
        {
            this._repositoryFactory = repositoryFactory;
        }

        #endregion //Constructors

        #region Actions

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public virtual ActionResult AlbumList()
        {
            using (var repository = this._repositoryFactory.CreateRepository<Album>())
            {
                return this.View(repository.GetAll());
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <returns>
        /// </returns>
        public virtual ActionResult AlbumList(int artistId)
        {
            using (var repository = this._repositoryFactory.CreateRepository<Album>())
            {
                return this.View(repository.GetAll(a => a.Artist.Id.Equals(artistId)));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// </returns>
        public virtual ActionResult AlbumTracks(int id)
        {
            using (var repository = this._repositoryFactory.CreateRepository<Track>())
            {
                return this.View(repository.GetAll(t => t.Album.Id.Equals(id)));
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public virtual ActionResult ArtistList()
        {
            using (var repository = this._repositoryFactory.CreateRepository<Artist>())
            {
                return this.View(repository.GetAll());
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// </returns>
        public virtual ActionResult ArtistTracks(int id)
        {
            using (var repository = this._repositoryFactory.CreateRepository<Track>())
            {
                return this.View(repository.GetAll(t => t.Artist.Id.Equals(id)));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int id)
        {
            using (var repository = this._repositoryFactory.CreateRepository<Track>())
            {
                return this.View(repository.GetById(id));
            }
        }

        /// <summary>
        /// Diplays tracks list.
        /// </summary>
        /// <returns>
        /// The view that renders tracks list.
        /// </returns>
        public virtual ActionResult TrackList()
        {
            using (var repository = this._repositoryFactory.CreateRepository<Track>())
            {
                return this.View(repository.GetAll());
            }
        }

        #endregion //Actions
    }
}