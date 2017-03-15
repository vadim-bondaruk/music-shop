namespace PVT.Q1._2017.Shop.Areas.Management.Models
{
    using System;

    /// <summary>
    /// </summary>
    public class ArtistManagmentViewModel
    {
        /// <summary>
        ///     Gets or sets the biography.
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        ///     Gets or sets the birthday.
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the photo.
        /// </summary>
        public byte[] Photo { get; set; }
    }
}