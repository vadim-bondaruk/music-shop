﻿namespace PVT.Q1._2017.Shop.Tests.Moq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using global::Moq;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Models;

    public class AlbumRepositoryMoq
    {
        private readonly List<Album> _albums = new List<Album>();
        private readonly Mock<IAlbumRepository> _mock;

        public AlbumRepositoryMoq()
        {
            _mock = new Mock<IAlbumRepository>();

            _mock.Setup(m => m.GetAll()).Returns(_albums);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Album, BaseEntity>>[]>()))
                 .Returns(_albums);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Album, bool>>>()))
                 .Returns(_albums);

            _mock.Setup(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Album, bool>>>(),
                                     It.IsAny<Expression<Func<Album, BaseEntity>>[]>()))
                 .Returns(_albums);

            _mock.Setup(m => m.GetById(It.IsInRange(1, _albums.Count, Range.Inclusive)))
                 .Returns(_albums.FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, _albums.Count))))
                 .Returns(() => null);

            _mock.Setup(m => m.GetById(It.IsInRange(1, _albums.Count, Range.Inclusive),
                                       It.IsAny<Expression<Func<Album, BaseEntity>>[]>()))
                 .Returns(_albums.Where(a => a.Artist != null).FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, _albums.Count)),
                                       It.IsAny<Expression<Func<Album, BaseEntity>>[]>()))
                 .Returns(() => null);

            _mock.Setup(m => m.AddOrUpdate(It.IsNotNull<Album>())).Callback(() => _albums.Add(new Album
            {
                Id = _albums.Count + 1,
                Name = $"Album{_albums.Count + 1}"
            }));

            _mock.Setup(m => m.Delete(It.IsNotNull<Album>())).Callback(() =>
            {
                if (_albums.Any())
                {
                    _albums.RemoveAt(_albums.Count - 1);
                }
            });
        }

        public IAlbumRepository Repository
        {
            get { return _mock.Object; }
        }
    }
}