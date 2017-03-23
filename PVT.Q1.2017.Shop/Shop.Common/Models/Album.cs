﻿namespace Shop.Common.Models
{
    using System;
    using System.Collections.Generic;
    using FluentValidation.Attributes;
    using Infrastructure.Models;
    using Validators;

    /// <summary>
    /// The album.
    /// </summary>
    [Validator(typeof(AlbumValidator))]
    public class Album : BaseEntity
    {
        /// <summary>
        /// Album name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cover. Optional.
        /// </summary>
        public byte[] Cover { get; set; }

        /// <summary>
        /// Album release date. Optional.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Artist id (foreign key). Optional.
        /// </summary>
        public int? ArtistId { get; set; }

        /// <summary>
        /// Artist.
        /// </summary>
        public virtual Artist Artist { get; set; }

        /// <summary>
        /// All tracks from the album.
        /// </summary>
        public virtual ICollection<AlbumTrackRelation> Tracks { get; set; }

        /// <summary>
        /// Album prices.
        /// </summary>
        public virtual ICollection<AlbumPrice> AlbumPrices { get; set; }
    }
}