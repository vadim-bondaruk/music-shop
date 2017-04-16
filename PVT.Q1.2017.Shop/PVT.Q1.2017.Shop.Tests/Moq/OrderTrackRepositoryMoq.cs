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

    class OrderTrackRepositoryMoq
    {
        private readonly List<OrderTrack> _orderTrack = new List<OrderTrack>();
        private readonly Mock<IOrderTrackRepository> _mock;

        public OrderTrackRepositoryMoq()
        {
            _mock = new Mock<IOrderTrackRepository>();

            _mock.Setup(m => m.GetAll()).Returns(_orderTrack);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<OrderTrack, BaseEntity>>[]>()))
                 .Returns(_orderTrack);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<OrderTrack, bool>>>()))
                 .Returns(_orderTrack);

            _mock.Setup(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<OrderTrack, bool>>>(),
                                     It.IsAny<Expression<Func<OrderTrack, BaseEntity>>[]>()))
                 .Returns(_orderTrack);

            _mock.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<OrderTrack, bool>>>()))
                 .Returns(() => _orderTrack.FirstOrDefault());

            _mock.Setup(
                        m =>
                            m.FirstOrDefault(It.IsAny<Expression<Func<OrderTrack, bool>>>(),
                                             It.IsAny<Expression<Func<OrderTrack, BaseEntity>>[]>()))
                 .Returns(() => _orderTrack.FirstOrDefault());

            _mock.Setup(m => m.GetById(It.IsAny<int>()))
                 .Returns(() => _orderTrack.FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.GetById(It.IsAny<int>(),
                                       It.IsAny<Expression<Func<OrderTrack, BaseEntity>>[]>()))
                 .Returns(() => _orderTrack.FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.Exist(It.IsAny<Expression<Func<OrderTrack, bool>>>()))
                 .Returns(() => _orderTrack.Any());

            _mock.Setup(m => m.AddOrUpdate(It.IsNotNull<OrderTrack>())).Callback(() => _orderTrack.Add(new OrderTrack
            {
                Id = _orderTrack.Count + 1
            }));

            _mock.Setup(m => m.Delete(It.IsNotNull<OrderTrack>())).Callback(() =>
            {
                if (_orderTrack.Any())
                {
                    _orderTrack.RemoveAt(_orderTrack.Count - 1);
                }
            });
        }

        public IOrderTrackRepository Repository
        {
            get { return _mock.Object; }
        }
    }
}
