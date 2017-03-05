﻿namespace PVT.Q1._2017.Shop.Tests
{
    #region using

    using System;
    using System.Linq;

    using global::Shop.Common.Models;
    using global::Shop.DAL;
    using global::Shop.DAL.Context;
    using global::Shop.DAL.Repositories.Infrastruture;
    using global::Shop.Infrastructure;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ninject;

    #endregion

    /// <summary>
    /// Summary description for <see cref="DataBaseTest"/>
    /// </summary>
    [TestClass]
    public class DataBaseTest
    {
        /// <summary>
        /// </summary>
        private readonly IKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseTest"/> class.
        /// </summary>
        public DataBaseTest()
        {
            this._kernel = new StandardKernel(new DefaultRepositoriesNinjectModule());
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void AddModelTest()
        {
            var trackName = "Hello";

            var repositoryFactory = this._kernel.Get<IFactory>();
            using (var repository = repositoryFactory.Create<ITrackRepository>())
            {
                // repository.AddOrUpdate(new Track { Name = trackName });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(t => t.Name == trackName).Any());
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void ArtistTracksTest()
        {
            var artist = new Artist { Name = "Sia" };

            var repositoryFactory = this._kernel.Get<IFactory>();
            using (var repository = repositoryFactory.Create<IArtistRepository>())
            {
                // repository.AddOrUpdate(artist);
            }

            var track1 = new Track { Name = "Unstoppable", ArtistId = artist.Id };
            var track2 = new Track { Name = "Alive", ArtistId = artist.Id };
            using (var repository = repositoryFactory.Create<ITrackRepository>())
            {
                // repository.AddOrUpdate(track1);
                // repository.AddOrUpdate(track2);
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(t => t.ArtistId != null).Any());
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void CreateDataBaseTest()
        {
            using (var context = new ShopContext())
            {
                if (context.Database.Exists()) context.Database.Delete();

                context.Database.Create();
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetAllModelWithExpressionTest()
        {
            var artist1Name = "Artist 1";
            var artist2Name = "Artist 2";
            var artist3Name = "Artist 3";

            var repositoryFactory = this._kernel.Get<IFactory>();
            using (var repository = repositoryFactory.Create<IArtistRepository>())
            {
                // repository.AddOrUpdate(new Artist { Name = artist1Name });
                // repository.AddOrUpdate(new Artist { Name = artist2Name });
                // repository.AddOrUpdate(new Artist { Name = artist3Name });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(a => a.Name.StartsWith("Artist")).Count >= 3);
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetAllModelWithoutExpressionTest()
        {
            var album1Name = "Album 1";
            var album2Name = "Album 2";
            var album3Name = "Album 3";

            var repositoryFactory = this._kernel.Get<IFactory>();
            using (var repository = repositoryFactory.Create<IAlbumRepository>())
            {
                // repository.AddOrUpdate(new Album { Name = album1Name });
                // repository.AddOrUpdate(new Album { Name = album2Name });
                // repository.AddOrUpdate(new Album { Name = album3Name });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Count >= 3, repository.GetAll().Count.ToString());
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void RepositoryFactoryTest()
        {
            var repositoryFactory = this._kernel.Get<IFactory>();
            Assert.IsNotNull(repositoryFactory);
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TracksWithArtistsTest()
        {
            var repositoryFactory = this._kernel.Get<IFactory>();
            using (var repository = repositoryFactory.Create<ITrackRepository>())
            {
                Assert.IsTrue(repository.GetAll(t => t.ArtistId != null).Any());
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void UpdateModelTest()
        {
            var trackName = "Super-puper track with duration";

            var repositoryFactory = this._kernel.Get<IFactory>();
            using (var repository = repositoryFactory.Create<ITrackRepository>())
            {
                // repository.AddOrUpdate(new Track { Name = trackName });
                repository.SaveChanges();
            }

            var duration = new TimeSpan(0, 2, 46);

            using (var repository = repositoryFactory.Create<ITrackRepository>())
            {
                var track = repository.GetAll(t => t.Name == trackName).FirstOrDefault();
                Assert.IsNotNull(track);

                track.Duration = duration;

                // repository.AddOrUpdate(track);
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(t => t.Duration == duration).Any());
            }

            using (var repository = repositoryFactory.Create<ITrackRepository>())
            {
                Assert.IsTrue(repository.GetAll(t => t.Duration == duration).Any());
            }
        }
    }
}