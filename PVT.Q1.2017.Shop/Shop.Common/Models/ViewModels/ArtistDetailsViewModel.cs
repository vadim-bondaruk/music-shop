namespace Shop.Common.Models.ViewModels
{
    using System;
    using System.Web;

    /// <summary>
    /// </summary>
    public class ArtistDetailsViewModel
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
        /// Gets or sets the birthday string.
        /// </summary>
        public string BirthdayString { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the photo.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        ///     Gets or sets the my property.
        /// </summary>
        public HttpPostedFileBase UploadedImage { get; set; }
    }
}