﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PVT.Q1._2017.Shop.Tests.Moq;
using Shop.BLL;
using Shop.BLL.Services;
using Shop.BLL.Services.Infrastructure;
using Shop.Common.Models;
using Shop.DAL.Infrastruture;

namespace PVT.Q1._2017.Shop.Tests
{
	[TestClass]
	public class CartServiceTest
	{
		private readonly ICartService _cartService;
		private readonly IRepositoryFactory _factory;

		public CartServiceTest()
		{
			_factory = new RepositoryFactoryMoq();
			_cartService = new CartService(_factory);
			DefaultModelsMapper.MapModels();
		}

		[TestMethod]
		public void AddCartTest()
		{
            using (var repo = this._factory.GetCartRepository())
			{
				repo.AddOrUpdate(new Cart { UserId = 1});
				repo.SaveChanges();

				Assert.IsTrue(repo.GetAll().Any());
			}
		}

		[TestMethod]
		public void AddTrackToCart_Test()
		{
            using (var repo = _factory.GetTrackRepository())
			{
				repo.AddOrUpdate(new Track { Id = 1 });
				repo.SaveChanges();
            }
            AddCartTest();
            _cartService.AddTrack(1, 1);
            using (var repo = _factory.GetCartRepository())
            {
                var result = repo.GetAll(c => c.UserId == 1).FirstOrDefault();
                Assert.IsTrue(result != null && result.Tracks.Any(t => t.Id == 1));
            }
        }

	    [TestMethod]
	    public void AddTrackListToCart_Test()
	    {
            using (var repo = _factory.GetTrackRepository())
            {
                repo.AddOrUpdate(new Track { Id = 1 });
                repo.AddOrUpdate(new Track { Id = 2 });
                repo.AddOrUpdate(new Track { Id = 3 });
                repo.SaveChanges();
            }
            AddCartTest();
	        var trackIds = new int[] {1, 2, 3};
            _cartService.AddTrack(1, trackIds);
            using (var repo = _factory.GetCartRepository())
            {
                var result = repo.GetAll(c => c.UserId == 1).FirstOrDefault();
                Assert.IsTrue(result != null && result.Tracks.Count == 3);
            }
        }

        [TestMethod]
        public void AddAlbumToCart_Test()
        {
            using (var repo = _factory.GetAlbumRepository())
            {
                repo.AddOrUpdate(new Album { Id = 1 });
                repo.SaveChanges();
            }
            AddCartTest();
            _cartService.AddAlbum(1, 1);
            using (var repo = _factory.GetCartRepository())
            {
                var result = repo.GetAll(c => c.UserId == 1).FirstOrDefault();
                Assert.IsTrue(result != null && result.Albums.Any(a => a.Id == 1));
            }
        }

        [TestMethod]
        public void AddAlbumListToCart_Test()
        {
            using (var repo = _factory.GetAlbumRepository())
            {
                repo.AddOrUpdate(new Album { Id = 1 });
                repo.AddOrUpdate(new Album { Id = 2 });
                repo.AddOrUpdate(new Album { Id = 3 });
                repo.SaveChanges();
            }
            AddCartTest();
            var albumIds = new int[] { 1, 2, 3 };
            _cartService.AddAlbum(1, albumIds);
            using (var repo = _factory.GetCartRepository())
            {
                var result = repo.GetAll(c => c.UserId == 1).FirstOrDefault();
                Assert.IsTrue(result != null && result.Albums.Count == 3);
            }
        }

	    //TODO: добавить тесты для методов удаления треков/альбомов
    }
}
