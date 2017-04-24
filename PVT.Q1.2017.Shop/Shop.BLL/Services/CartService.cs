namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;
    using Exceptions;
    using Common.ViewModels;
    using System.Collections;


    /// <summary>
    /// The Cart Service
    /// </summary>
    public class CartService : BaseService, ICartService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="factory">The  repositories factory</param>
        public CartService(IRepositoryFactory factory) : base(factory)
        {
        }

        /// <summary> 
        /// Add track to User's Cart 
        /// </summary> 
        /// <param name="userId">User's ID</param> 
        /// <param name="trackId">Added Track ID</param> 
        public void AddTrack(int userId, int trackId)
        {
            var cart = GetCartByUserId(userId);
            var track = GetTrackById(trackId);
            using (var orderTrackRepository = Factory.GetOrderTrackRepository())
            {
                if (orderTrackRepository.Exist(o => o.CartId == cart.Id && o.TrackId == track.Id))
                {
                    return;
                }

                var orderTrack = new OrderTrack() { CartId = cart.Id, TrackId = track.Id };
                orderTrackRepository.AddOrUpdate(orderTrack);
                orderTrackRepository.SaveChanges();
            }
        }

        /// <summary> 
        /// Add track list to User's Cart 
        /// </summary> 
        /// <param name="userId">User's ID</param> 
        /// <param name="trackIds">Added Tracks IDs</param> 
        public void AddTrack(int userId, IEnumerable<int> trackIds)
        {
            var cart = this.GetCartByUserId(userId);
            IEnumerable<Track> tracks;
            using (var trackRepository = Factory.GetTrackRepository())
            {
                tracks = trackRepository.GetAll(t => trackIds.Contains(t.Id));
            }

            using (var orderTrackRepository = Factory.GetOrderTrackRepository())
            {
                var orderTracks = orderTrackRepository.GetAll(o => o.CartId == cart.Id).Select(o => o.TrackId);
                tracks = tracks.Where(t => !orderTracks.Contains(t.Id));
                foreach (var track in tracks)
                {
                    var orderTrack = new OrderTrack() {CartId = cart.Id, TrackId = track.Id};
                    orderTrackRepository.AddOrUpdate(orderTrack);
                }
                orderTrackRepository.SaveChanges();
            }
        }

        /// <summary> 
        /// Remove track from User's Cart 
        /// </summary> 
        /// <param name="userId">User's ID</param> 
        /// <param name="trackId">Removed Track ID</param> 
        public void RemoveTrack(int userId, int trackId)
        {
            var cart = GetCartByUserId(userId);
            using (var orderTrackRepository = Factory.GetOrderTrackRepository())
            {
                var orderTrack = orderTrackRepository.FirstOrDefault(o =>
                    o.CartId == cart.Id
                    && o.TrackId == trackId);
                if (orderTrack == null)
                {
                    return;
                }

                orderTrackRepository.Delete(orderTrack);
                orderTrackRepository.SaveChanges();
            }
        }

        /// <summary> 
        /// Remove track list from User's Cart 
        /// </summary> 
        /// <param name="userId">User's ID</param> 
        /// <param name="trackIds">Removed Tracks IDs</param> 
        public void RemoveTrack(int userId, IEnumerable<int> trackIds)
        {
            var cart = GetCartByUserId(userId);
            using (var orderTrackRepository = Factory.GetOrderTrackRepository())
            {
                var orderTracks = new List<OrderTrack>();
                orderTracks.AddRange(orderTrackRepository.GetAll(o => o.CartId == cart.Id && trackIds.Contains(o.TrackId)));
                foreach (var orderTrack in orderTracks)
                {
                    orderTrackRepository.Delete(orderTrack);
                }

                orderTrackRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Add album to User's Cart
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <param name="albumId">Added Album ID</param>
        public void AddAlbum(int userId, int albumId)
        {
            var cart = GetCartByUserId(userId);
            var album = GetAlbumById(albumId);
            using (var orderAlbumRepository = Factory.GetOrderAlbumRepository())
            {
                if (orderAlbumRepository.Exist(o => o.CartId == cart.Id && o.AlbumId == album.Id))
                {
                    return;
                }

                var orderAlbum = new OrderAlbum() { CartId = cart.Id, AlbumId = album.Id };
                orderAlbumRepository.AddOrUpdate(orderAlbum);
                orderAlbumRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Add albums to User's Cart
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <param name="albumIds">Added Albums IDs</param>
        public void AddAlbum(int userId, IEnumerable<int> albumIds)
        {
            var cart = this.GetCartByUserId(userId);
            IEnumerable<Album> albums;
            using (var albumRepository = Factory.GetAlbumRepository())
            {
                albums = albumRepository.GetAll(a => albumIds.Contains(a.Id));
            }

            using (var orderAlbumRepository = Factory.GetOrderAlbumRepository())
            {
                var orderAlbums = orderAlbumRepository.GetAll(o => o.CartId == cart.Id).Select(o => o.AlbumId);
                albums = albums.Where(a => !orderAlbums.Contains(a.Id));
                foreach (var album in albums)
                {
                    var orderAlbum = new OrderAlbum() { CartId = cart.Id, AlbumId = album.Id };
                    orderAlbumRepository.AddOrUpdate(orderAlbum);
                }
                orderAlbumRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Remove album from User's Cart
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <param name="albumId">Removed Album ID</param>
        public void RemoveAlbum(int userId, int albumId)
        {
            var cart = GetCartByUserId(userId);
            using (var orderAlbumRepository = Factory.GetOrderAlbumRepository())
            {
                var orderAlbum = orderAlbumRepository.FirstOrDefault(o =>
                    o.CartId == cart.Id
                    && o.AlbumId == albumId);
                if (orderAlbum == null)
                {
                    return;
                }

                orderAlbumRepository.Delete(orderAlbum);
                orderAlbumRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Remove album list from User's Cart
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <param name="albumIds">Removed Albums IDs</param>
        public void RemoveAlbum(int userId, IEnumerable<int> albumIds)
        {
            var cart = GetCartByUserId(userId);
            using (var orderAlbumRepository = Factory.GetOrderAlbumRepository())
            {
                var orderAlbums = new List<OrderAlbum>();
                orderAlbums.AddRange(orderAlbumRepository.GetAll(o => o.CartId == cart.Id && albumIds.Contains(o.AlbumId)));
                foreach (var orderAlbum in orderAlbums)
                {
                    orderAlbumRepository.Delete(orderAlbum);
                }

                orderAlbumRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Get chosen to purchase Track's IDs
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <returns>Returns Array of IDs</returns>
        public IEnumerable<int> GetOrderTracksIds(int userId)
        {
            var returnResult = new List<int>();
            Cart cart;
            using (var cartRepository = Factory.GetCartRepository())
            {
                cart = cartRepository.GetByUserId(userId);
                if (cart == null)
                {
                    return returnResult;
                }
            }

            using (var orderTrackRepository = Factory.GetOrderTrackRepository())
            {
                returnResult.AddRange(orderTrackRepository.GetAll(o => o.CartId == cart.Id).Select(o => o.TrackId));
            }

            return returnResult;
        }

        /// <summary>
        /// Get collection of chosen to purchase Tracks
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <returns>Returns List of Tracks</returns>
        public ICollection<TrackDetailsViewModel> GetOrderTracks(int userId, int? currencyCode = null)
        {
            var result = new List<TrackDetailsViewModel>();
            Cart cart;
            using (var cartRepository = Factory.GetCartRepository())
            {
                cart = cartRepository.GetByUserId(userId);
                if (cart == null)
                {
                    return result;
                }
            }

            using (var trackRepository = Factory.GetTrackRepository())
            {
                var trackService = new TrackService(Factory);
                using (var orderTrackRepository = Factory.GetOrderTrackRepository())
                {
                    var tracksIds = orderTrackRepository.GetAll(o => o.CartId == cart.Id).Select(o => o.TrackId);
                    result =
                        trackRepository.GetAll(t => tracksIds.Contains(t.Id))
                            .Select(t => trackService.GetTrackDetails(t.Id, currencyCode)).ToList();
                }
            }
            
            return result;
        }

        /// <summary>
        /// Get chosen to purchase Album's IDs
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <returns>Returns Array of IDs</returns>
        public IEnumerable<int> GetOrderAlbumsIds(int userId)
        {
            var returnResult = new List<int>();
            Cart cart;
            using (var cartRepository = Factory.GetCartRepository())
            {
                cart = cartRepository.GetByUserId(userId);
                if (cart == null)
                {
                    return returnResult;
                }
            }

            using (var orderAlbumRepository = Factory.GetOrderAlbumRepository())
            {
                returnResult.AddRange(orderAlbumRepository.GetAll(o => o.CartId == cart.Id).Select(o => o.AlbumId));
            }

            return returnResult;
        }

        /// <summary>
        /// Get collection of chosen to purchase Albums
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <returns>Returns List of Albums</returns>
        public ICollection<AlbumDetailsViewModel> GetOrderAlbums(int userId, int? currencyCode = null)
        {
            var result = new List<AlbumDetailsViewModel>();
            Cart cart;
            using (var cartRepository = Factory.GetCartRepository())
            {
                cart = cartRepository.GetByUserId(userId);
                if (cart == null)
                {
                    return result;
                }
            }
            
            using (var albumRepository = Factory.GetAlbumRepository())
            {
                var albumService = new AlbumService(Factory);
                using (var orderAlbumRepository = Factory.GetOrderAlbumRepository())
                {
                    var albumsIds = orderAlbumRepository.GetAll(o => o.CartId == cart.Id).Select(o => o.AlbumId);
                    result =
                        albumRepository.GetAll(a => albumsIds.Contains(a.Id))
                            .Select(a => albumService.GetAlbumDetails(a.Id, currencyCode)).ToList();
                }
            }

            return result;
        }

        /// <summary>
        /// Remove All items from User's Cart
        /// </summary>
        /// <param name="userId">User ID</param>
        public void RemoveAll(int userId)
        {
            var cart = this.GetCartByUserId(userId);
            using (var orderTrackRepository = Factory.GetOrderTrackRepository())
            {
                var orderTracks = new List<OrderTrack>();
                orderTracks.AddRange(orderTrackRepository.GetAll(o => o.CartId == cart.Id));
                foreach (var orderTrack in orderTracks)
                {
                    orderTrackRepository.Delete(orderTrack);
                }

                orderTrackRepository.SaveChanges();
            }

            using (var orderAlbumRepository = Factory.GetOrderAlbumRepository())
            {
                var orderAlbums = new List<OrderAlbum>();
                orderAlbums.AddRange(orderAlbumRepository.GetAll(o => o.CartId == cart.Id));
                foreach (var orderAlbum in orderAlbums)
                {
                    orderAlbumRepository.Delete(orderAlbum);
                }

                orderAlbumRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Accept Payment of All User's Cart
        /// </summary>
        /// <param name="userId">User's ID</param>
        public void AcceptPayment(int userId)
        {
            this.RemoveAll(userId);
        }

        /// <summary>
        /// Get Cart by User's ID
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>User's Cart</returns>
        private Cart GetCartByUserId(int userId)
        {
            Cart cart;
            using (var cartRepository = Factory.GetCartRepository())
            {
                cart = cartRepository.GetByUserId(userId);
                if (cart == null)
                {
                    cart = new Cart(userId);
                    cartRepository.AddOrUpdate(cart);
                    cartRepository.SaveChanges();
                }
            }

            return cart;
        }

        /// <summary>
        /// Get Track by ID
        /// </summary>
        /// <param name="trackId">Track ID</param>
        /// <returns>Track</returns>
        private Track GetTrackById(int trackId)
        {
            Track track;
            using (var trackRepository = Factory.GetTrackRepository())
            {
                track = trackRepository.GetById(trackId);
                if (track == null || trackId == 0)
                {
                    throw new InvalidTrackIdException($"Трек с ID={trackId} не найден.");
                }
            }

            return track;
        }

        /// <summary>
        /// Get Album by ID
        /// </summary>
        /// <param name="albumId">Album ID</param>
        /// <returns>Album</returns>
        private Album GetAlbumById(int albumId)
        {
            Album album;
            using (var albumRepository = Factory.GetAlbumRepository())
            {
                album = albumRepository.GetById(albumId);
                if (album == null || albumId == 0)
                {
                    throw new InvalidAlbumIdException($"Альбом с ID={albumId} не найден.");
                }
            }

            return album;
        }
    }
}
