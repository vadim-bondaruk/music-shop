namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;
    using Exceptions;
    using Common.ViewModels;


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
            using (var cartRepository = Factory.GetCartRepository())
            {
                var cart = cartRepository.GetByUserId(userId);
                if (cart == null)
                {
                    cart = new Cart(userId);
                    cartRepository.AddOrUpdate(cart);
                }

                using (var trackRepository = Factory.GetTrackRepository())
                {
                    var track = trackRepository.GetById(trackId);
                    if (track == null || trackId == 0)
                    {
                        throw new InvalidTrackIdException($"Трек с ID={trackId} не найден.");
                    }

                    if (cart.Tracks == null)
                    {
                        cart.Tracks = new List<OrderTrack>();
                    }

                    var orderTrack = new OrderTrack() {CartId = cart.Id, Track = track, TrackId = trackId};
                    cart.Tracks.Add(orderTrack);

                    if (track.OrderTracks == null)
                    {
                        track.OrderTracks = new List<OrderTrack>();
                    }

                    track.OrderTracks.Add(orderTrack);
                    trackRepository.AddOrUpdate(track);
                }

                cartRepository.AddOrUpdate(cart);
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
            using (var cartRepository = Factory.GetCartRepository())
            {
                var cart = cartRepository.GetByUserId(userId);
                if (cart == null)
                {
                    return;
                }

                using (var trackRepository = Factory.GetTrackRepository())
                {
                    var track = trackRepository.GetById(trackId);
                    if (track == null || trackId == 0)
                    {
                        throw new InvalidTrackIdException($"Трек с ID={trackId} не найден.");
                    }

                    if (cart.Albums != null)
                    {
                        var orderTrack = track.OrderTracks.FirstOrDefault(c => c.CartId == cart.Id);
                        while (orderTrack != null)
                        {
                            track.OrderTracks.Remove(orderTrack);
                            cart.Tracks.Remove(orderTrack);
                            orderTrack = track.OrderTracks.FirstOrDefault(c => c.CartId == cart.Id);
                        }
                    }

                    trackRepository.AddOrUpdate(track);
                }

                cartRepository.AddOrUpdate(cart);
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
            using (var cartRepository = Factory.GetCartRepository())
            {
                var cart = cartRepository.GetByUserId(userId);
                if (cart == null)
                {
                    cart = new Cart(userId);
                    cartRepository.AddOrUpdate(cart);
                }

                using (var albumRepository = Factory.GetAlbumRepository())
                {
                    var album = albumRepository.GetById(albumId);
                    if (album == null || albumId == 0)
                    {
                        throw new InvalidAlbumIdException($"Альбом с ID={albumId} не найден.");
                    }

                    if (cart.Albums == null)
                    {
                        cart.Albums = new List<OrderAlbum>();
                    }

                    var orderAlbum = new OrderAlbum() { CartId = cart.Id, Album = album, AlbumId = albumId };
                    cart.Albums.Add(orderAlbum);
                    
                    if (album.OrderAlbums == null)
                    {
                        album.OrderAlbums = new List<OrderAlbum>();
                    }

                    album.OrderAlbums.Add(orderAlbum);
                    albumRepository.AddOrUpdate(album);
                }
                
                cartRepository.AddOrUpdate(cart);
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
            using (var cartRepository = Factory.GetCartRepository())
            {
                var cart = cartRepository.GetByUserId(userId);
                if (cart == null)
                {
                    return;
                }

                using (var albumRepository = Factory.GetAlbumRepository())
                {
                    var album = albumRepository.GetById(albumId);
                    if (album == null || albumId == 0)
                    {
                        throw new InvalidAlbumIdException($"Альбом с ID={albumId} не найден.");
                    }

                    if (cart.Albums != null)
                    {
                        var orderAlbum = album.OrderAlbums.FirstOrDefault(c => c.CartId == cart.Id);
                        while (orderAlbum != null)
                        {
                            album.OrderAlbums.Remove(orderAlbum);
                            cart.Albums.Remove(orderAlbum);
                            orderAlbum = album.OrderAlbums.FirstOrDefault(c => c.CartId == cart.Id);
                        }
                    }

                    albumRepository.AddOrUpdate(album);
                }

                cartRepository.AddOrUpdate(cart);
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
            var returnResult = new Stack<int>();
            using (var cartRepository = Factory.GetCartRepository())
            {
                var cart = cartRepository.GetByUserId(userId);
                foreach (var orderTrack in cart.Tracks)
                {
                    returnResult.Push(orderTrack.TrackId);
                }
            }
            return returnResult.ToArray();
        }

        /// <summary>
        /// Get collection of chosen to purchase Tracks
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <returns>Returns List of Tracks</returns>
        public ICollection<TrackDetailsViewModel> GetOrderTracks(int userId, int? currencyCode = null)
        {
            var returnResult = new List<Track>();
            var resultViewTracks = new List<TrackDetailsViewModel>();
            /// Вытягиваем Cart из базы
            using (var cartRepository = Factory.GetCartRepository())
            {
                var cart = cartRepository.GetByUserId(userId);
                /// Вытягиваем Tracks из базы
                using (var trackRepository = Factory.GetTrackRepository())
                {
                    returnResult.AddRange(cart.Tracks.Select(orderTrack => trackRepository.GetById(orderTrack.TrackId)));
                    /// Конвертируем Tracks in TracksDetailsViewModel
                    var trackService = new TrackService(Factory);
                    foreach(Track anyTrack in returnResult)
                    {
                        // TODO: Сделать передачу валюты пользователя в метод GetTrackDetails()
                        resultViewTracks.Add(trackService.GetTrackDetails(anyTrack.Id, currencyCode));
                    }
                }
            }
            return resultViewTracks;
        }

        /// <summary>
        /// Get chosen to purchase Album's IDs
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <returns>Returns Array of IDs</returns>
        public IEnumerable<int> GetOrderAlbumsIds(int userId)
        {
            var returnResult = new Stack<int>();
            using (var cartRepository = Factory.GetCartRepository())
            {
                var cart = cartRepository.GetByUserId(userId);
                foreach (var orderAlbum in cart.Albums)
                {
                    returnResult.Push(orderAlbum.AlbumId);
                }
            }
            return returnResult.ToArray();
        }

        /// <summary>
        /// Get collection of chosen to purchase Albums
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <returns>Returns List of Albums</returns>
        public ICollection<AlbumDetailsViewModel> GetOrderAlbums(int userId, int? currencyCode = null)
        {
            var returnResult = new List<Album>();
            var resultViewAlbums = new List<AlbumDetailsViewModel>();
            /// Вытягиваем Cart из базы
            using (var cartRepository = Factory.GetCartRepository())
            {
                var cart = cartRepository.GetByUserId(userId);
                /// Вытягиваем Albums из базы
                using (var albumsRepository = Factory.GetAlbumRepository())
                {
                    returnResult.AddRange(cart.Albums.Select(o => albumsRepository.GetById(o.AlbumId)));
                    /// Конвертируем Album in AlbumDetailsViewModel
                    var albumService = new AlbumService(Factory);
                    foreach (Album anyAlbum in returnResult)
                    {
                        // TODO: Сделать передачу валюты пользователя в метод GetAlbumDetails()
                        resultViewAlbums.Add(albumService.GetAlbumDetails(anyAlbum.Id, currencyCode));
                    }
                }
            }
            return resultViewAlbums;
        }

        /// <summary>
        /// Accept Payment of All User's Cart
        /// </summary>
        /// <param name="userId">User's ID</param>
        public void AcceptPayment(int userId)
        {
            var trackIds = this.GetOrderTracksIds(userId);
            if (trackIds != null && trackIds.Any())
            {
                foreach (var trackId in trackIds)
                {
                    this.AcceptPaymentForTrack(userId, trackId);
                }
            }

            var albumIds = this.GetOrderAlbumsIds(userId);
            if (albumIds != null && albumIds.Any())
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
        public void AcceptPayment(int userId, IEnumerable<int> ids, bool isTracks)
        {
            if (ids == null || !ids.Any()) return;
            if (isTracks)
            {
                foreach (var trackId in ids)
                {
                    this.AcceptPaymentForTrack(userId, trackId);
                }
            }
            else
            {
                foreach (var albumId in ids)
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
            using (var trackRepository = Factory.GetTrackRepository())
            {
                var track = trackRepository.GetById(trackId);
                this.RemoveTrack(userId, trackId);
                using (var userDataRepository = Factory.GetUserDataRepository())
                {
                    UserData user = userDataRepository.GetAll(u => u.UserId == userId).FirstOrDefault();
                    if (user == null)
                    {
                        throw new InvalidUserIdException($"Пользователь с ID={userId} не найден");
                    }

                    var purchasedTrack = new PurchasedTrack()
                    {
                        UserId = userId, User = user, TrackId = trackId, Track = track
                    };
                    if (track.PurchasedTracks == null)
                    {
                        track.PurchasedTracks = new List<PurchasedTrack>();
                    }

                    track.PurchasedTracks.Add(purchasedTrack);
                    if (user.PurchasedTracks == null)
                    {
                        user.PurchasedTracks = new List<PurchasedTrack>();
                    }

                    user.PurchasedTracks.Add(purchasedTrack);
                    userDataRepository.AddOrUpdate(user);
                }
                trackRepository.AddOrUpdate(track);
            }
        }

        /// <summary>
        /// Accept Album's Payment
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="albumId">Album ID</param>
        private void AcceptPaymentForAlbum(int userId, int albumId)
        {
            using (var albumRepository = Factory.GetAlbumRepository())
            {
                var album = albumRepository.GetById(albumId);
                this.RemoveAlbum(userId, albumId);
                using (var userDataRepository = Factory.GetUserDataRepository())
                {
                    UserData user = userDataRepository.GetAll(u => u.UserId == userId).FirstOrDefault();
                    if (user == null)
                    {
                        throw new InvalidUserIdException($"Пользователь с ID={userId} не найден");
                    }

                    var purchasedAlbum = new PurchasedAlbum()
                    {
                        UserId = userId, User = user, AlbumId = albumId, Album = album
                    };
                    if (album.PurchasedAlbums == null)
                    {
                        album.PurchasedAlbums = new List<PurchasedAlbum>();
                    }

                    album.PurchasedAlbums.Add(purchasedAlbum);
                    if (user.PurchasedAlbums == null)
                    {
                        user.PurchasedAlbums = new List<PurchasedAlbum>();
                    }

                    user.PurchasedAlbums.Add(purchasedAlbum);
                    userDataRepository.AddOrUpdate(user);
                }
                albumRepository.AddOrUpdate(album);
            }
        }
    }
}
