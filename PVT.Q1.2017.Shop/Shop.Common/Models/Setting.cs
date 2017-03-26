namespace Shop.Common.Models
{
    using Shop.Infrastructure.Models;

    /// <summary>
    /// Setting model
    /// </summary>
    public class Setting : BaseEntity
    {
        /// <summary>
        /// Gets or sets the default currency id.
        /// </summary>
        public int DefaultCurrencyId { get; set; }

        public int PriceLevelId { get; set; }
        /// <summary>
        /// Gets or sets the default currency.
        /// </summary>
        public virtual Currency DefaultCurrency { get; set; }

        /// <summary>
        /// Gets or sets the price level.
        /// </summary>
        public virtual PriceLevel PriceLevel { get; set; }
    }
}
