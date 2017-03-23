﻿namespace PVT.Q1._2017.Shop.Areas.Management.ViewModels
{
    using System;
    using System.Web;
    using global::Shop.Common.Models.ViewModels;

    /// <summary>
    /// The track management view model.
    /// </summary>
    public class TrackManagementViewModel
    {
        /// <summary>
        /// Track id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Track name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Track release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Track image.
        /// </summary>
        public HttpPostedFileBase Image { get; set; }

        /// <summary>
        /// Track file.
        /// </summary>
        public HttpPostedFileBase TrackFile { get; set; }

        /// <summary>
        /// Track duration.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Track artist.
        /// </summary>
        public ArtistViewModel Artist { get; set; }

        /// <summary>
        /// Track genre.
        /// </summary>
        public GenreViewModel Genre { get; set; }
    }
}
