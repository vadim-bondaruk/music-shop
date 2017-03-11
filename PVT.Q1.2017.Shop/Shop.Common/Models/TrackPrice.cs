namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// The track price.
    /// </summary>
    public class TrackPrice : BaseEntity
    {
        /// <summary>
        /// Price for track
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Track id
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        /// Get or set price level id
        /// </summary>
        public int PriceLevelId { get; set; }

        /// <summary>
        /// Currency id
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        public virtual Track Track { get; set; }

        /// <summary>
        /// Get or set <see cref="PriceLevel"/>
        /// </summary>
        public virtual PriceLevel PriceLevel { get; set; }

        /// <summary>
        /// The currency.
        /// </summary>
        public virtual Currency Currency { get; set; }
    }
}