namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// The album price.
    /// </summary>
    public class AlbumPrice : BaseEntity
    {
        /// <summary>
        /// Price for track
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Album id
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Get or set price level id
        /// </summary>
        public int PriceLevelId { get; set; }

        /// <summary>
        /// Currency id
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Album"/>.
        /// </summary>
        public virtual Album Album { get; set; }

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