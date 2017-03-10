namespace Shop.Common.ViewModels.Admin
{
    /// <summary>
    /// </summary>
    public class CurrencyViewModel
    {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Soft deleted flag
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the shortname
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the fullname
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the graphic currency symbol.
        /// </summary>
        public string Symbol { get; set; }
    }
}