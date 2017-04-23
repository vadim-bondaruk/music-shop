using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Common.Models;
using Moq;
using System.Collections.Generic;
using PVT.Q1._2017.Shop.Areas.Payment.Controllers;
using Shop.DAL.Infrastruture;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Linq;
using Shop.BLL.Services.Infrastructure;

namespace PVT.Q1._2017.Shop.Tests
{
    using global::Shop.BLL.Exceptions;
    using global::Shop.BLL.Utils;
    using global::Shop.Common.ViewModels;
    using Moq;
    using System.Web;
    using System.Web.Routing;

    [TestClass]
    public class CartControllerTests
    {
        public Mock<HttpContextBase> TestInitialize()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var session = new HttpSessionMock();
            var currentUser = new CurrentUser(new User(), 1, 1);
            context
                .Setup(c => c.Request)
                .Returns(request.Object);
            context
                .Setup(c => c.User)
                .Returns(currentUser);
            context
                .Setup(c => c.Session)
                .Returns(session);
            return context;
        }

        [TestMethod]
        public void CartController_ActionIndexTest()
        {
            Cart cart = new Cart { Id = 1, UserId = 1 };
            ICollection<Cart> carts = new List<Cart> { new Cart { Id = 1, UserId = 1 } };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(() => carts.FirstOrDefault());
            moqCartRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.GetOrderTracks(It.IsAny<int>(), It.IsAny<Nullable<int>>())).Returns(new List<TrackDetailsViewModel>());
            moqCartService.Setup(m => m.GetOrderAlbums(It.IsAny<int>(), It.IsAny<Nullable<int>>())).Returns(new List<AlbumDetailsViewModel>());

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            Mock<IServiceFactory> moqServiceFactory = new Mock<IServiceFactory>();
            moqServiceFactory.Setup(m => m.GetCartService()).Returns(moqCartService.Object);

            var cartController = new CartController(moqRepositoryFactory.Object, moqServiceFactory.Object);
            var context = TestInitialize();
            cartController.ControllerContext = new ControllerContext(context.Object, new RouteData(), cartController);
            var result = cartController.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CartController_AddTrackToCart()
        {
            var addedTrack = new Track { Id = 3, Name = "Hallelujah" };
            var addedOrderTrack = new OrderTrack { CartId = 1, Track = addedTrack };

            IList<OrderTrack> tracks = new List<OrderTrack>
            {
                new OrderTrack { CartId = 1, Track = new Track { Id = 1, Name = "Wide Awake" } },
                new OrderTrack { CartId = 1, Track = new Track { Id = 1, Name = "Be mine" } },
            };

            var cart = new Cart { Id = 1, UserId = 1, OrderTracks = tracks, OrderAlbums = new List<OrderAlbum>() };

            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.AddTrack(It.Is<int>(u => u == 1), It.Is<int>(t => t == 3))).Callback(() => cart.OrderTracks.Add(addedOrderTrack));
            moqCartService.Setup(m => m.AddTrack(It.Is<int>(u => u == 0), It.IsAny<int>()));
            moqCartService.Setup(m => m.AddTrack(It.IsAny<int>(), It.Is<int>(t => t == 0))).Throws<InvalidTrackIdException>();

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            Mock<IServiceFactory> moqServiceFactory = new Mock<IServiceFactory>();
            moqServiceFactory.Setup(m => m.GetCartService()).Returns(moqCartService.Object);

            var cartController = new CartController(moqRepositoryFactory.Object, moqServiceFactory.Object);
            var context = TestInitialize();
            cartController.ControllerContext = new ControllerContext(context.Object, new RouteData(), cartController);

