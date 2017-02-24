// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShopContext.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Music shop Db
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.DAL.Context
{
    #region

    using System.Data.Entity;

    using Shop.Common.Models;

    #endregion

    /// <summary>
    /// Music shop Db
    /// </summary>
    public class ShopContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShopContext"/> class.
        /// </summary>
        public ShopContext()
            : base("ShopConnection")
        {
        }

        /// <summary>
        /// Gets or sets the album prices.
        /// </summary>
        public DbSet<AlbumPrice> AlbumPrices { get; set; }

        /// <summary>
        /// Gets or sets the albums.
        /// </summary>
        public DbSet<Album> Albums { get; set; }

        /// <summary>
        /// Gets or sets the artists.
        /// </summary>
        public DbSet<Artist> Artists { get; set; }

        /// <summary>
        /// Gets or sets the currency rates.
        /// </summary>
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        /// <summary>
        /// Gets or sets the feedbacks.
        /// </summary>
        public DbSet<Feedback> Feedbacks { get; set; }

        /// <summary>
        /// Gets or sets the genres.
        /// </summary>
        public DbSet<Genre> Genres { get; set; }

        /// <summary>
        /// Gets or sets the track prices.
        /// </summary>
        public DbSet<TrackPrice> TrackPrices { get; set; }

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        public DbSet<Track> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the votes.
        /// </summary>
        public DbSet<Vote> Votes { get; set; }
    }
}