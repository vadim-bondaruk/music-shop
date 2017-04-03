namespace Shop.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Common.Models;
    using Context;

    internal sealed class Configuration : DbMigrationsConfiguration<Shop.DAL.Context.ShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context.ShopContext context)
        {
            AddDefaultCurrencies(context);
            AddDefaultCurrencyRates(context);
            AddDefaultGenres(context);
            AddDefaultPriceLevels(context);

#if DEBUG
            AddDefaultArtistsAndTracks(context);
#endif
        }

        private void AddDefaultAlbums(ShopContext context)
        {
            if (!context.Set<Album>().Any())
            {
                context.Set<Album>().AddOrUpdate(new[] {
                    new Album { ArtistId = 1, Name = "Крыша дома твоего" }
                });
                context.SaveChanges();

                context.Set<AlbumTrackRelation>().AddOrUpdate(new[]
                {
                    new AlbumTrackRelation { TrackId = 1, AlbumId = 1 },
                    new AlbumTrackRelation { TrackId = 2, AlbumId = 1 }
                });
                context.SaveChanges();
            }
        }

        private void AddDefaultTracks(ShopContext context)
        {
            if (!context.Set<Track>().Any())
            {
                context.Set<Track>().AddOrUpdate(new[] {
                    new Track { ArtistId = 1, Name = "Море", GenreId = 9 },
                    new Track { ArtistId = 1, Name = "Крыша дома твоего", GenreId = 9 }
                });
                context.SaveChanges();
            }
        }

        private void AddDefaultTracksPrices(ShopContext context)
        {
            if (!context.Set<TrackPrice>().Any())
            {
                context.Set<TrackPrice>().AddOrUpdate(new[] {
                    new TrackPrice { TrackId = 1, CurrencyId = 1, Price = 4.99m, PriceLevelId = 1 },
                    new TrackPrice { TrackId = 2, CurrencyId = 1, Price = 4.98m, PriceLevelId = 1 }
                });
                context.SaveChanges();
            }
        }

        private void AddDefaultAlbumsPrices(ShopContext context)
        {
            if (!context.Set<AlbumPrice>().Any())
            {
                context.Set<AlbumPrice>().AddOrUpdate(new[] {
                    new AlbumPrice { AlbumId = 1, CurrencyId = 1, Price = 14.99m, PriceLevelId = 1 }
                });
                context.SaveChanges();
            }
        }

        private void AddDefaultArtistsAndTracks(ShopContext context)
        {
            if (!context.Set<Artist>().Any())
            {
                context.Set<Artist>().AddOrUpdate(new[] {
                    new Artist { Name = "Ю.Антонов" },
                    new Artist { Name = "И.Кабзон" }
                });

                context.SaveChanges();

                AddDefaultTracks(context);
                AddDefaultAlbums(context);
                AddDefaultTracksPrices(context);
                AddDefaultAlbumsPrices(context);
            }
        }

        private void AddDefaultCurrencies(Context.ShopContext context)
        {
            if (!context.Set<Currency>().Any(c => c.ShortName == "EUR"))
            {
                context.Set<Currency>().AddOrUpdate(new[] { new Currency {
                    ShortName = "EUR",
                    Code = 978,
                    FullName = "EURO"
                }});
            }

            if (!context.Set<Currency>().Any(c => c.ShortName == "USD"))
            {
                context.Set<Currency>().AddOrUpdate(new[] { new Currency {
                    ShortName = "USD",
                    Code = 840,
                    FullName = "US Dollar"
                }});
            }

            context.SaveChanges();
        }

        private void AddDefaultCurrencyRates(Context.ShopContext context)
        {
            if (!context.Set<CurrencyRate>().Any())
            {
                context.Set<CurrencyRate>().AddOrUpdate(new[] { new CurrencyRate {
                    CurrencyId = 1,
                    TargetCurrencyId = 2,
                    CrossCourse = 0.9M
                }});

                context.Set<CurrencyRate>().AddOrUpdate(new[] { new CurrencyRate {
                    CurrencyId = 1,
                    TargetCurrencyId = 2,
                    CrossCourse = 1.2M
                }});
            }

            context.SaveChanges();
        }

        private void AddDefaultGenres(Context.ShopContext context)
        {
            if (!context.Set<Genre>().Any())
            {
                // source: https://en.wikipedia.org/wiki/List_of_popular_music_genres
                context.Set<Genre>().AddOrUpdate(new[]
                {
                    new Genre { Name = "Unknown" },
                    new Genre { Name = "Blues" },
                    new Genre { Name = "Country" },
                    new Genre { Name = "Electronic" },
                    new Genre { Name = "Folk" },
                    new Genre { Name = "Hip hop" },
                    new Genre { Name = "Jazz" },
                    new Genre { Name = "Latin" },
                    new Genre { Name = "Pop" },
                    new Genre { Name = "R&B and soul" },
                    new Genre { Name = "Rock" }
                });
                context.SaveChanges();
            }
        }

        private void AddDefaultPriceLevels(Context.ShopContext context)
        {
            if (!context.Set<PriceLevel>().Any())
            {
                context.Set<PriceLevel>().AddOrUpdate(new[] { new PriceLevel { Name = "Default" } });
                context.SaveChanges();
            }
        }
    }
}
