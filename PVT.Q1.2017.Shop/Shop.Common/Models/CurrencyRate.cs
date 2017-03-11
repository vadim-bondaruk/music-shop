﻿namespace Shop.Common.Models
{
    using Infrastructure.Models;
    
    /// <summary>
    ///     The currency rate.
    /// </summary>
    public class CurrencyRate : BaseEntity
    {
        /// <summary>
        ///     Gets or sets the cross course.
        /// </summary>
        public decimal CrossCourse { get; set; }

        /// <summary>
        /// Currency id
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Target currency id
        /// </summary>
        public int TargetCurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        ///     Gets or sets the target currency.
        /// </summary>
        public virtual Currency TargetCurrency { get; set; }
    }
}