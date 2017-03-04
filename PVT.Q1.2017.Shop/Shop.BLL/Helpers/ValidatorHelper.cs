namespace Shop.BLL.Helpers
{
    using System;
    using Common.Models;

    /// <summary>
    /// The validator helper.
    /// </summary>
    internal static class ValidatorHelper
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
        public static void CheckTrackForNull(Track track)
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
        public static void CheckUserForNull(User user)
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
        public static void CheckPriceLevelForNull(PriceLevel priceLevel)
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
        public static void CheckCurrencyForNull(Currency currency)
        {
            if (currency == null)
            {
                throw new ArgumentNullException(nameof(currency));
            }
        }

        /// <summary>
        /// Checks the <paramref name="feedback"/>.
        /// </summary>
        /// <param name="feedback">
        /// The feedback.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When the <paramref name="feedback"/> is <b>null</b>.
        /// </exception>
        public static void CheckFeedbackForNull(Feedback feedback)
        {
            if (feedback == null)
            {
                throw new ArgumentNullException(nameof(feedback));
            }
        }

        /// <summary>
        /// Checks the <paramref name="vote"/>.
        /// </summary>
        /// <param name="vote">
        /// The vote.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When the <paramref name="vote"/> is <b>null</b>.
        /// </exception>
        public static void CheckVoteForNull(Vote vote)
        {
            if (vote == null)
            {
                throw new ArgumentNullException(nameof(vote));
            }
        }

        /// <summary>
        /// Checks the <paramref name="album"/>.
        /// </summary>
        /// <param name="album">
        /// The album.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When the <paramref name="album"/> is <b>null</b>.
        /// </exception>
        public static void CheckAlbumForNull(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException(nameof(album));
            }
        }

        /// <summary>
        /// Checks the <paramref name="artist"/>.
        /// </summary>
        /// <param name="artist">
        /// The artist.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When the <paramref name="artist"/> is <b>null</b>.
        /// </exception>
        public static void CheckArtistForNull(Artist artist)
        {
            if (artist == null)
            {
                throw new ArgumentNullException(nameof(artist));
            }
        }
    }
}