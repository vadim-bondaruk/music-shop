namespace Shop.Common.Models
{
    using System.Collections.Generic;
    using Shop.Infrastructure.Models;

    /// <summary>
    /// Model for Currency
    /// </summary>
    public class Currency : BaseEntity
    {
        #region Properties

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

        #endregion //Properties

        #region Navigation Properties

        /// <summary>
        /// Currency rates
        /// </summary>
        public virtual ICollection<CurrencyRate> CurrencyRates { get; set; }

        /// <summary>
        /// Target currency rates
        /// </summary>
        public virtual ICollection<CurrencyRate> TargetCurrencyRates { get; set; }

        /// <summary>
        /// Get all users
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// Get all album prices
        /// </summary>
        public virtual ICollection<AlbumPrice> AlbumPrices { get; set; }

        /// <summary>
        /// Get all track prices
        /// </summary>
        public virtual ICollection<TrackPrice> TrackPrices { get; set; }

        #endregion //Navigation Properties
    }
}
