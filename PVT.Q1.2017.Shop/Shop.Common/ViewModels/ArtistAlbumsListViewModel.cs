namespace Shop.Common.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents all albums of the artist.
    /// </summary>
    public class ArtistAlbumsListViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistAlbumsListViewModel"/> class.
        /// </summary>
        public ArtistAlbumsListViewModel()
        {
            this.Albums = new List<AlbumViewModel>();
        }

        /// <summary>
        /// Artist id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Artist name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// All albums of the artist.
        /// </summary>
        public ICollection<AlbumViewModel> Albums { get; set; }
    }
}