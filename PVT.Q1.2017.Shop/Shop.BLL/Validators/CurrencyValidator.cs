namespace Shop.BLL.Validators
{
    using System;
    using Common.Models;
    using Exceptions;
    using Infrastructure.Validators;

    /// <summary>
    /// The currency validator.
    /// </summary>
    public class CurrencyValidator : IValidator<Currency>
    {
        #region Public Static Methods

        /// <summary>
        /// Determines whether the specified <paramref name="currencyName"/> is valid.
        /// </summary>
        /// <param name="currencyName">
        /// The currency name (see ISO 4217).
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="currencyName"/> is valid;
        /// otherwise <b>false</b>.
        /// </returns>
        public static bool IsCurrencyNameValid(string currencyName)
        {
            return !string.IsNullOrWhiteSpace(currencyName) && currencyName.Length == 3;
        }

        /// <summary>
        /// Determines whether the specified <paramref name="currencyCode"/> number is valid.
        /// </summary>
        /// <param name="currencyCode">
        /// The currency code number (see ISO 4217).
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="currencyCode"/> number is valid;
        /// otherwise <b>false</b>.
        /// </returns>
        public static bool IsCurrencyCodeValid(int currencyCode)
        {
            return currencyCode > 0;
        }

        #endregion //Public Static Methods

        #region IValidator<Currency> Members

        /// <summary>
        /// Validates the specified <paramref name="currency"/>.
        /// </summary>
        /// <param name="currency">
        /// The currency to validate.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// When the <paramref name="currency"/> is null.
        /// </exception>
        /// <exception cref="InvalidEntityException">
        /// When the <paramref name="currency"/> name is invalid
        /// or the <paramref name="currency"/> numeric code is invalid.
        /// </exception>
        public void Validate(Currency currency)
        {
            if (currency == null)
            {
                throw new ArgumentNullException(nameof(currency));
            }

            if (!IsCurrencyNameValid(currency.Name))
            {
                throw new InvalidEntityException("The currency has invalid name");
            }

            if (!IsCurrencyCodeValid(currency.Code))
            {
                throw new InvalidEntityException("The currency has invalid numeric code");
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="currency"/> is valid.
        /// </summary>
        /// <param name="currency">
        /// The currency to verify.
        /// </param>
        /// <returns>
        /// <b>true</b> if the <paramref name="currency"/> is valid; otherwise <b>false</b>;
        /// otherwise <b>false</b>.
        /// </returns>
        public bool IsValid(Currency currency)
        {
            if (currency == null)
            {
                return false;
            }

            if (!IsCurrencyNameValid(currency.Name))
            {
                return false;
            }

            if (!IsCurrencyCodeValid(currency.Code))
            {
                return false;
            }

            return true;
        }

        #endregion //IValidator<Currency> Members
    }
}