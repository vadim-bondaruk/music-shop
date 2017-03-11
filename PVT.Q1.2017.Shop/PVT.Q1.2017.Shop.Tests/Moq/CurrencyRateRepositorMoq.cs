﻿namespace PVT.Q1._2017.Shop.Tests.Moq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using global::Moq;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Models;

    public class CurrencyRateRepositorMoq
    {
        private readonly Mock<ICurrencyRateRepository> _mock;
        private readonly List<CurrencyRate> _currencyRates = new List<CurrencyRate>();

        public CurrencyRateRepositorMoq()
        {
            _mock = new Mock<ICurrencyRateRepository>();

            _mock.Setup(m => m.GetAll()).Returns(_currencyRates);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<CurrencyRate, BaseEntity>>[]>()))
                .Returns(_currencyRates);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<CurrencyRate, bool>>>()))
                .Returns(_currencyRates);

            _mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<CurrencyRate, bool>>>(),
                                    It.IsAny<Expression<Func<CurrencyRate, BaseEntity>>[]>()))
                .Returns(_currencyRates);

            _mock.Setup(m => m.GetById(It.IsInRange(1, _currencyRates.Count, Range.Inclusive)))
                .Returns(_currencyRates.FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, _currencyRates.Count))))
                .Returns(() => null);

            _mock.Setup(m => m.GetById(It.IsInRange(1, _currencyRates.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<CurrencyRate, BaseEntity>>[]>()))
                .Returns(_currencyRates.FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, _currencyRates.Count)),
                                      It.IsAny<Expression<Func<CurrencyRate, BaseEntity>>[]>()))
                .Returns(() => null);

            _mock.Setup(m => m.AddOrUpdate(It.IsNotNull<CurrencyRate>())).Callback(() => _currencyRates.Add(new CurrencyRate
            {
                Id = _currencyRates.Count + 1,
                CrossCourse = _currencyRates.Count + 1.99m,
                Currency = new Currency
                {
                    Id = 1,
                    ShortName = "USD",
                    Code = 840
                },
                TargetCurrency = new Currency
                {
                    Id = 2,
                    ShortName = "EUR",
                    Code = 978
                }
            }));

            _mock.Setup(m => m.Delete(It.IsNotNull<CurrencyRate>())).Callback(() =>
            {
                if (_currencyRates.Any())
                {
                    _currencyRates.RemoveAt(_currencyRates.Count - 1);
                }
            });
        }

        public ICurrencyRateRepository Repository
        {
            get { return _mock.Object; }
        }
    }
}