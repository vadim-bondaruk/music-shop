namespace PVT.Q1._2017.Shop.Areas.Management.ViewModels
{
    using System;
    using System.Web;
    using FluentValidation.Attributes;
    using global::Shop.Common.ViewModels;
    using Validators;

    /// <summary>
    /// The track management view model.
    /// </summary>
    [Validator(typeof(TrackManagementValidator))]
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
        /// The track image. Optional.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// The track file. Required. We are selling it!
        /// </summary>
        public byte[] TrackFile { get; set; }

        /// <summary>
        /// The track sample. Optional.
        /// </summary>
        public byte[] TrackSample { get; set; }

        /// <summary>
        /// Posted track image file.
        /// </summary>
        public HttpPostedFileBase PostedImage { get; set; }

        /// <summary>
        /// Posted track file.
        /// </summary>
        public HttpPostedFileBase PostedTrackFile { get; set; }

        /// <summary>
        /// Posted track sample file.
        /// </summary>
        public HttpPostedFileBase PostedTrackSample { get; set; }

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
