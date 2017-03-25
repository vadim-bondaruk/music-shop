namespace PVT.Q1._2017.Shop.Areas.Management.ViewModels
{
    using System;
    using System.Web;

    using global::Shop.Common.Models;

    /// <summary>
    ///     The album view model.
    /// </summary>
    public class AlbumManagementViewModel
    {
        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        ///     Gets or sets the album cover.
        /// </summary>
        public HttpPostedFileBase Cover { get; set; }

        /// <summary>
        ///     Gets or sets the album name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the album release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        ///     Gets or sets the album price.
        /// </summary>
        public HttpPostedFileBase UploadedImage { get; set; }
    }
}