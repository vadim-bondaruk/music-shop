namespace PVT.Q1._2017.Shop.ViewModels
{
    using System;
    using global::Shop.Common.Models;

    /// <summary>
    /// </summary>
    public class TrackViewModel
    {
        /// <summary>
        ///     Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        ///     Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        ///     Gets or sets the duration.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        ///     Gets or sets the genre.
        /// </summary>
        public Genre Genre { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}