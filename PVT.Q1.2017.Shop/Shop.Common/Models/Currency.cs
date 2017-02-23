namespace Shop.Common.Models
{
    using Shop.Infrastructure.Models;

    /// <summary>
    /// Model for Currency
    /// </summary>
    public class Currency : BaseEntity
    {
        /// <summary>
        /// ShortName of Currency
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// FullName of Currency
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Code of Currency
        /// </summary>
        public int Code { get; set; }

    }
}
