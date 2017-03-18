namespace Shop.Common.Models.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the albums list where the track exists.
    /// </summary>
    public class TrackAlbumListViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackAlbumListViewModel"/> class.
        /// </summary>
        public TrackAlbumListViewModel()
        {
            this.Albums = new List<AlbumDetailsViewModel>();
        }

        /// <summary>
        /// Gets or sets the track id.
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        /// Gets or sets the track name.
        /// </summary>
        public string TrackName { get; set; }

        /// <summary>
        /// Gets or sets the artist id.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the artist name.
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// Gets or sets the albums.
        /// </summary>
        public ICollection<AlbumDetailsViewModel> Albums { get; set; }
    }
}