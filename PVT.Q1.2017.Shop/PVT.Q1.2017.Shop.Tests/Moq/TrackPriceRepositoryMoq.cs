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

    public class TrackPriceRepositoryMoq
    {
        private readonly Mock<ITrackPriceRepository> _mock;
        private readonly List<TrackPrice> _trackPrices = new List<TrackPrice>();

        public TrackPriceRepositoryMoq()
        {
            _mock = new Mock<ITrackPriceRepository>();

            _mock.Setup(m => m.GetAll()).Returns(_trackPrices);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<TrackPrice, BaseEntity>>[]>()))
                .Returns(_trackPrices);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<TrackPrice, bool>>>()))
                .Returns(_trackPrices);

            _mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<TrackPrice, bool>>>(),
                                    It.Is<Expression<Func<TrackPrice, BaseEntity>>[]>(x => x != null)))
                .Returns(_trackPrices);

            _mock.Setup(m => m.GetById(It.IsInRange(1, _trackPrices.Count, Range.Inclusive)))
                .Returns(_trackPrices.FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, _trackPrices.Count))))
                .Returns(() => null);

            _mock.Setup(m => m.GetById(It.IsInRange(1, _trackPrices.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<TrackPrice, BaseEntity>>[]>()))
                .Returns(_trackPrices.FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, _trackPrices.Count)),
                                      It.IsAny<Expression<Func<TrackPrice, BaseEntity>>[]>()))
                .Returns(() => null);

            _mock.Setup(m => m.AddOrUpdate(It.IsNotNull<TrackPrice>())).Callback(() => _trackPrices.Add(new TrackPrice
            {
                Id = _trackPrices.Count + 1,
                Price = _trackPrices.Count + 1.49m
            }));

            _mock.Setup(m => m.Delete(It.IsNotNull<TrackPrice>())).Callback(() =>
            {
                if (_trackPrices.Any())
                {
                    _trackPrices.RemoveAt(_trackPrices.Count - 1);
                }
            });
        }

        public ITrackPriceRepository Repository
        {
            get { return _mock.Object; }
        }
    }
}