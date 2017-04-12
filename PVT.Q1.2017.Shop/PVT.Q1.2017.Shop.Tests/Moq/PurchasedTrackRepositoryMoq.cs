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

    class PurchasedTrackRepositoryMoq
    {
        private readonly List<PurchasedTrack> _purchasedTrack = new List<PurchasedTrack>();
        private readonly Mock<IPurchasedTrackRepository> _mock;

        public PurchasedTrackRepositoryMoq()
        {
            _mock = new Mock<IPurchasedTrackRepository>();

            _mock.Setup(m => m.GetAll()).Returns(_purchasedTrack);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<PurchasedTrack, BaseEntity>>[]>()))
                 .Returns(_purchasedTrack);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<PurchasedTrack, bool>>>()))
                 .Returns(_purchasedTrack);

            _mock.Setup(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<PurchasedTrack, bool>>>(),
                                     It.IsAny<Expression<Func<PurchasedTrack, BaseEntity>>[]>()))
                 .Returns(_purchasedTrack);

            _mock.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<PurchasedTrack, bool>>>()))
                 .Returns(() => _purchasedTrack.FirstOrDefault());

            _mock.Setup(
                        m =>
                            m.FirstOrDefault(It.IsAny<Expression<Func<PurchasedTrack, bool>>>(),
                                             It.IsAny<Expression<Func<PurchasedTrack, BaseEntity>>[]>()))
                 .Returns(() => _purchasedTrack.FirstOrDefault());

            _mock.Setup(m => m.GetById(It.IsAny<int>()))
                 .Returns(() => _purchasedTrack.FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.GetById(It.IsAny<int>(),
                                       It.IsAny<Expression<Func<PurchasedTrack, BaseEntity>>[]>()))
                 .Returns(() => _purchasedTrack.FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.AddOrUpdate(It.IsNotNull<PurchasedTrack>())).Callback(() => _purchasedTrack.Add(new PurchasedTrack
            {
                Id = _purchasedTrack.Count + 1
            }));

            _mock.Setup(m => m.Delete(It.IsNotNull<PurchasedTrack>())).Callback(() =>
            {
                if (_purchasedTrack.Any())
                {
                    _purchasedTrack.RemoveAt(_purchasedTrack.Count - 1);
                }
            });
        }

        public IPurchasedTrackRepository Repository
        {
            get { return _mock.Object; }
        }
    }
}
