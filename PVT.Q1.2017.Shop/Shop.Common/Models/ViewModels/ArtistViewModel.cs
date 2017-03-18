namespace Shop.Common.Models.ViewModels
{
    using System;

    /// <summary>
    /// The artist view model.
    /// </summary>
    public class ArtistViewModel
    {
        /// <summary>
        /// Gets or sets the artist id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the artist name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the biography.
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Gets or sets the artist photo.
        /// </summary>
        public byte[] Photo { get; set; }
    }
}