using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Common.Models;
using Shop.Infrastructure.Repositories;
using Moq;
using System.Collections.Generic;
using PVT.Q1._2017.Shop.Controllers.Cart;
using PVT.Q1._2017.Shop.ViewModels;
using System;
using System.Linq.Expressions;

namespace PVT.Q1._2017.Shop.Tests
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void ActionIndexTest()
        {
            IList<Track> tracks = new List<Track>()
            {
                new Track { Id=1, Name = "Wide Awake" },
                new Track { Id=2, Name = "Be mine" },
                new Track { Id=3, Name = "Escape From Love" }
            };

            User user = new User { Id = 1 };
            Cart cart = new Cart { Id = 1, User = user, Tracks = tracks };
            ICollection<Cart> carts = new List<Cart>() { cart };
            ICollection<User> users = new List<User>() { user };

            Mock<IRepository<Cart>> moqCartRepository = new Mock<IRepository<Cart>>();
            moqCartRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(cart);
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);

            Mock<IRepository<User>> moqUserRepository = new Mock<IRepository<User>>();
            moqUserRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(user);
            moqUserRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<User, bool>>>())).Returns(users);

            var cartController = new CartController(moqCartRepository.Object, moqUserRepository.Object);
            CartView cartView = (CartView)cartController.Index(1).Model;

            Assert.IsTrue(cartView.Tracks[0].Id == 1);
            Assert.IsTrue(string.Compare(cartView.Tracks[0].Name, "Wide Awake", StringComparison.OrdinalIgnoreCase) == 0);
            Assert.IsTrue(cartView.Tracks[1].Id == 2);
            Assert.IsTrue(string.Compare(cartView.Tracks[1].Name, "Be mine", StringComparison.OrdinalIgnoreCase) == 0);
            Assert.IsTrue(cartView.Tracks[2].Id == 3);
            Assert.IsTrue(string.Compare(cartView.Tracks[2].Name, "Escape From Love", StringComparison.OrdinalIgnoreCase) == 0);
        }
    }
}
