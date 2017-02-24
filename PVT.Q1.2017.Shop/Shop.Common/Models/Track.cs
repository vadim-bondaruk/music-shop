﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Track.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   The track.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    #region

    using System;
    using System.Collections.Generic;

    using Shop.Infrastructure.Models;

    #endregion

    /// <summary>
    /// The track.
    /// </summary>
    public class Track : BaseEntity, ITrack
    {
        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the track duration.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets or sets all user feedbacks the current track.
        /// </summary>
        public ICollection<Feedback> Feedbacks { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        public Genre Genre { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the track file.
        /// </summary>
        public byte[] TrackFile { get; set; }

        /// <summary>
        /// Gets or sets the track prices.
        /// </summary>
        public ICollection<TrackPrice> TrackPrices { get; set; }

        /// <summary>
        /// Gets or sets all user votes for the current track.
        /// </summary>
        public ICollection<Vote> Votes { get; set; }
    }
}