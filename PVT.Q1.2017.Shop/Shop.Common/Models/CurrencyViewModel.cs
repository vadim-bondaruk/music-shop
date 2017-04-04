namespace Shop.Common.Models
{
    /// <summary>
    /// The currency view model.
    /// </summary>
    public class CurrencyViewModel
    {
        /// <summary>
        /// ISO 4217 litteral code of currency
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Full name of currency
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// ISO 4217 numeric code of currency
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the graphic currency symbol.
        /// </summary>
        public string Symbol { get; set; }
    }
}