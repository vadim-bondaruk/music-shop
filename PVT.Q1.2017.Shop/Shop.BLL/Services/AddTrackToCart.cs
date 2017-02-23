namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using Common.Models;
    using Infrastructure.Services;

    /// <summary>
    /// Add track(s) to user's cart
    /// </summary>
    public class AddTrackToCart : IService<Cart>
    {
        /// <summary>
        /// Cart to add the track(s)
        /// </summary>
        private Cart _cart;

        #region Constructors

        /// <summary>
        /// Initialize new instance of AddTrack Service
        /// </summary>
        /// <param name="cart">Cart to add the track</param>
        public AddTrackToCart(Cart cart)
        {
            this._cart = cart;
        }

        /// <summary>
        /// Initialize new instance of AddTrack Service
        /// </summary>
        /// <param name="cart">Cart to add the track</param>
        /// <param name="track">Track to add</param>
        public AddTrackToCart(Cart cart, Track track)
        {
            this._cart = cart;
            this.AddTrack(track);
        }

        /// <summary>
        /// Initialize new instance of AddTrack Service
        /// </summary>
        /// <param name="cart">Cart to add the track</param>
        /// <param name="tracks">Tracks to add</param>
        public AddTrackToCart(Cart cart, ICollection<Track> tracks)
        {
            this._cart = cart;
            this.AddTracks(tracks);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add new track to user's cart
        /// </summary>
        /// <param name="track">Tracks to add</param>
        public void AddTrack(Track track)
        {
            this._cart.Tracks?.Add(track);
        }

        /// <summary>
        /// Add new track list to user's cart
        /// </summary>
        /// <param name="tracks">Tracks to add</param>
        public void AddTracks(ICollection<Track> tracks)
        {
            foreach (var track in tracks)
            {
                this._cart.Tracks?.Add(track);
            }
        }

        #endregion
    }
}
