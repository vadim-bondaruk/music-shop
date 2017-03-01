namespace Shop.Common.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The genre.
    /// </summary>
    public class Genre : BaseNamedEntity
    {
        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        public ICollection<Track> Tracks { get; set; }
    }
}