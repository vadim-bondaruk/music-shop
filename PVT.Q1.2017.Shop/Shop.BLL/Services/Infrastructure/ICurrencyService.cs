namespace Shop.BLL.Services.Infrastructure
{
    using Common.Models;
    using Shop.Infrastructure.Services;

    /// <summary>
    /// The currency service.
    /// </summary>
    public interface ICurrencyService : IService<Currency>
    {
        /// <summary>
        /// Adds the currency into Db.
        /// </summary>
        /// <param name="name">
        /// The currency name (see ISO 421).
        /// </param>
        /// <param name="code">
        /// The currency code number (see ISO 421).
        /// </param>
        /// <param name="fullName">
        /// The full name.
        /// </param>
        void AddCurrency(string name, int code, string fullName = null);

        /// <summary>
        /// Determines whether a currency with the specified numeric <paramref name="code"/> exists.
        /// </summary>
        /// <param name="code">
        /// The currency numeric code (see ISO 421).
        /// </param>
        /// <returns>
        /// <b>true</b> if a currency with the specified <paramref name="code"/> exists;
        /// otherwise <b>false</b>.
        /// </returns>
        bool CurrencyExists(int code);

        /// <summary>
        /// Determines whether the specified <paramref name="currency"/> already exists.
        /// </summary>
        /// <param name="currency">
        /// The currency.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified <paramref name="currency"/> already exists;
        /// otherwise <b>false</b>.
        /// </returns>
        bool CurrencyExists(Currency currency);
    }
}