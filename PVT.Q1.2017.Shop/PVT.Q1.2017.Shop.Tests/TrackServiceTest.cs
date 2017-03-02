namespace PVT.Q1._2017.Shop.Tests
{
    using System.Linq;
    using global::Shop.BLL;
    using global::Shop.BLL.Exceptions;
    using global::Shop.BLL.Services.Infrastruture;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Repositories.Infrastruture;
    using global::Shop.Infrastructure.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ninject;

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

        #region Tests

        [TestMethod]
        public void RegisterValidTrackTest()
        {
            var artist = new Artist { Name = "Some Artist" };

            var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.CreateRepository<IArtistRepository>())
            {
                repository.AddOrUpdate(artist);
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(a => a.Name == artist.Name).Any());
            }

            var album = new Album { Name = "Some Single", Artist = artist};
            using (var repository = repositoryFactory.CreateRepository<IAlbumRepository>())
            {
                repository.AddOrUpdate(album);
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(a => a.Name == album.Name).Any());
            }

            var track = new Track { Name = "Some Track", Album = album, Artist = artist };

            var trackService = this._kernel.Get<ITrackService>();
            trackService.Register(track);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidEntityException))]
        public void RegisterInvalidTrackTest()
        {
            var trackService = this._kernel.Get<ITrackService>();
            trackService.Register(new Track());
        }

        #endregion //Tests
    }
}
