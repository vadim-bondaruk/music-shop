namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;

    using Shop.Infrastructure.Models;

    /// <summary>
    /// </summary>
    public class Artist : BaseEntity
    {
        /// <summary>
        ///     All albums of the current artist.
        /// </summary>
        public virtual ICollection<Album> Albums { get; set; }

        /// <summary>
        ///     Artist biography.
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        ///     Artist birthday.
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        ///     Artist name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Artist photo.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        ///     All tracks of the current artist.
        /// </summary>
        public virtual ICollection<Track> Tracks { get; set; }
    }
}