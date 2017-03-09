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

    public class RepositoryMoqFactory : IRepositoryFactory
    {
        #region Fields

        private readonly List<Track> _tracks = new List<Track>();
        private readonly List<Artist> _artists = new List<Artist>();
        private readonly List<Album> _albums = new List<Album>();
        private readonly List<CurrencyRate> _currencyRates = new List<CurrencyRate>();
        private readonly List<Currency> _currencies = new List<Currency>();
        private readonly List<AlbumPrice> _albumPrices = new List<AlbumPrice>();
        private readonly List<Feedback> _feedbacks = new List<Feedback>();
        private readonly List<Genre> _genres = new List<Genre>();
        private readonly List<PriceLevel> _priceLevels = new List<PriceLevel>();
        private readonly List<TrackPrice> _trackPrices = new List<TrackPrice>();
        private readonly List<User> _users = new List<User>();
        private readonly List<Vote> votes = new List<Vote>();

        #endregion //Fields

        #region IRepositoryFactory Members

        public IAlbumRepository GetAlbumRepository()
        {
            var mock = new Mock<IAlbumRepository>();

            mock.Setup(m => m.GetAll()).Returns(this._albums);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Album, BaseEntity>>[]>()))
                .Returns(this._albums);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Album, bool>>>()))
                .Returns(this._albums);

            mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<Album, bool>>>(),
                                    It.IsAny<Expression<Func<Album, BaseEntity>>[]>()))
                .Returns(this._albums);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._albums.Count, Range.Inclusive)))
                .Returns(this._albums.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._albums.Count))))
                .Returns(() => null);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._albums.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<Album, BaseEntity>>[]>()))
                .Returns(this._albums.Where(a => a.Artist != null).FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._albums.Count)),
                                      It.IsAny<Expression<Func<Album, BaseEntity>>[]>()))
                .Returns(() => null);

            mock.Setup(m => m.AddOrUpdate(It.IsNotNull<Album>())).Callback(() => this._albums.Add(new Album
            {
                Id = this._albums.Count + 1,
                Name = $"Album{ this._albums.Count + 1 }"
            }));

            mock.Setup(m => m.Delete(It.IsNotNull<Album>())).Callback(() =>
            {
                if (this._albums.Any())
                {
                    this._albums.RemoveAt(this._albums.Count - 1);
                }
            });

            return mock.Object;
        }

        public IArtistRepository GetArtistRepository()
        {
            var mock = new Mock<IArtistRepository>();

            mock.Setup(m => m.GetAll()).Returns(this._artists);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Artist, BaseEntity>>[]>()))
                .Returns(this._artists);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Artist, bool>>>()))
                .Returns(this._artists);

            mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<Artist, bool>>>(),
                                    It.IsAny<Expression<Func<Artist, BaseEntity>>[]>()))
                .Returns(this._artists);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._artists.Count, Range.Inclusive)))
                .Returns(this._artists.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._artists.Count))))
                .Returns(() => null);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._artists.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<Artist, BaseEntity>>[]>()))
                .Returns(this._artists.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._artists.Count)),
                                      It.IsAny<Expression<Func<Artist, BaseEntity>>[]>()))
                .Returns(() => null);

            mock.Setup(m => m.AddOrUpdate(It.IsNotNull<Artist>())).Callback(() => this._artists.Add(new Artist
            {
                Id = this._artists.Count + 1,
                Name = $"Artist{ this._artists.Count + 1 }"
            }));

            mock.Setup(m => m.Delete(It.IsNotNull<Artist>())).Callback(() =>
            {
                if (this._artists.Any())
                {
                    this._artists.RemoveAt(this._artists.Count - 1);
                }
            });

            return mock.Object;
        }

        public ITrackRepository GetTrackRepository()
        {
            var mock = new Mock<ITrackRepository>();

            mock.Setup(m => m.GetAll()).Returns(this._tracks);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Track, BaseEntity>>[]>()))
                .Returns(this._tracks);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Track, bool>>>()))
                .Returns(this._tracks);

            mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<Track, bool>>>(),
                                    It.IsAny<Expression<Func<Track, BaseEntity>>[]>()))
                .Returns(this._tracks);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._tracks.Count, Range.Inclusive)))
                .Returns(this._tracks.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._tracks.Count))))
                .Returns(() => null);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._tracks.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<Track, BaseEntity>>[]>()))
                .Returns(this._tracks.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._tracks.Count)),
                                      It.IsAny<Expression<Func<Track, BaseEntity>>[]>()))
                .Returns(() => null);

            mock.Setup(m => m.AddOrUpdate(It.IsNotNull<Track>())).Callback(() => this._tracks.Add(new Track
            {
                Id = this._tracks.Count + 1,
                Name = $"Track{ this._tracks.Count + 1 }"
            }));

            mock.Setup(m => m.Delete(It.IsNotNull<Track>())).Callback(() =>
            {
                if (this._tracks.Any())
                {
                    this._tracks.RemoveAt(this._tracks.Count - 1);
                }
            });

            return mock.Object;
        }

        public ICurrencyRepository GetCurrencyRepository()
        {
            var mock = new Mock<ICurrencyRepository>();

            mock.Setup(m => m.GetAll()).Returns(this._currencies);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()))
                .Returns(this._currencies);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Currency, bool>>>()))
                .Returns(this._currencies);

            mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<Currency, bool>>>(),
                                    It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()))
                .Returns(this._currencies);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._currencies.Count, Range.Inclusive)))
                .Returns(this._currencies.FirstOrDefault(a => a.Id >= 1));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._currencies.Count))))
                .Returns(() => null);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._currencies.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()))
                .Returns(this._currencies.FirstOrDefault(a => a.Id >= 1));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._currencies.Count)),
                                      It.IsAny<Expression<Func<Currency, BaseEntity>>[]>()))
                .Returns(() => null);

            mock.Setup(m => m.AddOrUpdate(It.IsNotNull<Currency>())).Callback(() => this._currencies.Add(new Currency
            {
                Id = this._currencies.Count + 1,
                Code = this._currencies.Count + 100,
                ShortName = $"CR{ this._currencies.Count + 1 }"
            }));

            mock.Setup(m => m.Delete(It.IsNotNull<Currency>())).Callback(() =>
            {
                if (this._currencies.Any())
                {
                    this._currencies.RemoveAt(this._currencies.Count - 1);
                }
            });

            return mock.Object;
        }

        public IAlbumPriceRepository GetAlbumPriceRepository()
        {
            var mock = new Mock<IAlbumPriceRepository>();

            mock.Setup(m => m.GetAll()).Returns(this._albumPrices);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<AlbumPrice, BaseEntity>>[]>()))
                .Returns(this._albumPrices);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<AlbumPrice, bool>>>()))
                .Returns(this._albumPrices);

            mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<AlbumPrice, bool>>>(),
                                    It.IsAny<Expression<Func<AlbumPrice, BaseEntity>>[]>()))
                .Returns(this._albumPrices);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._albumPrices.Count, Range.Inclusive)))
                .Returns(this._albumPrices.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._albumPrices.Count))))
                .Returns(() => null);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._albumPrices.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<AlbumPrice, BaseEntity>>[]>()))
                .Returns(this._albumPrices.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._albumPrices.Count)),
                                      It.IsAny<Expression<Func<AlbumPrice, BaseEntity>>[]>()))
                .Returns(() => null);

            mock.Setup(m => m.AddOrUpdate(It.IsNotNull<AlbumPrice>())).Callback(() => this._albumPrices.Add(new AlbumPrice
            {
                Id = this._albumPrices.Count + 1,
                Price = this._albumPrices.Count + 1.49m
            }));

            mock.Setup(m => m.Delete(It.IsNotNull<AlbumPrice>())).Callback(() =>
            {
                if (this._albumPrices.Any())
                {
                    this._albumPrices.RemoveAt(this._albumPrices.Count - 1);
                }
            });

            return mock.Object;
        }

        public ICurrencyRateRepository GetCurrencyRateRepository()
        {
            var mock = new Mock<ICurrencyRateRepository>();

            mock.Setup(m => m.GetAll()).Returns(_currencyRates);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<CurrencyRate, BaseEntity>>[]>()))
                .Returns(_currencyRates);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<CurrencyRate, bool>>>()))
                .Returns(_currencyRates);

            mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<CurrencyRate, bool>>>(),
                                    It.IsAny<Expression<Func<CurrencyRate, BaseEntity>>[]>()))
                .Returns(_currencyRates);

            mock.Setup(m => m.GetById(It.IsInRange(1, _currencyRates.Count, Range.Inclusive)))
                .Returns(_currencyRates.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, _currencyRates.Count))))
                .Returns(() => null);

            mock.Setup(m => m.GetById(It.IsInRange(1, _currencyRates.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<CurrencyRate, BaseEntity>>[]>()))
                .Returns(_currencyRates.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, _currencyRates.Count)),
                                      It.IsAny<Expression<Func<CurrencyRate, BaseEntity>>[]>()))
                .Returns(() => null);

            mock.Setup(m => m.AddOrUpdate(It.IsNotNull<CurrencyRate>())).Callback(() => _currencyRates.Add(new CurrencyRate
            {
                Id = _currencyRates.Count + 1,
                CrossCourse = _currencyRates.Count + 1.99m,
                Currency = new Currency
                {
                    Id = 1,
                    ShortName = "USD",
                    Code = 840
                },
                TargetCurrency = new Currency
                {
                    Id = 2,
                    ShortName = "EUR",
                    Code = 978
                }
            }));

            mock.Setup(m => m.Delete(It.IsNotNull<CurrencyRate>())).Callback(() =>
            {
                if (_currencyRates.Any())
                {
                    _currencyRates.RemoveAt(_currencyRates.Count - 1);
                }
            });

            return mock.Object;
        }

        public IFeedbackRepository GetFeedbackRepository()
        {
            var mock = new Mock<IFeedbackRepository>();

            mock.Setup(m => m.GetAll()).Returns(this._feedbacks);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Feedback, BaseEntity>>[]>()))
                .Returns(this._feedbacks);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Feedback, bool>>>()))
                .Returns(this._feedbacks);

            mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<Feedback, bool>>>(),
                                    It.IsAny<Expression<Func<Feedback, BaseEntity>>[]>()))
                .Returns(this._feedbacks);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._feedbacks.Count, Range.Inclusive)))
                .Returns(this._feedbacks.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._feedbacks.Count))))
                .Returns(() => null);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._feedbacks.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<Feedback, BaseEntity>>[]>()))
                .Returns(this._feedbacks.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._feedbacks.Count)),
                                      It.IsAny<Expression<Func<Feedback, BaseEntity>>[]>()))
                .Returns(() => null);

            mock.Setup(m => m.AddOrUpdate(It.IsNotNull<Feedback>())).Callback(() => this._feedbacks.Add(new Feedback
            {
                Id = this._feedbacks.Count + 1,
                Comments = $"Comment{ this._feedbacks.Count + 1}"
            }));

            mock.Setup(m => m.Delete(It.IsNotNull<Feedback>())).Callback(() =>
            {
                if (this._feedbacks.Any())
                {
                    this._feedbacks.RemoveAt(this._feedbacks.Count - 1);
                }
            });

            return mock.Object;
        }

        public IGenreRepository GetGenreRepository()
        {
            var mock = new Mock<IGenreRepository>();

            mock.Setup(m => m.GetAll()).Returns(this._genres);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Genre, BaseEntity>>[]>()))
                .Returns(this._genres);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns(this._genres);

            mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<Genre, bool>>>(),
                                    It.IsAny<Expression<Func<Genre, BaseEntity>>[]>()))
                .Returns(this._genres);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._genres.Count, Range.Inclusive)))
                .Returns(this._genres.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._genres.Count))))
                .Returns(() => null);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._genres.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<Genre, BaseEntity>>[]>()))
                .Returns(this._genres.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._genres.Count)),
                                      It.IsAny<Expression<Func<Genre, BaseEntity>>[]>()))
                .Returns(() => null);

            mock.Setup(m => m.AddOrUpdate(It.IsNotNull<Genre>())).Callback(() => this._genres.Add(new Genre
            {
                Id = this._genres.Count + 1,
                Name = $"Genre{ this._genres.Count + 1}"
            }));

            mock.Setup(m => m.Delete(It.IsNotNull<Genre>())).Callback(() =>
            {
                if (this._genres.Any())
                {
                    this._genres.RemoveAt(this._genres.Count - 1);
                }
            });

            return mock.Object;
        }

        public IPriceLevelRepository GetPriceLevelRepository()
        {
            var mock = new Mock<IPriceLevelRepository>();

            mock.Setup(m => m.GetAll()).Returns(this._priceLevels);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<PriceLevel, BaseEntity>>[]>()))
                .Returns(this._priceLevels);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<PriceLevel, bool>>>()))
                .Returns(this._priceLevels);

            mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<PriceLevel, bool>>>(),
                                    It.IsAny<Expression<Func<PriceLevel, BaseEntity>>[]>()))
                .Returns(this._priceLevels);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._priceLevels.Count, Range.Inclusive)))
                .Returns(this._priceLevels.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._priceLevels.Count))))
                .Returns(() => null);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._priceLevels.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<PriceLevel, BaseEntity>>[]>()))
                .Returns(this._priceLevels.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._priceLevels.Count)),
                                      It.IsAny<Expression<Func<PriceLevel, BaseEntity>>[]>()))
                .Returns(() => null);

            mock.Setup(m => m.AddOrUpdate(It.IsNotNull<PriceLevel>())).Callback(() => this._priceLevels.Add(new PriceLevel
            {
                Id = this._priceLevels.Count + 1,
                Name = $"PriceLevel{ this._priceLevels.Count + 1}"
            }));

            mock.Setup(m => m.Delete(It.IsNotNull<PriceLevel>())).Callback(() =>
            {
                if (this._priceLevels.Any())
                {
                    this._priceLevels.RemoveAt(this._priceLevels.Count - 1);
                }
            });

            return mock.Object;
        }

        public ITrackPriceRepository GetTrackPriceRepository()
        {
            var mock = new Mock<ITrackPriceRepository>();

            mock.Setup(m => m.GetAll()).Returns(this._trackPrices);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<TrackPrice, BaseEntity>>[]>()))
                .Returns(this._trackPrices);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<TrackPrice, bool>>>()))
                .Returns(this._trackPrices);

            mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<TrackPrice, bool>>>(),
                                    It.Is<Expression<Func<TrackPrice, BaseEntity>>[]>(x => x != null)))
                .Returns(this._trackPrices);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._trackPrices.Count, Range.Inclusive)))
                .Returns(this._trackPrices.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._trackPrices.Count))))
                .Returns(() => null);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._trackPrices.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<TrackPrice, BaseEntity>>[]>()))
                .Returns(this._trackPrices.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._trackPrices.Count)),
                                      It.IsAny<Expression<Func<TrackPrice, BaseEntity>>[]>()))
                .Returns(() => null);

            mock.Setup(m => m.AddOrUpdate(It.IsNotNull<TrackPrice>())).Callback(() => this._trackPrices.Add(new TrackPrice
            {
                Id = this._trackPrices.Count + 1,
                Price = this._trackPrices.Count + 1.49m
            }));

            mock.Setup(m => m.Delete(It.IsNotNull<TrackPrice>())).Callback(() =>
            {
                if (this._trackPrices.Any())
                {
                    this._trackPrices.RemoveAt(this._trackPrices.Count - 1);
                }
            });

            return mock.Object;
        }

        public IUserRepository GetUserRepository()
        {
            var mock = new Mock<IUserRepository>();

            mock.Setup(m => m.GetAll()).Returns(this._users);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<User, BaseEntity>>[]>()))
                .Returns(this._users);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(this._users);

            mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<User, bool>>>(),
                                    It.IsAny<Expression<Func<User, BaseEntity>>[]>()))
                .Returns(this._users);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._users.Count, Range.Inclusive)))
                .Returns(this._users.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._users.Count))))
                .Returns(() => null);

            mock.Setup(m => m.GetById(It.IsInRange(1, this._users.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<User, BaseEntity>>[]>()))
                .Returns(this._users.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, this._users.Count)),
                                      It.IsAny<Expression<Func<User, BaseEntity>>[]>()))
                .Returns(() => null);

            mock.Setup(m => m.AddOrUpdate(It.IsNotNull<User>())).Callback(() => this._users.Add(new User
            {
                Id = this._users.Count + 1
            }));

            mock.Setup(m => m.Delete(It.IsNotNull<User>())).Callback(() =>
            {
                if (this._users.Any())
                {
                    this._users.RemoveAt(this._users.Count - 1);
                }
            });

            return mock.Object;
        }

        public IVoteRepository GetVoteRepository()
        {
            var mock = new Mock<IVoteRepository>();

            mock.Setup(m => m.GetAll()).Returns(votes);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Vote, BaseEntity>>[]>()))
                .Returns(votes);

            mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Vote, bool>>>()))
                .Returns(votes);

            mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<Vote, bool>>>(),
                                    It.IsAny<Expression<Func<Vote, BaseEntity>>[]>()))
                .Returns(votes);

            mock.Setup(m => m.GetById(It.IsInRange(1, votes.Count, Range.Inclusive)))
                .Returns(votes.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, votes.Count))))
                .Returns(() => null);

            mock.Setup(m => m.GetById(It.IsInRange(1, votes.Count, Range.Inclusive),
                                      It.IsAny<Expression<Func<Vote, BaseEntity>>[]>()))
                .Returns(votes.FirstOrDefault(a => a.Id > 0));

            mock.Setup(m => m.GetById(It.IsNotIn(Enumerable.Range(1, votes.Count)),
                                      It.IsAny<Expression<Func<Vote, BaseEntity>>[]>()))
                .Returns(() => null);

            mock.Setup(m => m.AddOrUpdate(It.IsNotNull<Vote>())).Callback(() => votes.Add(new Vote
            {
                Id = votes.Count + 1,
                Mark = (Mark)(votes.Count % 5 + 1)
            }));

            mock.Setup(m => m.Delete(It.IsNotNull<Vote>())).Callback(() =>
            {
                if (votes.Any())
                {
                    votes.RemoveAt(votes.Count - 1);
                }
            });

            return mock.Object;
        }

        #endregion //IRepositoryFactory Members
    }
}