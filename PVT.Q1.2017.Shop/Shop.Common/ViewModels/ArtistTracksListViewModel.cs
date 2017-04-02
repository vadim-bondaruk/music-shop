namespace Shop.Common.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents all tracks of the artist.
    /// </summary>
    public class ArtistTracksListViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistTracksListViewModel"/> class.
        /// </summary>
        public ArtistTracksListViewModel()
        {
            this.Tracks = new List<TrackViewModel>();
        }

        /// <summary>
        /// Artist id.
        /// </summary>
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// Artist name.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// All tracks of the artist.
        /// </summary>
        public ICollection<TrackViewModel> Tracks
        {
            get; set;
        }
    }
}