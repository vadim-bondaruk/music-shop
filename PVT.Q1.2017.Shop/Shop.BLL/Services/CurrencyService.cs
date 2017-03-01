namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Models;
    using Infrastructure.Core;
    using Infrastructure.Repositories;

    /// <summary>
    /// Currency service interface
    /// </summary>
    public interface ICurrencyService
    {
        /// <summary>
        /// Get rate of currency by date
        /// </summary>
        /// <param name="from">Currency</param>
        /// <param name="to">Target currency</param>
        /// <param name="date">Date</param>
        /// <returns></returns>
        Task<decimal> GetRateByDateAsync(int from, int to, DateTime date);

        /// <summary>
        /// Get rates by date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns><see cref="IEnumerable{CurrencyRate}"/></returns>
        Task<IEnumerable<CurrencyRate>> GetActualRatesAsync(DateTime date);
    }

    /// <summary>
    /// Currency service
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        /// <summary>
        /// Factory
        /// </summary>
        private readonly IFactory _factory;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="factory"></param>
        public CurrencyService(IFactory factory)
        {
            this._factory = factory;
        }

        /// <summary>
        /// Get rates by date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns><see cref="IEnumerable{CurrencyRate}"/></returns>
        public async Task<IEnumerable<CurrencyRate>> GetActualRatesAsync(DateTime date)
        {
            using (var repository = this._factory.Create<IRepository>())
            {
                var rates = (await repository.TakeAllAsync<CurrencyRate>(rate => rate.Currency, rate => rate.TargetCurrency));
                return rates.Where(c => c.Date <= date)
                    .GroupBy(rate => new { rate.Currency, rate.TargetCurrency })
                        .Select(grouping => grouping.OrderByDescending(rate => rate.Date).FirstOrDefault());
            }
        }

        /// <summary>
        /// Get rate of currency by date
        /// </summary>
        /// <param name="from">Currency</param>
        /// <param name="to">Target currency</param>
        /// <param name="date">Date</param>
        /// <returns></returns>
        public async Task<decimal> GetRateByDateAsync(int from, int to, DateTime date)
        {
            using (var repository = this._factory.Create<IRepository>())
            {
                if (from == to)
                {
                    return 1;
                }

                var val = (await repository.TakeAllAsync<CurrencyRate>())
                    .Where(c => c.CurrencyId == from && c.TargetCurrencyId == to && c.Date <= date).OrderByDescending(rate => rate.Date).FirstOrDefault();

                return val != null ? val.CrossCourse : 1;
            }
        }
    }
}
