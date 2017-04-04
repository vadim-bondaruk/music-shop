namespace Shop.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    /// <summary>
    ///     The album view model.
    /// </summary>
    public class AlbumManagementViewModel
    {
        /// <summary>
        ///     Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the artst name.
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        ///     Gets or sets the album cover.
        /// </summary>
        public byte[] Cover { get; set; }

        /// <summary>
        ///     Gets or sets the id id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the album name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the posted cover.
        /// </summary>
        public HttpPostedFileBase PostedCover { get; set; }

        /// <summary>
        ///     Gets or sets the album release date.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy} г.", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }
    }
}