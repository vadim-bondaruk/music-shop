namespace PVT.Q1._2017.Shop.Tests
{
    #region

    using System;
    using System.Linq;

    using global::Shop.Common.Models;
    using global::Shop.DAL.Context;
    using global::Shop.DAL.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    #endregion

    /// <summary>
    ///     Summary description for <see cref="DataBaseTest" />
    /// </summary>
    [TestClass]
    public class DataBaseTest
    {
        /// <summary>
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        ///     Gets or sets the test context which provides information about and
        ///     functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }

            set
            {
                this.testContextInstance = value;
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void AddModelTest()
        {
            var trackName = "Hello";
            using (var context = new ShopContext())
            {
                var repository = new Repository<Track>(context);

                repository.AddOrUpdate(new Track { Name = trackName });
                context.SaveChanges();

                Assert.IsTrue(context.Tracks.Any(t => t.Name == trackName));
            }
        }

        /// <summary>
        /// </summary>
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

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetAllModelWithExpressionTest()
        {
            var track1Name = "Track 1";
            var track2Name = "Track 2";
            var track3Name = "Track 3";

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

        /// <summary>
        /// </summary>
        [TestMethod]
        public void GetAllModelWithoutExpressionTest()
        {
            var track1Name = "Track 1";
            var track2Name = "Track 2";
            var track3Name = "Track 3";

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

        /// <summary>
        /// </summary>
        [TestMethod]
        public void UpdateModelTest()
        {
            var trackName = "Hello";
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