using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ninject;

using Shop.BLL;
using Shop.Common.Models;
using Shop.DAL;
using Shop.DAL.Context;
using Shop.DAL.Repositories;
using Shop.Infrastructure.Repositories;

/// <summary>
///     Summary description for <see cref="DataBaseTest" />
/// </summary>
[TestClass]
public class DataBaseTest
{
    /// <summary>
    /// </summary>
    private readonly IKernel _kernel;

    /// <summary>
    /// </summary>
    private TestContext testContextInstance;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataBaseTest"/> class.
    /// </summary>
    public DataBaseTest()
    {
        this._kernel = new StandardKernel(new DefaultRepositoriesNinjectModule());
    }

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

        var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
        using (var repository = repositoryFactory.CreateRepository<Track>())
        {
            repository.AddOrUpdate(new Track { Name = trackName });
            repository.SaveChanges();

            Assert.IsTrue(repository.GetAll(t => t.Name == trackName).Any());
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
        var artist1Name = "Artist 1";
        var artist2Name = "Artist 2";
        var artist3Name = "Artist 3";

        var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
        using (var repository = repositoryFactory.CreateRepository<Artist>())
        {
            repository.AddOrUpdate(new Artist { Name = artist1Name });
            repository.AddOrUpdate(new Artist { Name = artist2Name });
            repository.AddOrUpdate(new Artist { Name = artist3Name });
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

        var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
        using (var repository = repositoryFactory.CreateRepository<Album>())
        {
            repository.AddOrUpdate(new Album { Name = album1Name });
            repository.AddOrUpdate(new Album { Name = album2Name });
            repository.AddOrUpdate(new Album { Name = album3Name });
            repository.SaveChanges();

            Assert.IsTrue(repository.GetAll().Count >= 3, repository.GetAll().Count.ToString());
        }
    }

    /// <summary>
    /// </summary>
    [TestMethod]
    public void RepositoryFactoryTest()
    {
        var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
        Assert.IsNotNull(repositoryFactory);
    }

    /// <summary>
    /// </summary>
    [TestMethod]
    public void UpdateModelTest()
    {
        var trackName = "Hello";

        var repositoryFactory = this._kernel.Get<IRepositoryFactory>();
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

    [TestMethod]
    public void TracksWithArtistTest()
    {
        var artist = new Artist { Name = "Sia" };
        var track1 = new Track { Name = "Unstoppable", Artist = artist };
        var track2 = new Track { Name = "Alive", Artist = artist };

        var repositoryFactory = _kernel.Get<IRepositoryFactory>();
        using (var repository = repositoryFactory.CreateRepository<Artist>())
        {
            repository.AddOrUpdate(artist);
        }

        using (var repository = repositoryFactory.CreateRepository<Track>())
        {
            repository.AddOrUpdate(track1);
            repository.AddOrUpdate(track2);
            repository.SaveChanges();

            Assert.IsTrue(repository.GetAll(t => t.Artist != null).Any());
        }
    }

    [TestMethod]
    public void TracksWithArtistsTest()
    {
        int count = 0;
        var repositoryFactory = _kernel.Get<IRepositoryFactory>();
        using (var repository = repositoryFactory.CreateRepository<Track>())
        {
            var tracks = repository.GetAll();
            foreach (var track in tracks)
            {
                if (track.Artist != null)
                {
                    count++;
                }
            }

            Assert.IsTrue(count > 0);
        }
    }
}