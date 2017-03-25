namespace Shop.Common.Models.ViewModels
{
    using System;
    using System.Web;

    /// <summary>
    ///     The artist details view model.
    /// </summary>
    public class ArtistDetailsViewModel
    {
        /// <summary>
        ///     Artist biography.
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        ///     Artist birthday.
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        ///     Artist id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Artist name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the photo.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// Gets or sets the posted photo.
        /// </summary>
        public HttpPostedFileBase PostedPhoto { get; set; }
    }
}