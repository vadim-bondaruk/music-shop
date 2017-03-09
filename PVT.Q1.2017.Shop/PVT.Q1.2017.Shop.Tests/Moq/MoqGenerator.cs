namespace PVT.Q1._2017.Shop.Tests.Moq
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using global::Moq;

    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Models;

    /// <summary>
    /// </summary>
    public class MoqGenerator
    {
        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public IAlbumRepository GetAlbumRepoMoq()
        {
            var albumRepo = new Mock<IAlbumRepository>();
            albumRepo.Setup(m => m.GetAll(It.IsAny<Expression<Func<Album, BaseEntity>>>()))
                .Returns(
                    new List<Album>
                        {
                            new Album
                                {
                                    Id = 22,
                                    Name = "NewAlbum1",
                                    ReleaseDate = DateTime.Now,
                                    ArtistId = 22,
                                    IsDeleted = false,
                                    TrackId = 22,
                                    Tracks =
                                        new List<Track>
                                            {
                                                new Track
                                                    {
                                                        Name = "NewTrackName",
                                                        ReleaseDate = DateTime.Now,
                                                        ArtistId = 22,
                                                        Duration = TimeSpan.MinValue
                                                    }
                                            }
                                },
                            new Album
                                {
                                    Id = 23,
                                    Name = "NewAlbum2",
                                    ReleaseDate = DateTime.Now,
                                    ArtistId = 23,
                                    IsDeleted = false,
                                    TrackId = 23,
                                    Tracks =
                                        new List<Track>
                                            {
                                                new Track
                                                    {
                                                        Name = "NewTrackName",
                                                        ReleaseDate = DateTime.Now,
                                                        ArtistId = 23,
                                                        Duration = TimeSpan.MinValue
                                                    }
                                            }
                                }
                        });

            albumRepo.Setup(m => m.GetAll())
                .Returns(
                    new List<Album>
                        {
                            new Album
                                {
                                    Id = 22,
                                    Name = "NewAlbum1",
                                    ReleaseDate = DateTime.Now,
                                    ArtistId = 22,
                                    IsDeleted = false,
                                    TrackId = 22,
                                    Tracks =
                                        new List<Track>
                                            {
                                                new Track
                                                    {
                                                        Name = "NewTrackName1",
                                                        ReleaseDate = DateTime.Now,
                                                        ArtistId = 22,
                                                        Duration = TimeSpan.MinValue
                                                    }
                                            }
                                },
                            new Album
                                {
                                    Id = 23,
                                    Name = "NewAlbum2",
                                    ReleaseDate = DateTime.Now,
                                    ArtistId = 23,
                                    IsDeleted = false,
                                    TrackId = 23,
                                    Tracks =
                                        new List<Track>
                                            {
                                                new Track
                                                    {
                                                        Name = "NewTrackName2",
                                                        ReleaseDate = DateTime.Now,
                                                        ArtistId = 23,
                                                        Duration = TimeSpan.MinValue
                                                    }
                                            }
                                }
                        });


            albumRepo.Setup(m => m.GetById(1)).Returns(new Album { Name = "SomeAlbum", Id = 22, IsDeleted = false });
            return albumRepo.Object;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public ITrackRepository GetTrackRepoMoq()
        {
            var trackRepo = new Mock<ITrackRepository>();

            trackRepo.Setup(t => t.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<Track, BaseEntity>>[]>()))
                .Returns(
                    new Track
                        {
                            Id = 22,
                            Name = "SomeTrackName",
                            Artist = new Artist { Id = 21, Name = "SomeArtistName", IsDeleted = false },
                            AlbumId = 21,
                            Albums =
                                new List<Album> { new Album { Name = "SomeAlbum1" }, new Album { Name = "SomeAlbum2" } }
                        });

            trackRepo.Setup(t => t.GetAll(It.IsAny<Expression<Func<Track, BaseEntity>>>()))
                .Returns(
                    new List<Track>
                        {
                            new Track
                                {
                                    Id = 22,
                                    Name = "SomeTrack1",
                                    Artist = new Artist { Id = 12, Name = "SomeArtist1" },
                                    Albums =
                                        new List<Album> { new Album { Id = 18, Name = "SomeAlbum1" } }
                                },
                            new Track
                                {
                                    Id = 23,
                                    Name = "SomeTrack2",
                                    Artist = new Artist { Id = 12, Name = "SomeArtist1" },
                                    Albums =
                                        new List<Album> { new Album { Id = 18, Name = "SomeAlbum1" } }
                                },
                            new Track
                                {
                                    Id = 24,
                                    Name = "SomeTrack3",
                                    Artist = new Artist { Id = 14, Name = "SomeArtist2" },
                                    Albums =
                                        new List<Album> { new Album { Id = 18, Name = "SomeAlbum1" } }
                                },
                            new Track
                                {
                                    Id = 25,
                                    Name = "SomeTrack4",
                                    Artist = new Artist { Id = 15, Name = "SomeArtist3" },
                                    Albums =
                                        new List<Album> { new Album { Id = 19, Name = "SomeAlbum2" } }
                                }
                        });

            trackRepo.Setup(t => t.GetAll(It.IsAny<Expression<Func<Track, bool>>>()))
                 .Returns(
                     new List<Track>
                         {
                            new Track
                                {
                                    Id = 22,
                                    Name = "SomeTrack1",
                                    Artist = new Artist { Id = 22, Name = "SomeArtist1" },
                                    Albums =
                                        new List<Album> { new Album { Id = 22, Name = "SomeAlbum1" } }
                                },
                            new Track
                                {
                                    Id = 23,
                                    Name = "SomeTrack2",
                                    Artist = new Artist { Id = 22, Name = "SomeArtist1" },
                                    Albums =
                                        new List<Album> { new Album { Id = 22, Name = "SomeAlbum1" } }
                                },
                            new Track
                                {
                                    Id = 24,
                                    Name = "SomeTrack3",
                                    Artist = new Artist { Id = 23, Name = "SomeArtist2" },
                                    Albums =
                                        new List<Album> { new Album { Id = 22, Name = "SomeAlbum1" } }
                                },
                            new Track
                                {
                                    Id = 25,
                                    Name = "SomeTrack4",
                                    Artist = new Artist { Id = 24, Name = "SomeArtist3" },
                                    Albums =
                                        new List<Album> { new Album { Id = 23, Name = "SomeAlbum2" } }
                                }
                         });
            return trackRepo.Object;
        }
    }
}