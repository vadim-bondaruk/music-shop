namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;
    using Exceptions;


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
                    cart = new Cart { UserId = userId, Tracks = new List<OrderTrack>() };
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

                    if (track.Carts == null)
                    {
                        track.Carts = new List<OrderTrack>();
                    }

                    track.Carts.Add(orderTrack);
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
                if (cart == null || trackId == 0)
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
                        var orderTrack = track.Carts.FirstOrDefault(c => c.CartId == cart.Id);
                        while (orderTrack != null)
                        {
                            track.Carts.Remove(orderTrack);
                            cart.Tracks.Remove(orderTrack);
                            orderTrack = track.Carts.FirstOrDefault(c => c.CartId == cart.Id);
                        }
                    }
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
                    cart = new Cart { UserId = userId, Albums = new List<OrderAlbum>() };
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
                    
                    if (album.Carts == null)
                    {
                        album.Carts = new List<OrderAlbum>();
                    }

                    album.Carts.Add(orderAlbum);
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
                if (cart == null || albumId == 0)
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
                        var orderAlbum = album.Carts.FirstOrDefault(c => c.CartId == cart.Id);
                        while (orderAlbum != null)
                        {
                            album.Carts.Remove(orderAlbum);
                            cart.Albums.Remove(orderAlbum);
                            orderAlbum = album.Carts.FirstOrDefault(c => c.CartId == cart.Id);
                        }
                    }
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
        public ICollection<Track> GetOrderTracks(int userId)
        {
            var returnResult = new List<Track>();
            using (var cartRepository = Factory.GetCartRepository())
            {
                var cart = cartRepository.GetByUserId(userId);
                using (var trackRepository = Factory.GetTrackRepository())
                {
                    returnResult.AddRange(cart.Tracks.Select(o => trackRepository.GetById(o.TrackId)));
                }
            }
            return returnResult;
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
        public ICollection<Album> GetOrderAlbums(int userId)
        {
            var returnResult = new List<Album>();
            using (var cartRepository = Factory.GetCartRepository())
            {
                var cart = cartRepository.GetByUserId(userId);
                using (var albumsRepository = Factory.GetAlbumRepository())
                {
                    returnResult.AddRange(cart.Albums.Select(o => albumsRepository.GetById(o.AlbumId)));
                }
            }
            return returnResult;
        }
    }
}
