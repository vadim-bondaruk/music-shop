namespace Shop.Common.Models.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    /// <summary>
    /// </summary>
    public class ArtistManageViewModel
    {
        /// <summary>
        ///     Gets or sets the biography.
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        ///     Gets or sets the birthday.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}", ApplyFormatInEditMode = true)]
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
        ///     Gets or sets the photo.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        ///     Gets or sets the my property.
        /// </summary>
        public HttpPostedFileBase UploadedImage { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Bindable(true)]
        public virtual string Value { get; set; }
    }
}