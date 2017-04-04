namespace Shop.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    /// <summary>
    /// </summary>
    public class ArtistManagementViewModel
    {
        /// <summary>
        ///     Gets or sets the biography.
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        ///     Gets or sets the birthday.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy} г.", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        ///     Gets or sets the photo.
        /// </summary>
        public HttpPostedFileBase PostedPhoto { get; set; }
    }
}