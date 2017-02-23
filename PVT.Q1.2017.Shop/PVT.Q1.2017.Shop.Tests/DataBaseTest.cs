using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Common.Models;
using Shop.DAL.Context;
using Shop.DAL.Repositories;

namespace PVT.Q1._2017.Shop.Tests
{
    /// <summary>
    /// Summary description for DataBaseTest
    /// </summary>
    [TestClass]
    public class DataBaseTest
    {
        public DataBaseTest()
        {
            //
            // TODO: Add constructor logic here
            //
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
        public void AddModelTest()
        {
            string trackName = "Hello";
            using (var context = new ShopContext())
            {
                var repository = new Repository<Track>(context);

                repository.AddOrUpdate(new Track { Name = trackName });
                context.SaveChanges();

                Assert.IsTrue(context.Tracks.Any(t => t.Name == trackName));
            }
        }

        [TestMethod]
        public void GetAllModelWithExpressionTest()
        {
            string track1Name = "Track 1";
            string track2Name = "Track 2";
            string track3Name = "Track 3";

            using (var context = new ShopContext())
            {
                var repository = new Repository<Track>(context);

                repository.AddOrUpdate(new Track { Name = track1Name });
                repository.AddOrUpdate(new Track { Name = track2Name });
                repository.AddOrUpdate(new Track { Name = track3Name });
                context.SaveChanges();

                Assert.IsTrue(repository.GetAll(t => t.Name.StartsWith("Track")).Count >= 3);
            }
        }

        [TestMethod]
        public void GetAllModelWithoutExpressionTest()
        {
            string track1Name = "Track 1";
            string track2Name = "Track 2";
            string track3Name = "Track 3";

            using (var context = new ShopContext())
            {
                var repository = new Repository<Track>(context);

                repository.AddOrUpdate(new Track { Name = track1Name });
                repository.AddOrUpdate(new Track { Name = track2Name });
                repository.AddOrUpdate(new Track { Name = track3Name });
                context.SaveChanges();

                Assert.IsTrue(repository.GetAll().Count >= 3, repository.GetAll().Count.ToString());
            }
        }

        [TestMethod]
        public void UpdateModelTest()
        {
            string trackName = "Hello";
            using (var context = new ShopContext())
            {
                var repository = new Repository<Track>(context);

                repository.AddOrUpdate(new Track { Name = trackName });
                context.SaveChanges();

                var track = repository.GetAll(t => t.Name == trackName).FirstOrDefault();
                Assert.IsNotNull(track);

                var duration = new TimeSpan(0, 2, 46);
                track.Duration = duration;

                repository.AddOrUpdate(track);
                context.SaveChanges();

                Assert.IsTrue(context.Tracks.Any(t => t.Duration == duration));
            }
        }
    }
}
