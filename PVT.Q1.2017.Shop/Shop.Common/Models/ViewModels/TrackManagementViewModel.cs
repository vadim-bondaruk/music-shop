﻿namespace Shop.Common.Models.ViewModels
{
    using System;
    using System.Web;

    /// <summary>
    /// </summary>
    public class TrackManagementViewModel
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
        ///     Gets or sets the image.
        /// </summary>
        public HttpPostedFileBase Image { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the price.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        ///     Gets or sets the release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the track file.
        /// </summary>
        public HttpPostedFileBase TrackFile { get; set; }
    }
}