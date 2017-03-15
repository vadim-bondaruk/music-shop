namespace PVT.Q1._2017.Shop.Areas.Management.Models
{
    using System;

    /// <summary>
    /// </summary>
    public class AlbumManagmentViewModel
    {
        /// <summary>
        ///     Gets or sets the cover.
        /// </summary>
        public byte[] Cover { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }
    }
}