            var result = (RedirectToRouteResult)cartController.AddTrack(3);
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);

            var result1 = cartController.AddTrack(0);
            Assert.IsInstanceOfType(result1, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void CartController_AddAlbumToCart()
        {
            var addedAlbum = new Album { Id = 3, Name = "So-So" };
            var addedOrderAlbum = new OrderAlbum { CartId = 1, Album = addedAlbum };

            IList<OrderAlbum> albums = new List<OrderAlbum>
            {
                new OrderAlbum { CartId=1, Album = new Album { Id = 1, Name = "Gold" } },
                new OrderAlbum { CartId=1, Album  = new Album { Id =2, Name = "Platinum" } },
            };

            Cart cart = new Cart { Id = 1, UserId = 1, OrderTracks = new List<OrderTrack>(), OrderAlbums = albums };

            ICollection<Cart> carts = new List<Cart> { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.AddAlbum(It.Is<int>(u => u == 1), It.Is<int>(t => t == 3))).Callback(() => cart.OrderAlbums.Add(addedOrderAlbum));
            moqCartService.Setup(m => m.AddAlbum(It.Is<int>(u => u == 0), It.IsAny<int>()));
            moqCartService.Setup(m => m.AddAlbum(It.IsAny<int>(), It.Is<int>(t => t == 0))).Throws<InvalidAlbumIdException>();

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            Mock<IServiceFactory> moqServiceFactory = new Mock<IServiceFactory>();
            moqServiceFactory.Setup(m => m.GetCartService()).Returns(moqCartService.Object);

            var cartController = new CartController(moqRepositoryFactory.Object, moqServiceFactory.Object);
            var context = TestInitialize();
            cartController.ControllerContext = new ControllerContext(context.Object, new RouteData(), cartController);

            var result = (RedirectToRouteResult)cartController.AddAlbum(3);
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);

            var result1 = cartController.AddAlbum(0);
            Assert.IsInstanceOfType(result1, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void CartController_DeleteTrackFromCart()
        {
            IList<OrderTrack> tracks = new List<OrderTrack>
            {
                new OrderTrack { CartId = 1, Track = new Track { Id = 1, Name = "Wide Awake" } },
                new OrderTrack { CartId = 1, Track = new Track { Id = 1, Name = "Be mine" } },
            };

            var cart = new Cart { Id = 1, UserId = 1, OrderTracks = tracks, OrderAlbums = new List<OrderAlbum>() };
            var deletedTrack = tracks[1];

            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.RemoveTrack(It.Is<int>(u => u == 1), It.Is<int>(t => t == 2))).Callback(() => cart.OrderTracks.Remove(deletedTrack));
            moqCartService.Setup(m => m.RemoveTrack(It.Is<int>(u => u == 0), It.IsAny<int>()));
            moqCartService.Setup(m => m.RemoveTrack(It.IsAny<int>(), It.Is<int>(t => t == 0))).Throws<InvalidTrackIdException>();

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            Mock<IServiceFactory> moqServiceFactory = new Mock<IServiceFactory>();
            moqServiceFactory.Setup(m => m.GetCartService()).Returns(moqCartService.Object);

            var cartController = new CartController(moqRepositoryFactory.Object, moqServiceFactory.Object);
            var context = TestInitialize();
            cartController.ControllerContext = new ControllerContext(context.Object, new RouteData(), cartController);

            var result = (RedirectToRouteResult)cartController.DeleteTrack(3);
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);

            var result1 = cartController.DeleteTrack(0);
            Assert.IsInstanceOfType(result1, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void CartController_DeleteAlbumFromCart()
        {
            IList<OrderAlbum> albums = new List<OrderAlbum>
            {
                new OrderAlbum { CartId=1, Album = new Album { Id = 1, Name = "Gold" } },
                new OrderAlbum { CartId=1, Album  = new Album { Id =2, Name = "Platinum" } },
            };

            Cart cart = new Cart { Id = 1, UserId = 1, OrderTracks = new List<OrderTrack>(), OrderAlbums = albums };
            var deletedAlbum = albums[1];

            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.RemoveAlbum(It.Is<int>(u => u == 1), It.Is<int>(t => t == 2))).Callback(() => cart.OrderAlbums.Remove(deletedAlbum));
            moqCartService.Setup(m => m.RemoveAlbum(It.Is<int>(u => u == 0), It.IsAny<int>()));
            moqCartService.Setup(m => m.RemoveAlbum(It.IsAny<int>(), It.Is<int>(t => t == 0))).Throws<InvalidAlbumIdException>();

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            Mock<IServiceFactory> moqServiceFactory = new Mock<IServiceFactory>();
            moqServiceFactory.Setup(m => m.GetCartService()).Returns(moqCartService.Object);

            var cartController = new CartController(moqRepositoryFactory.Object, moqServiceFactory.Object);
            var context = TestInitialize();
            cartController.ControllerContext = new ControllerContext(context.Object, new RouteData(), cartController);

            var result = (RedirectToRouteResult)cartController.DeleteAlbum(3);
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);

            var result1 = cartController.DeleteAlbum(0);
            Assert.IsInstanceOfType(result1, typeof(HttpNotFoundResult));
        }
        [TestMethod]
        public void CartController_GetOrdersCount()
        {
            IList<OrderTrack> tracks = new List<OrderTrack>
            {
                new OrderTrack { CartId = 1, Track = new Track { Id = 1, Name = "Wide Awake" } },
                new OrderTrack { CartId = 1, Track = new Track { Id = 1, Name = "Be mine" } },
            };

            IList<OrderAlbum> albums = new List<OrderAlbum>
            {
                new OrderAlbum { CartId=1, Album = new Album { Id = 1, Name = "Gold" } },
                new OrderAlbum { CartId=1, Album  = new Album { Id =2, Name = "Platinum" } },
            };

            var cart = new Cart { Id = 1, UserId = 1, OrderTracks = tracks, OrderAlbums = albums };
            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(cart);
            moqCartRepository.Setup(m => m.GetByUserId(It.IsAny<int>())).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.RemoveAlbum(It.Is<int>(u => u == 0), It.IsAny<int>()));
            moqCartService.Setup(m => m.RemoveAlbum(It.IsAny<int>(), It.Is<int>(t => t == 0))).Throws<InvalidAlbumIdException>();

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            Mock<IServiceFactory> moqServiceFactory = new Mock<IServiceFactory>();
            moqServiceFactory.Setup(m => m.GetCartService()).Returns(moqCartService.Object);

            var cartController = new CartController(moqRepositoryFactory.Object, moqServiceFactory.Object);
            var context = TestInitialize();
            cartController.ControllerContext = new ControllerContext(context.Object, new RouteData(), cartController);
            var result = cartController.GetOrdersCount();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
    }
}
