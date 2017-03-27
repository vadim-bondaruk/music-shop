﻿namespace Shop.Common.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     The album details view model.
    /// </summary>
    public class AlbumDetailsViewModel
    {
        /// <summary>
        ///     The Artist. Optional.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the artist name.
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        ///     Album cover.
        /// </summary>
        public byte[] Cover { get; set; }

        /// <summary>
        ///     Album id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Album name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Album price.
        /// </summary>
        public PriceViewModel Price { get; set; }

        /// <summary>
        ///     Album release date.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}г.", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }
    }
}