using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Shop.Common.Models;
using Shop.DAL;
using Shop.DAL.Context;
using Shop.DAL.Repositories;
using Shop.Infrastructure.Repositories;

namespace PVT.Q1._2017.Shop.Tests
{
    /// <summary>
    /// Summary description for DataBaseTest
    /// </summary>
    [TestClass]
    public class DataBaseTest
    {
        #region Fields

        private IKernel _kernel;

        #endregion //Fields

        public DataBaseTest()
        {
            this._kernel = new StandardKernel(new DalNinjectModule());
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CreateDataBaseTest()
        {
            using (var context = new ShopContext())
            {
                if (context.Database.Exists())
                {
                    context.Database.Delete();
                }

                context.Database.Create();
            }
        }

        [TestMethod]
        public void RepositoryFactoryTest()
        {
            var repositoryFactory = _kernel.Get<IRepositoryFactory>();
            Assert.IsNotNull(repositoryFactory);
        }

        [TestMethod]
        public void AddModelTest()
        {
            string trackName = "Hello";

            var repositoryFactory = _kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.CreateTrackRepository())
            {
                repository.AddOrUpdate(new Track { Name = trackName });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(t => t.Name == trackName).Any());
            }
        }

        [TestMethod]
        public void GetAllModelWithExpressionTest()
        {
            string artist1Name = "Artist 1";
            string artist2Name = "Artist 2";
            string artist3Name = "Artist 3";

            var repositoryFactory = _kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.CreateArtistRepository())
            {
                repository.AddOrUpdate(new Artist { Name = artist1Name });
                repository.AddOrUpdate(new Artist { Name = artist2Name });
                repository.AddOrUpdate(new Artist { Name = artist3Name });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(a => a.Name.StartsWith("Artist")).Count >= 3);
            }
        }

        [TestMethod]
        public void GetAllModelWithoutExpressionTest()
        {
            string album1Name = "Album 1";
            string album2Name = "Album 2";
            string album3Name = "Album 3";

            var repositoryFactory = _kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.CreateAlbumRepository())
            {
                repository.AddOrUpdate(new Album { Name = album1Name });
                repository.AddOrUpdate(new Album { Name = album2Name });
                repository.AddOrUpdate(new Album { Name = album3Name });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Count >= 3, repository.GetAll().Count.ToString());
            }
        }

        [TestMethod]
        public void UpdateModelTest()
        {
            string trackName = "Hello";

            var repositoryFactory = _kernel.Get<IRepositoryFactory>();
            using (var repository = repositoryFactory.CreateRepository<Track>())
            {
                repository.AddOrUpdate(new Track { Name = trackName });
                repository.SaveChanges();

                var track = repository.GetAll(t => t.Name == trackName).FirstOrDefault();
                Assert.IsNotNull(track);

                var duration = new TimeSpan(0, 2, 46);
                track.Duration = duration;

                repository.AddOrUpdate(track);
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll(t => t.Duration == duration).Any());
            }
        }
    }
}
