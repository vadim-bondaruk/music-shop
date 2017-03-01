using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Shop.BLL;
using Shop.BLL.Exceptions;
using Shop.BLL.Services;
using Shop.Common.Models;
using Shop.DAL;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Services;

namespace PVT.Q1._2017.Shop.Tests
{
    /// <summary>
    /// Summary description for TrackServiceTest
    /// </summary>
    [TestClass]
    public class TrackServiceTest
    {
        #region Fields

        private IKernel _kernel;

        #endregion //Fields

        #region Constructors

        public TrackServiceTest()
        {
            this._kernel = new StandardKernel(new DefaultServicesNinjectModule());
        }

        #endregion //Constructors

        [TestMethod]
        public void RegisterValidTrackTest()
        {
            var artist = new Artist { Name = "Adele" };

            var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.CreateRepository<Artist>())
            {
                repository.AddOrUpdate(artist);
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(a => a.Name == artist.Name).Any());
            }

            var album = new Album { Name = "Some Single" };
            using (var repository = repositoryFactory.CreateRepository<Album>())
            {
                repository.AddOrUpdate(album);
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(a => a.Name == album.Name).Any());
            }

            var currency = new Currency { Code = 840, ShortName = "$", FullName = "USD" };
            using (var repository = repositoryFactory.CreateRepository<Currency>())
            {
                repository.AddOrUpdate(currency);
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(c => c.Code == currency.Code).Any());
            }

            var track = new Track { Name = "Hello", Album = album, Artist = artist };

            var trackService = this._kernel.Get<IService<Track>>();
            trackService.Register(track);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidEntityException))]
        public void RegisterInvalidTrackTest()
        {
            var trackService = this._kernel.Get<IService<Track>>();
            trackService.Register(new Track());
        }
    }
}
