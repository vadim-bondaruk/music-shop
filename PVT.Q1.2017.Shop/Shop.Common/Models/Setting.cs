namespace Shop.Common.Models
{
    using Shop.Infrastructure.Models;

    /// <summary>
    /// Setting model
    /// </summary>
    public class Setting : BaseEntity
    {
        /// <summary>
        /// The default currency id.
        /// </summary>
        public int DefaultCurrencyId { get; set; }

        /// <summary>
        /// The default priceId
        /// </summary>
        public int PriceLevelId { get; set; }

        /// <summary>
        /// The default currency.
        /// </summary>
        public virtual Currency DefaultCurrency { get; set; }

        /// <summary>
        /// The price level.
        /// </summary>
        public virtual PriceLevel PriceLevel { get; set; }
    }
}
