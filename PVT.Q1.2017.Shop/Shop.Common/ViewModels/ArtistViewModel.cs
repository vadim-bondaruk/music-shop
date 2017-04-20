namespace Shop.Common.ViewModels
{
    /// <summary>
    /// The artist view model.
    /// </summary>
    public class ArtistViewModel
    {
        /// <summary>
        /// Artist id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Artist name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of the artist tracks.
        /// </summary>
        public int TracksCount { get; set; }

        /// <summary>
        /// The number of the artist albums.
        /// </summary>
        public int AlbumsCount { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// Gets or sets the biography.
        /// </summary>
        public object Biography { get; set; }
    }
}