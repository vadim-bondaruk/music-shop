namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using DAL.Infrastruture;
    using Infrastructure;

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
                var cart = cartRepository.GetAll(c => c.UserId == userId).FirstOrDefault();
                if (cart == null)
                {
                    cart = new Cart { UserId = userId, Tracks = new List<Track>() };
                    cartRepository.AddOrUpdate(cart);
                }

                Track track;
                using (var trackRepository = Factory.GetTrackRepository())
                {
                    track = trackRepository.GetById(trackId);
                    if (track == null || trackId == 0)
                    {
                        throw new Exception($"Трек с ID={trackId} не найден.");
                    }
                }

                if (cart.Tracks == null)
                {
                    cart.Tracks = new List<Track>();
                }

                cart.Tracks.Add(track);
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
                var cart = cartRepository.GetAll(c => c.UserId == userId).FirstOrDefault();
                if (cart == null || trackId == 0)
                {
                    return;
                }

                Track track;
                using (var trackRepository = Factory.GetTrackRepository())
                {
                    track = trackRepository.GetById(trackId);
                }

                if (track != null && cart.Tracks != null)
                {
                    cart.Tracks.Remove(track);
                    cartRepository.AddOrUpdate(cart);
                }

                if ((cart.Tracks == null || cart.Tracks.Count == 0) 
                    && (cart.Albums == null || cart.Albums.Count == 0))
                {
                    cartRepository.Delete(cart);
                }
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
                var cart = cartRepository.GetAll(c => c.UserId == userId).FirstOrDefault();
                if (cart == null)
                {
                    cart = new Cart { UserId = userId, Albums = new List<Album>() };
                    cartRepository.AddOrUpdate(cart);
                }

                Album album;
                using (var albumRepository = Factory.GetAlbumRepository())
                {
                    album = albumRepository.GetById(albumId);
                    if (album == null || albumId == 0)
                    {
                        throw new Exception($"Альбом с ID={albumId} не найден.");
                    }
                }

                if (cart.Albums == null)
                {
                    cart.Albums = new List<Album>();
                }

                cart.Albums.Add(album);
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
                var cart = cartRepository.GetAll(c => c.UserId == userId).FirstOrDefault();
                if (cart == null || albumId == 0)
                {
                    return;
                }

                Album album;
                using (var albumRepository = Factory.GetAlbumRepository())
                {
                    album = albumRepository.GetById(albumId);
                }

                if (album != null && cart.Albums != null)
                {
                    cart.Albums.Remove(album);
                    cartRepository.AddOrUpdate(cart);
                }

                if ((cart.Albums == null || cart.Albums.Count == 0)
                    && (cart.Tracks == null || cart.Tracks.Count == 0))
                {
                    cartRepository.Delete(cart);
                }
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
    }
}
