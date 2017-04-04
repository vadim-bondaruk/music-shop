namespace PVT.Q1._2017.Shop.Tests.Moq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using global::Moq;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Models;

    public class TrackRepositoryMoq
    {
        private readonly List<Track> _tracks = new List<Track>();
        private readonly Mock<ITrackRepository> _mock;

        public TrackRepositoryMoq()
        {
            _mock = new Mock<ITrackRepository>();

            _mock.Setup(m => m.GetAll()).Returns(_tracks);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Track, BaseEntity>>[]>()))
                 .Returns(_tracks);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Track, bool>>>()))
                 .Returns(_tracks);

            _mock.Setup(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Track, bool>>>(),
                                     It.IsAny<Expression<Func<Track, BaseEntity>>[]>()))
                 .Returns(_tracks);

            _mock.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Track, bool>>>()))
                 .Returns(() => _tracks.FirstOrDefault());

            _mock.Setup(
                        m =>
                            m.FirstOrDefault(It.IsAny<Expression<Func<Track, bool>>>(),
                                             It.IsAny<Expression<Func<Track, BaseEntity>>[]>()))
                 .Returns(() => _tracks.FirstOrDefault());

            _mock.Setup(m => m.GetById(It.IsAny<int>()))
                 .Returns(() => _tracks.FirstOrDefault(t => t.Id > 0));

            _mock.Setup(m => m.GetById(It.IsAny<int>(),
                                       It.IsAny<Expression<Func<Track, BaseEntity>>[]>()))
                 .Returns(() => _tracks.FirstOrDefault(t => t.Id > 0));

            _mock.Setup(m => m.AddOrUpdate(It.IsNotNull<Track>())).Callback(() => _tracks.Add(new Track
            {
                Id = _tracks.Count + 1,
                Name = $"Track{_tracks.Count + 1}"
            }));

            _mock.Setup(m => m.Delete(It.IsNotNull<Track>())).Callback(() =>
            {
                if (_tracks.Any())
                {
                    _tracks.RemoveAt(_tracks.Count - 1);
                }
            });
        }

        public ITrackRepository Repository
        {
            get { return _mock.Object; }
        }
    }
}