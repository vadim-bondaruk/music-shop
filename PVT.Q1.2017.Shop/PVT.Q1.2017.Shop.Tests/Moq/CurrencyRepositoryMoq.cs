namespace PVT.Q1._2017.Shop.Tests.Moq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using global::Moq;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Models;

    public class CurrencyRepositoryMoq
    {
        private readonly List<Currency> _currencies = new List<Currency>();
        private readonly Mock<ICurrencyRepository> _mock;

        public CurrencyRepositoryMoq()
        {
            this._mock = new Mock<ICurrencyRepository>();

            this._currencies = new List<Currency>
                                   {
                                       new Currency { Id = 1, ShortName = "USD" },
                                       new Currency { Id = 2, ShortName = "Euro" },
                                       new Currency { Id = 3, ShortName = "BYR" }
                                   };

            this._mock.Setup(m => m.GetAll()).Returns(this._currencies);

            this._mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()))
                 .Returns(this._currencies);

            this._mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Currency, bool>>>()))
                 .Returns(this._currencies);

            this._mock.Setup(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Currency, bool>>>(),
                                     It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()))
                 .Returns(this._currencies);

            this._mock.Setup(m => m.GetById(It.IsAny<int>()))
                 .Returns(() => this._currencies.FirstOrDefault(a => a.Id >= 1));

            this._mock.Setup(m => m.GetById(It.IsAny<int>(),
                                       It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()))
                 .Returns(() => this._currencies.FirstOrDefault(a => a.Id >= 1));

            this._mock.Setup(m => m.AddOrUpdate(It.IsNotNull<Currency>())).Callback(() => this._currencies.Add(new Currency
            {
                Id = this._currencies.Count + 1,
                Code = this._currencies.Count + 100,
                ShortName = $"CR{this._currencies.Count + 1}"
            }));

            this._mock.Setup(m => m.Delete(It.IsNotNull<Currency>())).Callback(() =>
            {
                if (this._currencies.Any())
                {
                    this._currencies.RemoveAt(this._currencies.Count - 1);
                }
            });

            this._mock.Setup(m => m.GetDefaultCurrency()).Returns(new Currency { Code = 840, ShortName = "USD" });
        }

        public ICurrencyRepository Repository
        {
            get { return this._mock.Object; }
        }
    }
}