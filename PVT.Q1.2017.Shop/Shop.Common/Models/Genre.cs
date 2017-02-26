namespace Shop.Common.Models
{
    using System.Collections.Generic;
    using Infrastructure.Models;

    /// <summary>
    /// The genre.
    /// </summary>
    public class Genre : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        public ICollection<Track> Tracks { get; set; }
    }
}