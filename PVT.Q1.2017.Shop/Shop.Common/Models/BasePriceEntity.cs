namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// The base price entity.
    /// </summary>
    public class BasePriceEntity : BaseEntity
    {
        /// <summary>
        /// Gets or sets the album cost amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        public Currency Currency { get; set; }
    }
}