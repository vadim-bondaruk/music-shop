namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Shop.BLL.Helpers;
    using Shop.BLL.Services.Infrastructure;
    using Shop.Common.Models;
    using Shop.Common.ViewModels;
    using Shop.DAL.Infrastruture;

    /// <summary>
    ///     The artist service.
    /// </summary>
    public class ArtistService : BaseService, IArtistService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArtistService" /> class.
        /// </summary>
        /// <param name="factory">
        ///     The repository factory.
        /// </param>
        public ArtistService(IRepositoryFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        ///     Returns all registered albums for the specified artist using the specified currency and price level for album
        ///     price.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <param name="currencyCode">
        ///     The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All registered albums with price for the specified artist.
        /// </returns>
        public ArtistAlbumsListViewModel GetAlbumsList(int artistId, int? currencyCode = null, int? priceLevel = null)
        {
            var artistAlbumsListViewModel = this.CreateArtistAlbumsListViewModel(artistId);

            ICollection<Album> albums;
            using (var repository = this.Factory.GetAlbumRepository())
            {
                albums = repository.GetAll(a => a.ArtistId != null && a.ArtistId.Value == artistId, a => a.Artist);
            }

            artistAlbumsListViewModel.Albums = ServiceHelper.ConvertToAlbumViewModels(
                this.Factory,
                albums,
                currencyCode,
                priceLevel);
            return artistAlbumsListViewModel;
        }

        /// <summary>
        ///     Returns all albums for the specified artist without price.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <returns>
        ///     All albums for the specified artist without price.
        /// </returns>
        public ArtistAlbumsListViewModel GetAlbumsWithoutPrice(int artistId)
        {
            var artistAlbumsListViewModel = this.CreateArtistAlbumsListViewModel(artistId);

            ICollection<Album> albums;
            using (var repository = this.Factory.GetAlbumRepository())
            {
                albums =
                    repository.GetAll(a => a.ArtistId != null && a.ArtistId.Value == artistId && !a.AlbumPrices.Any());
            }

            artistAlbumsListViewModel.Albums = albums.Select(ModelsMapper.GetAlbumViewModel).ToList();
            return artistAlbumsListViewModel;
        }

        /// <summary>
        ///     Returns all albums for the specified artist with price specified using the specified currency and price level for
        ///     album price.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <param name="currencyCode">
        ///     The currency code for album price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for album price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All albums for the specified artist without price specified.
        /// </returns>
        public ArtistAlbumsListViewModel GetAlbumsWithPrice(
            int artistId,
            int? currencyCode = null,
            int? priceLevel = null)
        {
            var artistAlbumsListViewModel = this.CreateArtistAlbumsListViewModel(artistId);

            ICollection<Album> albums;
            using (var repository = this.Factory.GetAlbumRepository())
            {
                albums =
                    repository.GetAll(a => a.ArtistId != null && a.ArtistId.Value == artistId && !a.AlbumPrices.Any());
            }

            artistAlbumsListViewModel.Albums = ServiceHelper.ConvertToAlbumViewModels(
                this.Factory,
                albums,
                currencyCode,
                priceLevel);
            return artistAlbumsListViewModel;
        }

        /// <summary>
        ///     Returns the artist details.
        /// </summary>
        /// <param name="id">The artist id.</param>
        /// <returns>
        ///     The information about artist with the specified <paramref name="id" /> or <b>null</b> if artist doesn't exist.
        /// </returns>
        public ArtistDetailsViewModel GetArtistDetails(int id)
        {
            Artist artist;
            using (var repository = this.Factory.GetArtistRepository())
            {
                artist = repository.GetById(id);
            }

            var artistDetails = ModelsMapper.GetArtistDetailsViewModel(artist);

            using (var repository = this.Factory.GetTrackRepository())
            {
                artistDetails.TracksCount = repository.Count(t => t.ArtistId == artist.Id);
            }

            using (var repository = this.Factory.GetAlbumRepository())
            {
                artistDetails.AlbumsCount = repository.Count(a => a.ArtistId != null && a.ArtistId == artist.Id);
            }

            return artistDetails;
        }

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// </returns>
        public ArtistDetailsViewModel GetArtistDetailsViewModel(int id)
        {
            using (var repository = this.Factory.GetArtistRepository())
            {
                var artist = repository.GetById(id);
                return ModelsMapper.GetArtistDetailsViewModel(artist);
            }
        }

        /// <summary>
        ///     Returns all registered artists.
        /// </summary>
        /// <returns>
        ///     All registered artists.
        /// </returns>
        public ICollection<ArtistViewModel> GetArtistsList()
        {
            ICollection<Artist> artists;
            using (var repository = this.Factory.GetArtistRepository())
            {
                artists = repository.GetAll();
            }

            var artistViewModels = new List<ArtistViewModel>();
            using (var repository = this.Factory.GetTrackRepository())
            {
                foreach (var artist in artists)
                {
                    var artistViewModel = ModelsMapper.GetArtistViewModel(artist);
                    if (artistViewModel != null)
                    {
                        artistViewModel.TracksCount = repository.Count(t => t.ArtistId == artist.Id);
                        artistViewModels.Add(artistViewModel);
                    }
                }
            }

            using (var repository = this.Factory.GetAlbumRepository())
            {
                foreach (var artistViewModel in artistViewModels)
                {
                    artistViewModel.AlbumsCount =
                        repository.Count(a => a.ArtistId != null && a.ArtistId == artistViewModel.Id);
                }
            }

            return artistViewModels;
        }

        /// <summary>
        ///     Returns all registered tracks for the specified artist using the specified currency and price level for track
        ///     price.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <param name="currencyCode">
        ///     The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All registered tracks with price for the specified artist.
        /// </returns>
        public ArtistTracksListViewModel GetTracksList(int artistId, int? currencyCode = null, int? priceLevel = null)
        {
            var artistTracksListViewModel = this.CreateArtistTracksListViewModel(artistId);

            ICollection<Track> tracks;
            using (var repository = this.Factory.GetTrackRepository())
            {
                tracks = repository.GetAll(t => t.ArtistId == artistId, t => t.Artist);
            }

            artistTracksListViewModel.Tracks = ServiceHelper.ConvertToTrackViewModels(
                this.Factory,
                tracks,
                currencyCode,
                priceLevel);
            return artistTracksListViewModel;
        }

        /// <summary>
        ///     Returns all tracks for the specified artist without price.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <returns>
        ///     All tracks for the specified artist without price.
        /// </returns>
        public ArtistTracksListViewModel GetTracksWithoutPrice(int artistId)
        {
            var artistTracksListViewModel = this.CreateArtistTracksListViewModel(artistId);

            ICollection<Track> tracks;
            using (var repository = this.Factory.GetTrackRepository())
            {
                tracks = repository.GetAll(t => t.ArtistId == artistId && !t.TrackPrices.Any());
            }

            artistTracksListViewModel.Tracks = tracks.Select(ModelsMapper.GetTrackViewModel).ToList();
            return artistTracksListViewModel;
        }

        /// <summary>
        ///     Returns all tracks for the specified artist with price specified using the specified currency and price level for
        ///     track price.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <param name="currencyCode">
        ///     The currency code for track price. If it doesn't specified than default currency is used.
        /// </param>
        /// <param name="priceLevel">
        ///     The price level for track price. If it doesn't specified than default price level is used.
        /// </param>
        /// <returns>
        ///     All tracks for the specified artist without price specified.
        /// </returns>
        public ArtistTracksListViewModel GetTracksWithPrice(
            int artistId,
            int? currencyCode = null,
            int? priceLevel = null)
        {
            var artistTracksListViewModel = this.CreateArtistTracksListViewModel(artistId);

            ICollection<Track> tracks;
            using (var repository = this.Factory.GetTrackRepository())
            {
                tracks = repository.GetAll(t => t.ArtistId == artistId && t.TrackPrices.Any());
            }

            artistTracksListViewModel.Tracks = ServiceHelper.ConvertToTrackViewModels(
                this.Factory,
                tracks,
                currencyCode,
                priceLevel);
            return artistTracksListViewModel;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ArtistAlbumsListViewModel" /> type.
        /// </summary>
        /// <param name="artistId">
        ///     The artist id.
        /// </param>
        /// <returns>
        ///     A new instance of the <see cref="ArtistAlbumsListViewModel" /> type.
        /// </returns>
        private ArtistAlbumsListViewModel CreateArtistAlbumsListViewModel(int artistId)
        {
            Artist artist;
            using (var repository = this.Factory.GetArtistRepository())
            {
                artist = repository.GetById(artistId);
            }

            return ModelsMapper.GetArtistAlbumsListViewModel(artist);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ArtistTracksListViewModel" /> type.
        /// </summary>
        /// <param name="artistId">
        ///     The artist id.
        /// </param>
        /// <returns>
        ///     A new instance of the <see cref="ArtistTracksListViewModel" /> type.
        /// </returns>
        private ArtistTracksListViewModel CreateArtistTracksListViewModel(int artistId)
        {
            Artist artist;
            using (var repository = this.Factory.GetArtistRepository())
            {
                artist = repository.GetById(artistId);
            }

            return ModelsMapper.GetArtistTracksListViewModel(artist);
        }
    }
}