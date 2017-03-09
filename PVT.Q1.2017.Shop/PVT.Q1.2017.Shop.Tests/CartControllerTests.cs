using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Common.Models;
using Shop.Infrastructure.Repositories;
using Moq;
using System.Collections.Generic;
using PVT.Q1._2017.Shop.Controllers.Cart;
using PVT.Q1._2017.Shop.ViewModels;
using Shop.DAL.Infrastruture;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace PVT.Q1._2017.Shop.Tests
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void CartController_ActionIndexTest()
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

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);

            Mock<IUserRepository> moqUserRepository = new Mock<IUserRepository>();
            moqUserRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(user);

            var cartController = new CartController(moqCartRepository.Object, moqUserRepository.Object);
            CartView cartView = (CartView)cartController.Index(1).Model;

            Assert.IsTrue(cartView.Tracks[0].Id == 1);
            Assert.IsTrue(string.Compare(cartView.Tracks[0].Name, "Wide Awake", StringComparison.OrdinalIgnoreCase) == 0);
            Assert.IsTrue(cartView.Tracks[1].Id == 2);
            Assert.IsTrue(string.Compare(cartView.Tracks[1].Name, "Be mine", StringComparison.OrdinalIgnoreCase) == 0);
            Assert.IsTrue(cartView.Tracks[2].Id == 3);
            Assert.IsTrue(string.Compare(cartView.Tracks[2].Name, "Escape From Love", StringComparison.OrdinalIgnoreCase) == 0);
        }

        [TestMethod]
        public void CartController_AddTrackToCart_WhenTrackIsNotNull()
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

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.AddOrUpdate(It.IsAny<Cart>()));

            Mock<IUserRepository> moqUserRepository = new Mock<IUserRepository>();
            moqUserRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(user);

            var cartController = new CartController(moqCartRepository.Object, moqUserRepository.Object);
            RedirectToRouteResult result = cartController.AddTrackToCart(1, new Track { Name = "Hallelujah" });

            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);
        }

        [TestMethod]
        public void CartController_AddTrackToCart_WhenTrackIsNull()
        {
            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            Mock<IUserRepository> moqUserRepository = new Mock<IUserRepository>();

            var cartController = new CartController(moqCartRepository.Object, moqUserRepository.Object);
            RedirectToRouteResult result = cartController.AddTrackToCart(1, null);

            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);
        }

        [TestMethod]
        public void CartController_DeleteTrackFromCart_WhenTrackIsNotNull()
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

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.AddOrUpdate(It.IsAny<Cart>()));
            moqCartRepository.Setup(m => m.Delete(It.IsAny<Cart>()));

            Mock<IUserRepository> moqUserRepository = new Mock<IUserRepository>();
            moqUserRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(user);

            var cartController = new CartController(moqCartRepository.Object, moqUserRepository.Object);
            RedirectToRouteResult result = cartController.DeleteTrackFromCart(1, new Track { Name = "Hallelujah" });

            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);
        }

        [TestMethod]
        public void CartController_DeleteTrackFromCart_WhenTrackIsNull()
        {

            Cart cart = new Cart { Id = 1, User = new User { Id = 1 }, Tracks = new List<Track>() };
            ICollection<Cart> carts = new List<Cart>() { cart };
            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);

            Mock<IUserRepository> moqUserRepository = new Mock<IUserRepository>();

            var cartController = new CartController(moqCartRepository.Object, moqUserRepository.Object);
            RedirectToRouteResult result = cartController.DeleteTrackFromCart(1, null);

            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);
        }
    }
}
