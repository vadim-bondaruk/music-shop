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
using System.Collections;

namespace PVT.Q1._2017.Shop.Tests
{
    using BLL.Utils;
    using Controllers;
    using global::Shop.BLL.Exceptions;
    using global::Shop.Common.ViewModels;

    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void CartController_ActionIndexTest()
        {
            Cart cart = new Cart { Id = 1, UserId = 1};
            ICollection<Cart> carts = new List<Cart> { new Cart { Id = 1, UserId = 1 } };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(() => carts.FirstOrDefault());
            moqCartRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            var cartController = new CartController(moqCartService.Object, moqRepositoryFactory.Object);
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

            var cart = new Cart { Id = 1, UserId = 1, Tracks = tracks, Albums = new List<OrderAlbum>() };

            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.AddTrack(It.Is<int>(u => u == 1), It.Is<int>(t => t == 3))).Callback(() => cart.Tracks.Add(addedOrderTrack));
            moqCartService.Setup(m => m.AddTrack(It.Is<int>(u => u == 0), It.IsAny<int>()));
            moqCartService.Setup(m => m.AddTrack(It.IsAny<int>(), It.Is<int>(t => t == 0))).Throws<InvalidTrackIdException>();

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            var cartController = new CartController(moqCartService.Object, moqRepositoryFactory.Object);

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

            Cart cart = new Cart { Id = 1, UserId = 1, Tracks = new List<OrderTrack>(), Albums = albums };

            ICollection<Cart> carts = new List<Cart> { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.AddAlbum(It.Is<int>(u => u == 1), It.Is<int>(t => t == 3))).Callback(() => cart.Albums.Add(addedOrderAlbum));
            moqCartService.Setup(m => m.AddAlbum(It.Is<int>(u => u == 0), It.IsAny<int>()));
            moqCartService.Setup(m => m.AddAlbum(It.IsAny<int>(), It.Is<int>(t => t == 0))).Throws<InvalidAlbumIdException>(); 

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            var cartController = new CartController(moqCartService.Object, moqRepositoryFactory.Object);

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

            var cart = new Cart { Id = 1, UserId = 1, Tracks = tracks, Albums = new List<OrderAlbum>() };
            var deletedTrack = tracks[1];

            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.RemoveTrack(It.Is<int>(u => u == 1), It.Is<int>(t => t == 2))).Callback(() => cart.Tracks.Remove(deletedTrack));
            moqCartService.Setup(m => m.RemoveTrack(It.Is<int>(u => u == 0), It.IsAny<int>()));
            moqCartService.Setup(m => m.RemoveTrack(It.IsAny<int>(), It.Is<int>(t => t == 0))).Throws<InvalidTrackIdException>(); 

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            var cartController = new CartController(moqCartService.Object, moqRepositoryFactory.Object);

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

            Cart cart = new Cart { Id = 1, UserId = 1, Tracks = new List<OrderTrack>(), Albums = albums };
            var deletedAlbum = albums[1];

            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.RemoveAlbum(It.Is<int>(u => u == 1), It.Is<int>(t => t == 2))).Callback(() => cart.Albums.Remove(deletedAlbum));
            moqCartService.Setup(m => m.RemoveAlbum(It.Is<int>(u => u == 0), It.IsAny<int>()));
            moqCartService.Setup(m => m.RemoveAlbum(It.IsAny<int>(), It.Is<int>(t => t == 0))).Throws<InvalidAlbumIdException>();

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            var cartController = new CartController(moqCartService.Object, moqRepositoryFactory.Object);

            var result = (RedirectToRouteResult)cartController.DeleteAlbum(3);
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);

            var result1 = cartController.DeleteAlbum(0);
            Assert.IsInstanceOfType(result1, typeof(HttpNotFoundResult));
        }
    }
}
