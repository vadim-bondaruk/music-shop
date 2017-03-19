using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Common.Models;
using Moq;
using System.Collections.Generic;
using PVT.Q1._2017.Shop.Controllers.Cart;
using Shop.DAL.Infrastruture;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Linq;
using Shop.Common.Models.ViewModels;

namespace PVT.Q1._2017.Shop.Tests
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void CartController_ActionIndexTest()
        {
            IList<Track> tracks = new List<Track>
            {
                new Track { Id=1, Name = "Wide Awake" },
                new Track { Id=2, Name = "Be mine" },
                new Track { Id=3, Name = "Escape From Love" }
            };
            UserData user = new UserData { Id = 1 };
            Cart cart = new Cart { Id = 1, UserId = 1, Tracks = tracks };
            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);

            Mock<IUserDataRepository> moqUserRepository = new Mock<IUserDataRepository>();
            moqUserRepository.Setup(m => m.GetById(It.Is<int>(u => u == 1))).Returns(user);

            Mock<ITrackRepository> moqTrackRepository = new Mock<ITrackRepository>();
            moqTrackRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(tracks[0]);

            var cartController = new CartController(moqCartRepository.Object, moqUserRepository.Object, moqTrackRepository.Object);
            CartViewModel cartView = (CartViewModel)cartController.Index(1).Model;

            Assert.IsTrue(cartView.Tracks[0].Id == 1);
            Assert.IsTrue(string.Compare(cartView.Tracks[0].Name, "Wide Awake", StringComparison.OrdinalIgnoreCase) == 0);
            Assert.IsTrue(cartView.Tracks[1].Id == 2);
            Assert.IsTrue(string.Compare(cartView.Tracks[1].Name, "Be mine", StringComparison.OrdinalIgnoreCase) == 0);
            Assert.IsTrue(cartView.Tracks[2].Id == 3);
            Assert.IsTrue(string.Compare(cartView.Tracks[2].Name, "Escape From Love", StringComparison.OrdinalIgnoreCase) == 0);
        }

        [TestMethod]
        public void CartController_AddTrackToCart_WhenTrackIdIsNotZero()
        {
            IList<Track> tracks = new List<Track>
            {
                new Track { Id=1, Name = "Wide Awake" },
                new Track { Id=2, Name = "Be mine" },
                new Track { Id=3, Name = "Escape From Love" }
            };

            var user = new UserData { Id = 1 };
            var cart = new Cart { Id = 1, UserId = 1, Tracks = tracks };
            var addedTrack = new Track { Id = 4, Name = "Hallelujah" };
            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.AddOrUpdate(It.IsAny<Cart>()));

            Mock<IUserDataRepository> moqUserRepository = new Mock<IUserDataRepository>();
            moqUserRepository.Setup(m => m.GetById(It.Is<int>(u => u == 1))).Returns(user);

            Mock<ITrackRepository> moqTrackRepository = new Mock<ITrackRepository>();
            moqTrackRepository.Setup(m => m.GetById(It.Is<int>(t => t == 4))).Returns(addedTrack);

            var cartController = new CartController(moqCartRepository.Object, moqUserRepository.Object, moqTrackRepository.Object);
            RedirectToRouteResult result = cartController.AddTrack(1, 4);

            Assert.IsFalse(result.Permanent);
            Assert.IsTrue(cart.Tracks.Contains(addedTrack));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);
        }

        [TestMethod]
        public void CartController_AddTrackToCart_WhenTrackIdIsZero()
        {
            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            Mock<IUserDataRepository> moqUserRepository = new Mock<IUserDataRepository>();

            Mock<ITrackRepository> moqTrackRepository = new Mock<ITrackRepository>();
            moqTrackRepository.Setup(m => m.GetById(It.Is<int>(Id => Id == 0))).Returns((Track)null);

            var cartController = new CartController(moqCartRepository.Object, moqUserRepository.Object, moqTrackRepository.Object);
            RedirectToRouteResult result = cartController.AddTrack(1, 0);

            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);
        }

        [TestMethod]
        public void CartController_DeleteTrackFromCart_WhenTrackIsNotZero()
        {
            IList<Track> tracks = new List<Track>
            {
                new Track { Id=1, Name = "Wide Awake" },
                new Track { Id=2, Name = "Be mine" },
            };

            var user = new UserData { Id = 1 };
            var cart = new Cart { Id = 1, UserId = 1, Tracks = tracks };
            var deletedTrack = cart.Tracks.FirstOrDefault(t => t.Id == 1);
            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.AddOrUpdate(It.IsAny<Cart>()));
            moqCartRepository.Setup(m => m.Delete(It.IsAny<Cart>())).Callback(() => cart = null);

            Mock<IUserDataRepository> moqUserRepository = new Mock<IUserDataRepository>();
            moqUserRepository.Setup(m => m.GetById(It.Is<int>(u => u == 1))).Returns(user);

            Mock<ITrackRepository> moqTrackRepository = new Mock<ITrackRepository>();
            moqTrackRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(tracks[0]);

            var cartController = new CartController(moqCartRepository.Object, moqUserRepository.Object, moqTrackRepository.Object);
            RedirectToRouteResult result = cartController.DeleteTrack(1, 1);

            Assert.IsFalse(result.Permanent);
            Assert.IsFalse(cart.Tracks.Contains(deletedTrack));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);

            // если в корзине была только одна песня и она удаляется, то удаляется и сама корзина
            result = cartController.DeleteTrack(1, 2);
            Assert.IsNull(cart);
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);
        }

        [TestMethod]
        public void CartController_DeleteTrackFromCart_WhenTrackIsZero()
        {
            Cart cart = new Cart
            {
                Id = 1,
                UserId = 1,
                Tracks = new List<Track>()
            };
            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);

            Mock<IUserDataRepository> moqUserRepository = new Mock<IUserDataRepository>();

            Mock<ITrackRepository> moqTrackRepository = new Mock<ITrackRepository>();
            moqTrackRepository.Setup(m => m.GetById(It.Is<int>(t => t == 0))).Returns(new Track());

            var cartController = new CartController(moqCartRepository.Object, moqUserRepository.Object, moqTrackRepository.Object);
            RedirectToRouteResult result = cartController.DeleteTrack(1, 0);

            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);
        }
    }
}
