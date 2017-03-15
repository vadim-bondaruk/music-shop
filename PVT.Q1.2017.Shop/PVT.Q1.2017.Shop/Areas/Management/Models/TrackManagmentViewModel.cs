namespace PVT.Q1._2017.Shop.Areas.Management.Models
{
    using System;

    using global::Shop.Common.Models;

    /// <summary>
    /// </summary>
    public class TrackManagmentViewModel
    {
        /// <summary>
        ///     Gets or sets the album name.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        ///     Gets or sets the artist name.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        ///     Gets or sets the duration.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        ///     Gets or sets the genre name.
        /// </summary>
        public Genre Genre { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        ///     Gets or sets the release date.
        /// </summary>
        public TimeSpan? ReleaseDate { get; set; }
    }
}