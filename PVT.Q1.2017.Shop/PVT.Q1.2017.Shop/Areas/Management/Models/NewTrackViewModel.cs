namespace PVT.Q1._2017.Shop.Areas.Management.Models
{
    using System;
    using System.Collections.Generic;

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
        public TimeSpan Duration { get; set; }

        /// <summary>
        ///     Gets or sets the track.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        public Track Track { get; set; }
    }
}