namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using Common.Models;
    using Infrastructure.Services;

    /// <summary>
    /// Remove track(s) from user's cart
    /// </summary>
    public class RemoveTrackFromCart : IService<Cart>
    {
        /// <summary>
        /// Cart to remove the track(s)
        /// </summary>
        private Cart _cart;

        #region Constructors

        /// <summary>
        /// Initialize new instance of RemoveTrack Service
        /// </summary>
        /// <param name="cart">Cart to remove the track</param>
        public RemoveTrackFromCart(Cart cart)
        {
            this._cart = cart;
        }

        /// <summary>
        /// Initialize new instance of RemoveTrack Service
        /// </summary>
        /// <param name="cart">Cart to remove the track</param>
        /// <param name="track">Track to remove</param>
        public RemoveTrackFromCart(Cart cart, Track track)
        {
            this._cart = cart;
            this.RemoveTrack(track);
        }

        /// <summary>
        /// Initialize new instance of RemoveTrack Service
        /// </summary>
        /// <param name="cart">Cart to remove the track</param>
        /// <param name="tracks">Tracks to remove</param>
        public RemoveTrackFromCart(Cart cart, ICollection<Track> tracks)
        {
            this._cart = cart;
            this.RemoveTracks(tracks);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Remove track from user's cart
        /// </summary>
        /// <param name="track">Track to remove</param>
        /// <returns>True if item was successfully removed from the user's cart; otherwise, false. 
        /// This method also returns false if item is not found in the user's cart</returns>
        public bool RemoveTrack(Track track)
        {
            return this._cart.Tracks.Remove(track);
        }

        /// <summary>
        /// Remove track from user's cart
        /// </summary>
        /// <param name="tracks">Tracks to remove</param>
        /// <returns>True if items was successfully removed from the user's cart; otherwise, false. 
        /// This method also returns false if any of items is not found in the user's cart</returns>
        public bool RemoveTracks(ICollection<Track> tracks)
        {
            var result = true;
            var backup = this._cart.Tracks;
            foreach (var track in tracks)
            {
                result = this._cart.Tracks.Remove(track);
            }
            ////If not found in the cart one or more tracks, it returns to its original state
            if (result)
            {
                return true;
            }

            this._cart.Tracks.Clear();
            this._cart.Tracks = backup;
            return false;
        }

        #endregion
    }
}
