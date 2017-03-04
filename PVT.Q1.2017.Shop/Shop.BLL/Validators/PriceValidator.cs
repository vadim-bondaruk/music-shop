namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Infrastructure.Validators;

    /// <summary>
    /// The common price validator.
    /// </summary>
    /// <typeparam name="TPrice">
    /// The price model type derived from <see cref="BasePriceEntity"/>
    /// </typeparam>
    public abstract class PriceValidator<TPrice> : IValidator<TPrice> where TPrice : BasePriceEntity
    {
        #region Public Methods

        /// <summary>
        /// Determines whether the specified <paramref name="price"/> is valid.
        /// </summary>
        /// <param name="price">
        /// The price amount.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="price"/> is valid; otherwise <b>false</b>.
        /// </returns>
        public static bool IsPriceValid(decimal price)
        {
            return price >= 0m;
        }

        #endregion //Public Methods

        #region IValidator<TPrice> Members

        /// <summary>
        /// </summary>
        /// <param name="price">
        /// The price.
        /// </param>
        public virtual void Validate(TPrice price)
        {
            if (price == null)
            {
                throw new ArgumentNullException(nameof(price));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="price">
        /// The price.
        /// </param>
        /// <returns>
        /// </returns>
        public bool IsValid(TPrice price)
        {
            if (price == null)
            {
                return false;
            }

            return true;
        }

        #endregion //IValidator<TPrice> Members
    }
}