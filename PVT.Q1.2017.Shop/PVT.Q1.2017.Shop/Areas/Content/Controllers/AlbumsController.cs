namespace PVT.Q1._2017.Shop.Areas.Content.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using global::Shop.BLL.Helpers;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.Common.Utils;
    using global::Shop.DAL.Infrastruture;

    using PVT.Q1._2017.Shop.Controllers;

    /// <summary>
    ///     The album controller.
    /// </summary>
    public class AlbumsController : BaseController
    {
        /// <summary>
        ///     The album service.
        /// </summary>
        private readonly IAlbumService albumService;

        /// <summary>
        /// </summary>
        private readonly IAlbumTrackRelationRepository albumTrackRelationRepository;

        /// <summary>
        /// </summary>
        private readonly IArtistRepository artistRepository;

        /// <summary>
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        /// </summary>
        private readonly ITrackRepository trackRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AlbumsController" /> class.
        /// </summary>
        /// <param name="albumService">
        ///     The album service.
        /// </param>
        /// <param name="repositoryFactory">
        ///     The repository factory.
        /// </param>
        /// <param name="artistRepository"></param>
        /// <param name="trackRepository"></param>
        /// <param name="albumTrackRelationRepository"></param>
        public AlbumsController(
            IAlbumService albumService,
            IRepositoryFactory repositoryFactory,
            IArtistRepository artistRepository,
            ITrackRepository trackRepository,
            IAlbumTrackRelationRepository albumTrackRelationRepository)
        {
            this.albumService = albumService;
            this.repositoryFactory = repositoryFactory;
            this.artistRepository = artistRepository;
            this.trackRepository = trackRepository;
            this.albumTrackRelationRepository = albumTrackRelationRepository;
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        /// </returns>
        public virtual ActionResult Details(int id)
        {
            Album album;
            using (var albumRepo = this.repositoryFactory.GetAlbumRepository())
            {
                album = albumRepo.GetById(id, m => m.Artist);
            }

            var viewModel = ModelsMapper.GetAlbumDetailsViewModel(album);

            using (var artistRepo = this.repositoryFactory.GetArtistRepository())
            {
                if (album.ArtistId == null)
                {
                    return this.View(viewModel);
                }

                var artistId = album.ArtistId ?? default(int);
                var artist = artistRepo.GetById(artistId);
                if (artist == null)
                {
                    return this.View(viewModel);
                }

                /*viewModel.ArtistName = artist.Name;
                viewModel.Artist = artist;*/

                using (var trackRepo = this.trackRepository)
                {
                    using (var albumTrackRepo = this.albumTrackRelationRepository)
                    {
                        var albumTrackRelations = albumTrackRepo.GetAll(t => t.AlbumId == id);
                        if (albumTrackRelations == null)
                        {
                            return this.View(viewModel);
                        }

                        foreach (var relation in albumTrackRelations)
                        {
                            //viewModel.Tracks.Add(trackRepo.GetById(relation.TrackId));
                        }
                    }
                }
            }

            return this.View(viewModel);
        }

        /// <summary>
        ///     Shows all albums.
        /// </summary>
        /// <returns>
        ///     All albums view.
        /// </returns>
        public ActionResult List()
        {
            var currency = this.GetCurrentUserCurrency();
            if (currency != null)
            {
                var priceLevel = this.GetCurrentUserPriceLevel();
                return this.View(/*this.albumService.GetAlbumsList(currency.Code, priceLevel)*/);
            }

            return this.View(/*this.albumService.GetAllViewModels()*/);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>
        /// </returns>
        public virtual ActionResult TracksList(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("List");
            }

            return this.View(this.albumService.GetTracksList(id.Value));
        }
    }
}