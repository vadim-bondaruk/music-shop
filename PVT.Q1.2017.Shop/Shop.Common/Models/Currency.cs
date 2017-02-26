// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Currency.cs" company="PVT Q1 2017">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the Currency type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    using System.Collections.Generic;
    using Infrastructure.Models;
    
    /// <summary>
    /// The currency model.
    /// </summary>
    public class Currency : BaseEntity
    {
        /// <summary>
        /// Short name of currency
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Full name of currency
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// ISO 4217 code of currency 
        /// </summary>
        public int Code { get; set; }

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
    }
}