// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrencyRate.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   The currency rate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.Common.Models
{
    #region

    using Shop.Infrastructure.Models;

    #endregion

    /// <summary>
    ///     The currency rate.
    /// </summary>
    public class CurrencyRate : BaseEntity
    {
        /// <summary>
        ///     Gets or sets the cross course.
        /// </summary>
        public double CrossCourse { get; set; }

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