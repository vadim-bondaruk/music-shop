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
            _mock = new Mock<ICurrencyRepository>();

            _mock.Setup(m => m.GetAll()).Returns(_currencies);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()))
                .Returns(_currencies);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Currency, bool>>>()))
                .Returns(_currencies);

            _mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<Currency, bool>>>(),
                                    It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()))
                .Returns(_currencies);

            _mock.Setup(m => m.GetById(It.IsInRange(1, _currencies.Count, Range.Inclusive)))
                .Returns(_currencies.FirstOrDefault(a => a.Id >= 1));

            _mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, _currencies.Count))))
                .Returns(() => null);

            _mock.Setup(m => m.GetById(It.IsInRange(1, _currencies.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()))
                .Returns(_currencies.FirstOrDefault(a => a.Id >= 1));

            _mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, _currencies.Count)),
                                      It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()))
                .Returns(() => null);

            _mock.Setup(m => m.AddOrUpdate(It.IsNotNull<Currency>())).Callback(() => _currencies.Add(new Currency
            {
                Id = _currencies.Count + 1,
                Code = _currencies.Count + 100,
                ShortName = $"CR{ _currencies.Count + 1 }"
            }));

            _mock.Setup(m => m.Delete(It.IsNotNull<Currency>())).Callback(() =>
            {
                if (_currencies.Any())
                {
                    _currencies.RemoveAt(_currencies.Count - 1);
                }
            });
        }

        public ICurrencyRepository Repository
        {
            get { return _mock.Object; }
        }
    }
}