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

    class PurchasedAlbumRepositoryMoq
    {
        private readonly List<PurchasedAlbum> _purchasedAlbum = new List<PurchasedAlbum>();
        private readonly Mock<IPurchasedAlbumRepository> _mock;

        public PurchasedAlbumRepositoryMoq()
        {
            _mock = new Mock<IPurchasedAlbumRepository>();

            _mock.Setup(m => m.GetAll()).Returns(_purchasedAlbum);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<PurchasedAlbum, BaseEntity>>[]>()))
                 .Returns(_purchasedAlbum);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<PurchasedAlbum, bool>>>()))
                 .Returns(_purchasedAlbum);

            _mock.Setup(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<PurchasedAlbum, bool>>>(),
                                     It.IsAny<Expression<Func<PurchasedAlbum, BaseEntity>>[]>()))
                 .Returns(_purchasedAlbum);

            _mock.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<PurchasedAlbum, bool>>>()))
                 .Returns(() => _purchasedAlbum.FirstOrDefault());

            _mock.Setup(
                        m =>
                            m.FirstOrDefault(It.IsAny<Expression<Func<PurchasedAlbum, bool>>>(),
                                             It.IsAny<Expression<Func<PurchasedAlbum, BaseEntity>>[]>()))
                 .Returns(() => _purchasedAlbum.FirstOrDefault());

            _mock.Setup(m => m.GetById(It.IsAny<int>()))
                 .Returns(() => _purchasedAlbum.FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.GetById(It.IsAny<int>(),
                                       It.IsAny<Expression<Func<PurchasedAlbum, BaseEntity>>[]>()))
                 .Returns(() => _purchasedAlbum.FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.Exist(It.IsAny<Expression<Func<PurchasedAlbum, bool>>>()))
                 .Returns(() => _purchasedAlbum.Any());

            _mock.Setup(m => m.AddOrUpdate(It.IsNotNull<PurchasedAlbum>())).Callback(() => _purchasedAlbum.Add(new PurchasedAlbum
            {
                Id = _purchasedAlbum.Count + 1
            }));

            _mock.Setup(m => m.Delete(It.IsNotNull<PurchasedAlbum>())).Callback(() =>
            {
                if (_purchasedAlbum.Any())
                {
                    _purchasedAlbum.RemoveAt(_purchasedAlbum.Count - 1);
                }
            });
        }

        public IPurchasedAlbumRepository Repository
        {
            get { return _mock.Object; }
        }
    }
}
