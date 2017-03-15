namespace PVT.Q1._2017.Shop.Tests
{
    using global::Shop.BLL;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;
    using System;
    using System.Linq;

    [TestClass]
    public class CurrencyRateTest
    {
        private readonly ICurrencyRateService _currencyService;
        private readonly IRepositoryFactory _factory;

        public CurrencyRateTest()
        {
            IKernel kernel = new StandardKernel(new DefaultServicesNinjectModule());
            this._factory = kernel.Get<IRepositoryFactory>();
            this._currencyService = kernel.Get<ICurrencyRateService>();
            //Initialize();
        }

        [TestMethod]
        public void GetActualRatesTest()
        {
            var rates = _currencyService.GetActualRates(DateTime.Now);
            Func<CurrencyRate, bool> firstPair = r => r.CurrencyId == 1 && r.TargetCurrencyId == 2;
            Func<CurrencyRate, bool> secondPair = r => r.CurrencyId == 2 && r.TargetCurrencyId == 1;
            Assert.IsNotNull(rates.FirstOrDefault(firstPair));
            Assert.IsNotNull(rates.FirstOrDefault(secondPair));
            Assert.AreEqual(rates.First(firstPair).CrossCourse, 0.9m);
            Assert.AreEqual(rates.First(secondPair).CrossCourse, 1.2m);

        }

        [TestMethod]
        public void GetRateByDateTest()
        {
            Assert.AreEqual(_currencyService.GetRateByDate(1, 2, DateTime.Now), 0.9m);
            Assert.AreEqual(_currencyService.GetRateByDate(2, 1, DateTime.Now), 1.2m);
        }

        private void Initialize()
        {
            using (var curRateRepo = this._factory.GetCurrencyRateRepository())
            using (var curRepo = this._factory.GetCurrencyRepository())
            {
                //add currencies
                curRepo.AddOrUpdate(new[] {
                    new Currency {
                        Id = 1,
                        ShortName = "USD",
                        Code = 1
                    },
                     new Currency {
                        Id = 2,
                        ShortName = "EUR",
                        Code = 2
                    }
                });

                curRepo.SaveChanges();

                ////add currency rates 
                curRateRepo.AddOrUpdate(new[] {
                    new CurrencyRate {
                        CurrencyId = 1,
                        TargetCurrencyId = 2,
                        Date = DateTime.Now.AddDays(-1).Date,
                        CrossCourse = 0.8m
                    },
                    new CurrencyRate {
                        CurrencyId = 1,
                        TargetCurrencyId = 2,
                        Date = DateTime.Now.Date,
                        CrossCourse = 0.9m
                    },
                    new CurrencyRate {
                        CurrencyId = 2,
                        TargetCurrencyId = 1,
                        Date = DateTime.Now.AddDays(-1).Date,
                        CrossCourse = 1.1m
                    },
                    new CurrencyRate {
                        CurrencyId = 2,
                        TargetCurrencyId = 1,
                        Date = DateTime.Now.Date,
                        CrossCourse = 1.2m
                    }
                });

                curRateRepo.SaveChanges();

            }
        }
    }
}
