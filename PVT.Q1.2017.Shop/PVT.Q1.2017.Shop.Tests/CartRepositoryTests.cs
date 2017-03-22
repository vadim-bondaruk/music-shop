using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shop.Common.Models;
using Shop.Infrastructure.Repositories;

namespace PVT.Q1._2017.Shop.Tests
{
    [TestClass]
    public class CartRepositoryTests
    {
        [TestMethod]
        public void GetById()
        {
            var mock = new Mock<IRepository<Cart>>();
            mock.Setup(o => o.GetById(1)).Returns(new Cart() { Id = 1 });
            var res = mock.Object.GetById(1);
            Assert.IsNotNull(res);
        }


        [TestMethod]
        public void GetAll()
        {
            var mock = new Mock<IRepository<Cart>>();
            mock.Setup(o => o.GetAll()).Returns(new List<Cart>()
            {
                new Cart() { Id = 1 },
                new Cart() { Id = 2 },
                new Cart() { Id = 3 }
            });
            var res = mock.Object.GetAll();
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count == 3);
        }

        // TODO: Add other tests
    }
}
