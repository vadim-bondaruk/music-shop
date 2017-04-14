namespace Shop.Common.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents all tracks list from the album.
    /// </summary>
    public class AlbumTracksListViewModel
    {
        private int? _tracksCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumTracksListViewModel"/> class.
        /// </summary>
        public AlbumTracksListViewModel()
        {
            this.Tracks = new List<TrackViewModel>();
        }

        /// <summary>
        /// Gets or sets the album details.
        /// </summary>
        public AlbumDetailsViewModel AlbumDetails
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

        /// <summary>
        /// The number of the tracks from the album.
        /// </summary>
        public int TracksCount  
        {
            get
            {
                if (_tracksCount == null)
                {
                    return Tracks != null ? Tracks.Count : 0;
                }

                return _tracksCount.Value;
            }
            set
            {
                _tracksCount = value;
            }
        }
    }
}