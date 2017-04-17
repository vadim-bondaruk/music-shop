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
            var cart = this.GetCartByUserId(userId);
            var track = this.GetTrackById(trackId);
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
            foreach (var trackId in trackIds)
            {
                this.AddTrack(userId, trackId);
            }
        }

        /// <summary> 
        /// Remove track from User's Cart 
        /// </summary> 
        /// <param name="userId">User's ID</param> 
        /// <param name="trackId">Removed Track ID</param> 
        public void RemoveTrack(int userId, int trackId)
        {
            var cart = this.GetCartByUserId(userId);
            var track = this.GetTrackById(trackId);
            using (var orderTrackRepository = Factory.GetOrderTrackRepository())
            {
                var orderTrack = orderTrackRepository.FirstOrDefault(o =>
                    o.CartId == cart.Id
                    && o.TrackId == track.Id);
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
            foreach (var trackId in trackIds)
            {
                this.RemoveTrack(userId, trackId);
            }
        }

        /// <summary>
        /// Add album to User's Cart
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <param name="albumId">Added Album ID</param>
        public void AddAlbum(int userId, int albumId)
        {
            var cart = this.GetCartByUserId(userId);
            var album = this.GetAlbumById(albumId);
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
            foreach (var albumId in albumIds)
            {
                this.AddAlbum(userId, albumId);
            }
        }

        /// <summary>
        /// Remove album from User's Cart
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <param name="albumId">Removed Album ID</param>
        public void RemoveAlbum(int userId, int albumId)
        {
            var cart = this.GetCartByUserId(userId);
            var album = this.GetAlbumById(albumId);
            using (var orderAlbumRepository = Factory.GetOrderAlbumRepository())
            {
                var orderAlbum = orderAlbumRepository.FirstOrDefault(o =>
                    o.CartId == cart.Id
                    && o.AlbumId == album.Id);
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
            foreach (var albumId in albumIds)
            {
                this.RemoveAlbum(userId, albumId);
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
            /// Вытягиваем Cart из базы
            Cart cart;
            using (var cartRepository = Factory.GetCartRepository())
            {
                cart = cartRepository.GetByUserId(userId);
                if (cart == null)
                {
                    return result;
                }
            }

            var tracks = new List<Track>();
            /// Вытягиваем Tracks из базы
            using (var trackRepository = Factory.GetTrackRepository())
            {
                var tracksIds = this.GetOrderTracksIds(userId);
                tracks.AddRange(tracksIds.Select(trackId => trackRepository.GetById(trackId)));
                /// Конвертируем Tracks in TracksDetailsViewModel
                var trackService = new TrackService(Factory);
                // TODO: Сделать передачу валюты пользователя в метод GetTrackDetails()
                result.AddRange(tracks.Select(anyTrack => trackService.GetTrackDetails(anyTrack.Id, currencyCode)));
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
            /// Вытягиваем Cart из базы
            Cart cart;
            using (var cartRepository = Factory.GetCartRepository())
            {
                cart = cartRepository.GetByUserId(userId);
                if (cart == null)
                {
                    return result;
                }
            }
            /// Вытягиваем Albums из базы
            var albums = new List<Album>();
            using (var albumRepository = Factory.GetAlbumRepository())
            {
                var albumsIds = this.GetOrderAlbumsIds(userId);
                albums.AddRange(albumsIds.Select(albumId => albumRepository.GetById(albumId)));
                /// Конвертируем Album in AlbumDetailsViewModel
                var albumService = new AlbumService(Factory);
                // TODO: Сделать передачу валюты пользователя в метод GetAlbumDetails()
                result.AddRange(albums.Select(anyAlbum => albumService.GetAlbumDetails(anyAlbum.Id, currencyCode)));
            }
            
            return result;
        }

        /// <summary>
        /// Remove All items from User's Cart
        /// </summary>
        /// <param name="userId">User ID</param>
        public void RemoveAll(int userId)
        {
            // Remove all tracks
            var trackIds = GetOrderTracksIds(userId);
            this.RemoveTrack(userId, trackIds);

            // Remove all albums

            var albumIds = GetOrderAlbumsIds(userId);
            this.RemoveAlbum(userId, albumIds);
        }

        /// <summary>
        /// Accept Payment of All User's Cart
        /// </summary>
        /// <param name="userId">User's ID</param>
        public void AcceptPayment(int userId)
        {
            var trackIds = this.GetOrderTracksIds(userId).ToArray();
            if (trackIds.Length > 0)
            {
                foreach (var trackId in trackIds)
                {
                    this.AcceptPaymentForTrack(userId, trackId);
                }
            }

            var albumIds = this.GetOrderAlbumsIds(userId).ToArray();
            if (albumIds.Length > 0)
            {
                foreach (var albumId in albumIds)
                {
                    this.AcceptPaymentForAlbum(userId, albumId);
                }
            }
        }

        /// <summary>
        /// Accept Payment of selected items
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="ids">IDs of payment items</param>
        /// <param name="isTracks">If paid items is tracks, then True
        /// If paid items is albums, then False</param>
        public void AcceptPayment(int userId, IEnumerable<int> ids, bool isTracks = true)
        {
            var idArray = ids.ToArray();
            if (idArray.Length == 0) return;
            if (isTracks)
            {
                foreach (var trackId in idArray)
                {
                    this.AcceptPaymentForTrack(userId, trackId);
                }
            }
            else
            {
                foreach (var albumId in idArray)
                {
                    this.AcceptPaymentForAlbum(userId, albumId);
                }
            }
        }

        /// <summary>
        /// Accept Track's Payment
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="trackId">Track ID</param>
        private void AcceptPaymentForTrack(int userId, int trackId)
        {
            UserData user;
            using (var userDataRepository = Factory.GetUserDataRepository())
            {
                user = userDataRepository.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    throw new InvalidUserIdException($"Пользователь с ID={userId} не найден");
                }
            }

            using (var purchasedTrackRepository = Factory.GetPurchasedTrackRepository())
            {
                var purchasedTrack = purchasedTrackRepository.FirstOrDefault(p =>
                    p.UserId == userId
                    && p.TrackId == trackId);
                if (purchasedTrack != null)
                {
                    throw new InvalidPaymentOperation(
                        $"Трек ID={trackId} пользователем ID={userId} уже был куплен ранее. Необходимо вернуть деньги!");
                }

                this.RemoveTrack(userId, trackId);
                purchasedTrack = new PurchasedTrack() { UserId = userId, TrackId = trackId };
                purchasedTrackRepository.AddOrUpdate(purchasedTrack);
                purchasedTrackRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Accept Album's Payment
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="albumId">Album ID</param>
        private void AcceptPaymentForAlbum(int userId, int albumId)
        {
            UserData user;
            using (var userDataRepository = Factory.GetUserDataRepository())
            {
                user = userDataRepository.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    throw new InvalidUserIdException($"Пользователь с ID={userId} не найден");
                }
            }

            using (var purchasedAlbumRepository = Factory.GetPurchasedAlbumRepository())
            {
                var purchasedAlbum = purchasedAlbumRepository.FirstOrDefault(p =>
                    p.UserId == userId
                    && p.AlbumId == albumId);
                if (purchasedAlbum != null)
                {
                    throw new InvalidPaymentOperation(
                        $"Альбом ID={albumId} пользователем ID={userId} уже был куплен ранее. Необходимо вернуть деньги!");
                }

                this.RemoveAlbum(userId, albumId);
                purchasedAlbum = new PurchasedAlbum() { UserId = userId, AlbumId = albumId };
                purchasedAlbumRepository.AddOrUpdate(purchasedAlbum);
                purchasedAlbumRepository.SaveChanges();
            }
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
