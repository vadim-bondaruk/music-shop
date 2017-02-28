namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// The base price entity.
    /// </summary>
    public class BasePriceEntity : BaseEntity
    {
        /// <summary>
        /// Get or set price level id
        /// </summary>
        public int PriceLevelId { get; set; }
        
        /// <summary>
        /// Get or set <see cref="PriceLevel"/>
        /// </summary>
        public virtual PriceLevel PriceLevel { get; set; }

        /// <summary>
        /// Price for track
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Currency id
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        public Currency Currency { get; set; }
    }
}