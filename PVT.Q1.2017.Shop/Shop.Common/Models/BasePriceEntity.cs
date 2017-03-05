namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// The base price entity.
    /// </summary>
    public abstract class BasePriceEntity : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Price for track
        /// </summary>
        public decimal Price { get; set; }

        #endregion //Properties

        #region Foreign Keys

        /// <summary>
        /// Get or set price level id
        /// </summary>
        public int PriceLevelId { get; set; }

        /// <summary>
        /// Currency id
        /// </summary>
        public int CurrencyId { get; set; }

        #endregion //Foreign Keys

        #region Navigation Properties

        /// <summary>
        /// Get or set <see cref="PriceLevel"/>
        /// </summary>
        public virtual PriceLevel PriceLevel { get; set; }

        /// <summary>
        /// The currency.
        /// </summary>
        public virtual Currency Currency { get; set; }

        #endregion //Navigation Properties
    }
}
