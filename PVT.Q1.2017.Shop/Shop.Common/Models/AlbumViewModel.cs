namespace Shop.Common.Models
{
    /// <summary>
    /// The album view model.
    /// </summary>
    public class AlbumViewModel
    {
        /// <summary>
        /// Album id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Album name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Artist. Optional.
        /// </summary>
        public ArtistViewModel Artist { get; set; }

        /// <summary>
        /// Album price.
        /// </summary>
        public PriceViewModel Price { get; set; }
    }
}