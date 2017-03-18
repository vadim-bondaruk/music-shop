﻿namespace Shop.Common.Models.ViewModels
{
    /// <summary>
    /// The track view model.
    /// </summary>
    public class TrackViewModel
    {
        /// <summary>
        /// Gets or sets the track id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the track name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The track artist.
        /// </summary>
        public ArtistViewModel Artist { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public PriceViewModel Price { get; set; }
    }
}