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

    public class CartRepositoryMoq
    {
        private readonly List<Cart> _carts = new List<Cart>();
        private readonly Mock<ICartRepository> _mock;

        public CartRepositoryMoq()
        {
            _mock = new Mock<ICartRepository>();

            _mock.Setup(m => m.GetAll()).Returns(_carts);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, BaseEntity>>[]>()))
                 .Returns(_carts);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>()))
                 .Returns(_carts);

            _mock.Setup(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>(),
                                     It.IsAny<Expression<Func<Cart, BaseEntity>>[]>()))
                 .Returns(_carts);

            _mock.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Cart, bool>>>()))
                 .Returns((Func<Cart, bool> exp) => _carts.FirstOrDefault(exp));

            _mock.Setup(
                        m =>
                            m.FirstOrDefault(It.IsAny<Expression<Func<Cart, bool>>>(),
                                             It.IsAny<Expression<Func<Cart, BaseEntity>>[]>()))
                 .Returns(() => _carts.FirstOrDefault());

            _mock.Setup(m => m.GetById(It.IsAny<int>()))
                 .Returns((int id) => _carts.FirstOrDefault(c => c.Id == id));

            _mock.Setup(m => m.GetById(It.IsAny<int>(),
                                       It.IsAny<Expression<Func<Cart, BaseEntity>>[]>()))
                 .Returns(() => _carts.FirstOrDefault(c => c.Id > 0));

            _mock.Setup(m => m.GetByUserId(It.IsAny<int>()))
                .Returns((int id) => _carts.FirstOrDefault(c => c.UserId == id));

            _mock.Setup(m => m.Exist(It.IsAny<Expression<Func<Cart, bool>>>()))
                 .Returns((Func<Cart, bool> exp) => _carts.Any(exp));

            _mock.Setup(m => m.Count(It.IsAny<Expression<Func<Cart, bool>>>()))
                 .Returns(() => _carts.Count);

            _mock.Setup(m => m.AddOrUpdate(It.IsNotNull<Cart>())).Callback((Cart cart) =>
            {
                cart.Id = _carts.Count + 1;
                _carts.Add(cart);
            });

            _mock.Setup(m => m.Delete(It.IsNotNull<Cart>())).Callback((Cart cart) =>
            {
                if (_carts.Any(o => o.Id == cart.Id))
                {
                    _carts.RemoveAt(cart.Id - 1);
                }
            });
        }

        public ICartRepository Repository
        {
            get { return _mock.Object; }
        }
    }
}