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
using Shop.Common.Models.ViewModels;
using Shop.BLL.Services.Infrastructure;
using System.Collections;

namespace PVT.Q1._2017.Shop.Tests
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void CartController_ActionIndexTest()
        {
            Cart cart = new Cart { Id = 1, UserId = 1, Tracks = new List<Track>(), Albums = new List<Album>() };
            ICollection<Cart> carts = new List<Cart> { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            var cartController = new CartController(moqCartService.Object, moqRepositoryFactory.Object);
            var Result = cartController.Index(1);
            Assert.IsInstanceOfType(Result, typeof(ViewResult));
        }

        [TestMethod]
        public void CartController_AddTrackToCart()
        {
            IList<Track> tracks = new List<Track>
            {
                new Track { Id=1, Name = "Wide Awake" },
                new Track { Id=2, Name = "Be mine" },
            };

            var cart = new Cart { Id = 1, UserId = 1, Tracks = tracks, Albums = new List<Album>() };
            var addedTrack = new Track { Id = 3, Name = "Hallelujah" };

            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.AddTrack(It.Is<int>(u => u == 1), It.Is<int>(t => t == 3))).Callback(() => cart.Tracks.Add(addedTrack));
            moqCartService.Setup(m => m.AddTrack(It.Is<int>(u => u == 0), It.IsAny<int>()));
            moqCartService.Setup(m => m.AddTrack(It.IsAny<int>(), It.Is<int>(t => t == 0)));

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            var cartController = new CartController(moqCartService.Object, moqRepositoryFactory.Object);

            RedirectToRouteResult result = cartController.AddTrack(0, 3);
            Assert.IsFalse(result.Permanent);
            Assert.IsFalse(cart.Tracks.Contains(addedTrack));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(0, result.RouteValues["currentUserId"]);

            result = cartController.AddTrack(1, 0);
            Assert.IsFalse(result.Permanent);
            Assert.IsFalse(cart.Tracks.Contains(addedTrack));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);

            result = cartController.AddTrack(1, 3);
            Assert.IsFalse(result.Permanent);
            Assert.IsTrue(cart.Tracks.Contains(addedTrack));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);
        }

        [TestMethod]
         public void CartController_AddAlbumToCart()
         {
            IList<Album> albums = new List<Album>
            {
                new Album { Id=1, Name = "Gold" },
                new Album { Id=2, Name = "Platinum" },
            };

            Cart cart = new Cart { Id = 1, UserId = 1, Tracks = new List<Track>(), Albums = albums };
            var addedAlbum = new Album { Id = 3, Name = "So-So" };

            ICollection<Cart> carts = new List<Cart> { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.AddAlbum(It.Is<int>(u => u == 1), It.Is<int>(t => t == 3))).Callback(() => cart.Albums.Add(addedAlbum));
            moqCartService.Setup(m => m.AddAlbum(It.Is<int>(u => u == 0), It.IsAny<int>()));
            moqCartService.Setup(m => m.AddAlbum(It.IsAny<int>(), It.Is<int>(t => t == 0)));

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            var cartController = new CartController(moqCartService.Object, moqRepositoryFactory.Object);

            RedirectToRouteResult result = cartController.AddAlbum(0, 3);
            Assert.IsFalse(result.Permanent);
            Assert.IsFalse(cart.Albums.Contains(addedAlbum));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(0, result.RouteValues["currentUserId"]);

            result = cartController.AddAlbum(1, 0);
            Assert.IsFalse(result.Permanent);
            Assert.IsFalse(cart.Albums.Contains(addedAlbum));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);

            result = cartController.AddAlbum(1, 3);
            Assert.IsFalse(result.Permanent);
            Assert.IsTrue(cart.Albums.Contains(addedAlbum));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);
        }

        [TestMethod]
        public void CartController_DeleteTrackFromCart()
        {
            IList<Track> tracks = new List<Track>
            {
                new Track { Id=1, Name = "Wide Awake" },
                new Track { Id=2, Name = "Be mine" },
            };

            var cart = new Cart { Id = 1, UserId = 1, Tracks = tracks, Albums = new List<Album>() };
            var deletedTrack = tracks[1];

            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.RemoveTrack(It.Is<int>(u => u == 1), It.Is<int>(t => t == 2))).Callback(() => cart.Tracks.Remove(deletedTrack));
            moqCartService.Setup(m => m.RemoveTrack(It.Is<int>(u => u == 0), It.IsAny<int>()));
            moqCartService.Setup(m => m.RemoveTrack(It.IsAny<int>(), It.Is<int>(t => t == 0)));

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            var cartController = new CartController(moqCartService.Object, moqRepositoryFactory.Object);

            RedirectToRouteResult result = cartController.DeleteTrack(0, 3);
            Assert.IsFalse(result.Permanent);
            Assert.IsTrue(cart.Tracks.Contains(deletedTrack));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(0, result.RouteValues["currentUserId"]);

            result = cartController.DeleteTrack(1, 0);
            Assert.IsFalse(result.Permanent);
            Assert.IsTrue(cart.Tracks.Contains(deletedTrack));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);

            result = cartController.DeleteTrack(1, 2);
            Assert.IsFalse(result.Permanent);
            Assert.IsFalse(cart.Tracks.Contains(deletedTrack));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);
        }

        [TestMethod]
        public void CartController_DeleteAlbumFromCart()
        {
            IList<Album> albums = new List<Album>
            {
                new Album { Id=1, Name = "Gold" },
                new Album { Id=2, Name = "Platinum" },
            };

            Cart cart = new Cart { Id = 1, UserId = 1, Tracks = new List<Track>(), Albums = albums };
            var deletedAlbum = albums[1];

            ICollection<Cart> carts = new List<Cart>() { cart };

            Mock<ICartRepository> moqCartRepository = new Mock<ICartRepository>();
            moqCartRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Cart, bool>>>())).Returns(carts);
            moqCartRepository.Setup(m => m.GetById(It.Is<int>(t => t == 1))).Returns(cart);

            Mock<ICartService> moqCartService = new Mock<ICartService>();
            moqCartService.Setup(m => m.RemoveAlbum(It.Is<int>(u => u == 1), It.Is<int>(t => t == 2))).Callback(() => cart.Albums.Remove(deletedAlbum));
            moqCartService.Setup(m => m.RemoveAlbum(It.Is<int>(u => u == 0), It.IsAny<int>()));
            moqCartService.Setup(m => m.RemoveAlbum(It.IsAny<int>(), It.Is<int>(t => t == 0)));

            Mock<IRepositoryFactory> moqRepositoryFactory = new Mock<IRepositoryFactory>();
            moqRepositoryFactory.Setup(m => m.GetCartRepository()).Returns(moqCartRepository.Object);

            var cartController = new CartController(moqCartService.Object, moqRepositoryFactory.Object);

            RedirectToRouteResult result = cartController.DeleteAlbum(0, 3);
            Assert.IsFalse(result.Permanent);
            Assert.IsTrue(cart.Albums.Contains(deletedAlbum));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(0, result.RouteValues["currentUserId"]);

            result = cartController.DeleteAlbum(1, 0);
            Assert.IsFalse(result.Permanent);
            Assert.IsTrue(cart.Albums.Contains(deletedAlbum));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);

            result = cartController.DeleteAlbum(1, 2);
            Assert.IsFalse(result.Permanent);
            Assert.IsFalse(cart.Albums.Contains(deletedAlbum));
            Assert.AreEqual("Cart", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, result.RouteValues["currentUserId"]);
        }
    }
}
