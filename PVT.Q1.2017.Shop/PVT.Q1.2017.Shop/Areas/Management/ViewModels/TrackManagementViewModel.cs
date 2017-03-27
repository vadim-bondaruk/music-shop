namespace PVT.Q1._2017.Shop.Areas.Management.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using global::Shop.Common.Models;

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
        public byte[] Image { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the posted image.
        /// </summary>
        public HttpPostedFileBase PostedImage { get; set; }

        /// <summary>
        /// Gets or sets the posted track file.
        /// </summary>
        public HttpPostedFileBase PostedTrackFile { get; set; }

        /// <summary>
        ///     Gets or sets the price.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        ///     Gets or sets the release date.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy} г.", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        ///     Gets or sets the track file.
        /// </summary>
        public byte[] TrackFile { get; set; }
    }
}