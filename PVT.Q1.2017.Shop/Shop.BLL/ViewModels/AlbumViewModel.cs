namespace Shop.BLL.ViewModels
{
    using System;
    using Common.Models;

    /// <summary>
    /// The album view model.
    /// </summary>
    public class AlbumViewModel
    {
        /// <summary>
        /// Gets or sets the album id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the album name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the album cover.
        /// </summary>
        public byte[] Cover { get; set; }

        /// <summary>
        /// Gets or sets the album release date.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the artist id.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the artist name.
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// Gets or sets the album price.
        /// </summary>
        public AlbumPrice Price { get; set; }
    }
}