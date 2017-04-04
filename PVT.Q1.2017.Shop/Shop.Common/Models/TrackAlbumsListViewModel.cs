namespace Shop.Common.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the albums list where the track exists.
    /// </summary>
    public class TrackAlbumsListViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackAlbumsListViewModel"/> class.
        /// </summary>
        public TrackAlbumsListViewModel()
        {
            this.Albums = new List<AlbumViewModel>();
        }

        /// <summary>
        /// Track id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Track name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The track artist.
        /// </summary>
        public ArtistViewModel Artist { get; set; }

        /// <summary>
        /// All albums where the track exists.
        /// </summary>
        public ICollection<AlbumViewModel> Albums { get; set; }
    }
}