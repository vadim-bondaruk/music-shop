namespace Shop.Common.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents all tracks list from the album.
    /// </summary>
    public class AlbumTracksListViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumTracksListViewModel"/> class.
        /// </summary>
        public AlbumTracksListViewModel()
        {
            this.Tracks = new List<TrackViewModel>();
        }

        /// <summary>
        /// Album id.
        /// </summary>
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// Album name.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// The album artist. Optional.
        /// </summary>
        public ArtistViewModel Artist
        {
            get; set;
        }

        /// <summary>
        /// All tracks from the album.
        /// </summary>
        public ICollection<TrackViewModel> Tracks
        {
            get; set;
        }
    }
}