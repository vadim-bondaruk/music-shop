namespace Shop.BLL.Helpers
{
    using System;
    using Common.Models;

    /// <summary>
    /// The service helper.
    /// </summary>
    internal static class ServiceHelper
    {
        /// <summary>
        /// Checks the <paramref name="track"/>.
        /// </summary>
        /// <param name="track">
        /// The track.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="track"/> is <b>null</b>.
        /// </exception>
        public static void CheckTrack(Track track)
        {
            if (track == null)
            {
                throw new ArgumentNullException(nameof(track));
            }
        }

        /// <summary>
        /// Checks the <paramref name="user"/>.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="user"/> is <b>null</b>.
        /// </exception>
        public static void CheckUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
        }

        /// <summary>
        /// Checks the price level.
        /// </summary>
        /// <param name="priceLevel">
        /// The price level.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When the price level is <b>null</b>.
        /// </exception>
        public static void CheckPriceLevel(PriceLevel priceLevel)
        {
            if (priceLevel == null)
            {
                throw new ArgumentNullException(nameof(priceLevel));
            }
        }

        /// <summary>
        /// Checks the <paramref name="currency"/>.
        /// </summary>
        /// <param name="currency">
        /// The currency.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When the <paramref name="currency"/> is <b>null</b>.
        /// </exception>
        public static void CheckCurrency(Currency currency)
        {
            if (currency == null)
            {
                throw new ArgumentNullException(nameof(currency));
            }
        }
    }
}