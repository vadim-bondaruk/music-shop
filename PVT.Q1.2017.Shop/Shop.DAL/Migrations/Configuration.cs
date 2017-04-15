namespace Shop.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Infrastructure.Enums;
    using Shop.Common.Models;
    using Shop.DAL.Context;

    /// <summary>
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<ShopContext>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        protected override void Seed(ShopContext context)
        {
            this.AddDefaultCurrencies(context);
            this.AddDefaultCurrencyRates(context);
            this.AddDefaultGenres(context);
            this.AddDefaultPriceLevels(context);
#if DEBUG
            this.AddDefaultUsers(context);
            this.AddDefaultArtistsAndTracks(context);
#endif
        }

        private void AddDefaultUsers(ShopContext context)
        {
            if (!context.Set<User>().Any())
            {
                context.Set<User>()
                       .AddOrUpdate(
                                    new User
                                    {
                                        UserRoles = UserRoles.Admin,
                                        Login = "admin",
                                        FirstName = "admin",
                                        LastName = "admin",
                                        Email = "admin@shop.com",
                                        Sex = "M",
                                        Password = "8D66A53A381493BEC08DA23CEF5A43767F20A42C" // admin123!
                                    },
                                    new User
                                    {
                                        UserRoles = UserRoles.Seller,
                                        Login = "seller",
                                        FirstName = "seller",
                                        LastName = "seller",
                                        Email = "seller@shop.com",
                                        Sex = "M",
                                        Password = "1C1947F409978BC0A2601027FCCA6290583A60D9" // seller123!
                                    },
                                    new User
                                    {
                                        UserRoles = UserRoles.Buyer,
                                        Login = "buyer",
                                        FirstName = "buyer",
                                        LastName = "buyer",
                                        Email = "buyer@shop.com",
                                        Sex = "M",
                                        Password = "06490A9E09845EC0AAC622A6C43DAB47A516B096" // buyer123!
                                    },
                                    new User
                                    {
                                        UserRoles = UserRoles.User,
                                        Login = "user",
                                        FirstName = "user",
                                        LastName = "user",
                                        Email = "user@shop.com",
                                        Sex = "M",
                                        Password = "C0018FDBFC43F406264C2A3D82CAB7373AE090A1" // user123!
                                    });

                context.Set<UserData>()
                       .AddOrUpdate(
                                    new UserData { CurrencyId = 1, PriceLevelId = 1, UserId = 1 },
                                    new UserData { CurrencyId = 1, PriceLevelId = 1, UserId = 2 },
                                    new UserData { CurrencyId = 1, PriceLevelId = 1, UserId = 3 },
                                    new UserData { CurrencyId = 1, PriceLevelId = 1, UserId = 4 });

                context.SaveChanges();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        private void AddDefaultAlbums(ShopContext context)
        {
            if (!context.Set<Album>().Any())
            {
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 1, Name = "Крыша дома твоего" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 5, Name = "Elements of life" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 5, Name = "Kaleidoscope" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 5, Name = "Just be" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 6, Name = "Delirium" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 7, Name = "More life" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 7, Name = "Views" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 7, Name = "Take care" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 8, Name = "Daydream" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 8, Name = "Emotions" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 8, Name = "Rainbow" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 15, Name = "Illuminate" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 15, Name = "Handwritten" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 15, Name = "Don`t be a fool" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 16, Name = "Joanne" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 16, Name = "Artpop" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 16, Name = "The fame" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 17, Name = "Sacred love" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 17, Name = "Ten summoners tales" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 17, Name = "The soul cage" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 18, Name = "Load" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 18, Name = "Garage inc." });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 18, Name = "S&M" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 19, Name = "Imagine" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 19, Name = "Mirage" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 19, Name = "Intence" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 20, Name = "Motion" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 20, Name = "18 month" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 20, Name = "I created disco" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 21, Name = "Girl" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 21, Name = "In my mind" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 21, Name = "Out of my mind" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 22, Name = "Organik" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 22, Name = "23am" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 22, Name = "Dreamland" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 23, Name = "Crossroads" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 23, Name = "I still do" });
                context.Set<Album>().AddOrUpdate(new Album { ArtistId = 23, Name = "Blues" });
                context.SaveChanges();

                context.Set<AlbumTrackRelation>()
                    .AddOrUpdate(
                        new AlbumTrackRelation { TrackId = 1, AlbumId = 1 },
                        new AlbumTrackRelation { TrackId = 2, AlbumId = 1 });
                context.SaveChanges();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        private void AddDefaultAlbumsPrices(ShopContext context)
        {
            if (!context.Set<AlbumPrice>().Any())
            {
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 1, CurrencyId = 1, Price = 14.99m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 2, CurrencyId = 1, Price = 12.58m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 3, CurrencyId = 1, Price = 11.44m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 4, CurrencyId = 1, Price = 10.99m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 5, CurrencyId = 1, Price = 10.12m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 6, CurrencyId = 1, Price = 14.22m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 7, CurrencyId = 1, Price = 9.54m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 8, CurrencyId = 1, Price = 12.32m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 9, CurrencyId = 1, Price = 5.45m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 10, CurrencyId = 1, Price = 7.88m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 11, CurrencyId = 1, Price = 9.29m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 12, CurrencyId = 1, Price = 11.46m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 13, CurrencyId = 1, Price = 10.28m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 14, CurrencyId = 1, Price = 9.45m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 15, CurrencyId = 1, Price = 7.24m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 16, CurrencyId = 1, Price = 7.41m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 17, CurrencyId = 1, Price = 9.22m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 18, CurrencyId = 1, Price = 12.15m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 19, CurrencyId = 1, Price = 10.14m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 20, CurrencyId = 1, Price = 16.40m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 21, CurrencyId = 1, Price = 4.12m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 22, CurrencyId = 1, Price = 6.10m, PriceLevelId = 1 });
                context.Set<AlbumPrice>()
                    .AddOrUpdate(new AlbumPrice { AlbumId = 23, CurrencyId = 1, Price = 7.55m, PriceLevelId = 1 });
                context.SaveChanges();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        private void AddDefaultArtistsAndTracks(ShopContext context)
        {
            if (!context.Set<Artist>().Any())
            {
                context.Set<Artist>()
                    .AddOrUpdate(
                        new Artist { Name = "Ю.Антонов" },
                        new Artist { Name = "И.Кабзон" },
                        new Artist { Name = "Bruno Mars" },
                        new Artist { Name = "Rihanna" },
                        new Artist { Name = "Tiesto" },
                        new Artist { Name = "Ellie Goulding" },
                        new Artist { Name = "Drake" },
                        new Artist { Name = "Mariah Carey" },
                        new Artist { Name = "Khalid" },
                        new Artist { Name = "Migos" },
                        new Artist { Name = "Skrillex" },
                        new Artist { Name = "Martin Garrix" },
                        new Artist { Name = "Maroon 5" },
                        new Artist { Name = "Justin Timberlake" },
                        new Artist { Name = "Shawn Mendes" },
                        new Artist { Name = "Lady Gaga" },
                        new Artist { Name = "Sting" },
                        new Artist { Name = "Mettalica" },
                        new Artist { Name = "Armin van Buuren" },
                        new Artist { Name = "Calvin Harris" },
                        new Artist { Name = "Pharell Williams" },
                        new Artist { Name = "Robert Miles" },
                        new Artist { Name = "Eric Clapton" },
                        new Artist { Name = "Modern Talking" },
                        new Artist { Name = "Enigma" },
                        new Artist { Name = "Kygo" },
                        new Artist { Name = "dj Snake" },
                        new Artist { Name = "MO" },
                        new Artist { Name = "James Arthur" },
                        new Artist { Name = "Katy Perry" },
                        new Artist { Name = "Ayo & Teo" },
                        new Artist { Name = "Jon Pardi" },
                        new Artist { Name = "Travis Scott" },
                        new Artist { Name = "Imagine Dragons" },
                        new Artist { Name = "Jason Aldean" },
                        new Artist { Name = "Lorde" },
                        new Artist { Name = "Childish Gambino" },
                        new Artist { Name = "Luke Combs" },
                        new Artist { Name = "Luke Bryan" },
                        new Artist { Name = "Logic" },
                        new Artist { Name = "Lady Antebellum" },
                        new Artist { Name = "Auli'i Cravalho" },
                        new Artist { Name = "Josh Turner" },
                        new Artist { Name = "Dierks Bentley" },
                        new Artist { Name = "Lauren Alaina" },
                        new Artist { Name = "Ed Sheeran" },
                        new Artist { Name = "Train" },
                        new Artist { Name = "J. Cole" },
                        new Artist { Name = "Big Sean" },
                        new Artist { Name = "Miranda Lambert" },
                        new Artist { Name = "Adele" });

                context.SaveChanges();

                this.AddDefaultTracks(context);
                this.AddDefaultAlbums(context);
                this.AddDefaultTracksPrices(context);
                this.AddDefaultAlbumsPrices(context);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        private void AddDefaultCurrencies(ShopContext context)
        {
            if (!context.Set<Currency>().Any(c => c.ShortName == "EUR"))
            {
                context.Set<Currency>().AddOrUpdate(new Currency { ShortName = "EUR", Code = 978, FullName = "EURO" });
            }

            if (!context.Set<Currency>().Any(c => c.ShortName == "USD"))
            {
                context.Set<Currency>()
                    .AddOrUpdate(new Currency { ShortName = "USD", Code = 840, FullName = "US Dollar" });
            }

            if (!context.Set<Setting>().Any())
            {
                context.Set<Setting>().AddOrUpdate(new Setting { DefaultCurrencyId = 1 });
            }

            context.SaveChanges();
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        private void AddDefaultCurrencyRates(ShopContext context)
        {
            if (!context.Set<CurrencyRate>().Any())
            {
                context.Set<CurrencyRate>()
                    .AddOrUpdate(new CurrencyRate { CurrencyId = 1, TargetCurrencyId = 2, CrossCourse = 1.2M });

                context.Set<CurrencyRate>()
                    .AddOrUpdate(new CurrencyRate { CurrencyId = 1, TargetCurrencyId = 2, CrossCourse = 0.9M });
            }

            context.SaveChanges();
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        private void AddDefaultGenres(ShopContext context)
        {
            if (!context.Set<Genre>().Any())
            {
                // source: https://en.wikipedia.org/wiki/List_of_popular_music_genres
                context.Set<Genre>()
                    .AddOrUpdate(
                        new Genre { Name = "Unknown" },
                        new Genre { Name = "Blues" },
                        new Genre { Name = "Country" },
                        new Genre { Name = "Electronic" },
                        new Genre { Name = "Folk" },
                        new Genre { Name = "Hip hop" },
                        new Genre { Name = "Jazz" },
                        new Genre { Name = "Latin" },
                        new Genre { Name = "Latin house" },
                        new Genre { Name = "Pop" },
                        new Genre { Name = "Disco" },
                        new Genre { Name = "Rock" },
                        new Genre { Name = "New age" },
                        new Genre { Name = "Metall" },
                        new Genre { Name = "Trash metall" },
                        new Genre { Name = "Heavy metall" },
                        new Genre { Name = "R&B and soul" },
                        new Genre { Name = "Rap" },
                        new Genre { Name = "Rap" },
                        new Genre { Name = "Hip-hop" },
                        new Genre { Name = "Jazz" },
                        new Genre { Name = "House" },
                        new Genre { Name = "Deep house" },
                        new Genre { Name = "Trance" },
                        new Genre { Name = "Ambient" },
                        new Genre { Name = "Chillout" },
                        new Genre { Name = "Speed garage" },
                        new Genre { Name = "Breakbit" },
                        new Genre { Name = "Breaks" },
                        new Genre { Name = "Drum `n` Bass" },
                        new Genre { Name = "Dubstep" },
                        new Genre { Name = "Trip hop" });
                context.SaveChanges();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        private void AddDefaultPriceLevels(ShopContext context)
        {
            if (!context.Set<PriceLevel>().Any())
            {
                context.Set<PriceLevel>().AddOrUpdate(new PriceLevel { Name = "Default" });
                context.SaveChanges();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        private void AddDefaultTracks(ShopContext context)
        {
            if (!context.Set<Track>().Any())
            {
                context.Set<Track>()
                    .AddOrUpdate(
                        new Track { ArtistId = 1, Name = "Море", GenreId = 10 },
                        new Track { ArtistId = 1, Name = "Крыша дома твоего", GenreId = 10 },
                        new Track { ArtistId = 2, Name = "Песня о тревожной молодости", GenreId = 10 },
                        new Track { Name = "Just be", ArtistId = 5, GenreId = 24 },
                        new Track { Name = "Adagio for strings", ArtistId = 5, GenreId = 24 },
                        new Track { Name = "Traffic", ArtistId = 5, GenreId = 24 },
                        new Track { Name = "Fligth 643", ArtistId = 5, GenreId = 24 },
                        new Track { Name = "Dance for life", ArtistId = 5, GenreId = 24 },
                        new Track { Name = "Breda 8pm", ArtistId = 5, GenreId = 24 },
                        new Track { Name = "Music", ArtistId = 4, GenreId = 10 },
                        new Track { Name = "Umbrella", ArtistId = 4, GenreId = 10 },
                        new Track { Name = "Work", ArtistId = 4, GenreId = 10 },
                        new Track { Name = "Rude boy", ArtistId = 4, GenreId = 10 },
                        new Track { Name = "S&M", ArtistId = 4, GenreId = 10 },
                        new Track { Name = "Kiss it better", ArtistId = 4, GenreId = 10 },
                        new Track { Name = "Disturbia", ArtistId = 4, GenreId = 10 },
                        new Track { Name = "Love me like you do", ArtistId = 8, GenreId = 10 },
                        new Track { Name = "Your song", ArtistId = 8, GenreId = 10 },
                        new Track { Name = "Lights", ArtistId = 8, GenreId = 10 },
                        new Track { Name = "TKO", ArtistId = 14, GenreId = 10 },
                        new Track { Name = "Hair up", ArtistId = 14, GenreId = 10 },
                        new Track { Name = "My love", ArtistId = 14, GenreId = 10 },
                        new Track { Name = "Mercy", ArtistId = 15, GenreId = 10 },
                        new Track { Name = "Ruin", ArtistId = 15, GenreId = 10 },
                        new Track { Name = "Imagination", ArtistId = 15, GenreId = 10 },
                        new Track { Name = "Alejandro", ArtistId = 16, GenreId = 10 },
                        new Track { Name = "Paparazzi", ArtistId = 16, GenreId = 10 },
                        new Track { Name = "Just dance", ArtistId = 16, GenreId = 10 },
                        new Track { Name = "Shape of my heart", ArtistId = 17, GenreId = 10 },
                        new Track { Name = "Fields of gold", ArtistId = 17, GenreId = 10 },
                        new Track { Name = "Fragile", ArtistId = 17, GenreId = 10 },
                        new Track { Name = "Enter sandman", ArtistId = 18, GenreId = 24 },
                        new Track { Name = "The unforgiven", ArtistId = 18, GenreId = 24 },
                        new Track { Name = "Fade to black", ArtistId = 18, GenreId = 24 },
                        new Track { Name = "One", ArtistId = 18, GenreId = 24 },
                        new Track { Name = "Hardwired", ArtistId = 18, GenreId = 24 },
                        new Track { Name = "Until it sleeps", ArtistId = 18, GenreId = 24 },
                        new Track { Name = "Enter sandman", ArtistId = 18, GenreId = 24 },
                        new Track { Name = "Mama said", ArtistId = 18, GenreId = 24 },
                        new Track { Name = "Englishman in New York", ArtistId = 17, GenreId = 10 },
                        new Track { Name = "Orbion", ArtistId = 19, GenreId = 10 },
                        new Track { Name = "Intence", ArtistId = 19, GenreId = 10 },
                        new Track { Name = "Freefall", ArtistId = 19, GenreId = 10 });
                context.SaveChanges();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        private void AddDefaultTracksPrices(ShopContext context)
        {
            if (!context.Set<TrackPrice>().Any())
            {
                context.Set<TrackPrice>()
                    .AddOrUpdate(
                        new TrackPrice { TrackId = 1, CurrencyId = 1, Price = 4.99m, PriceLevelId = 1 },
                        new TrackPrice { TrackId = 2, CurrencyId = 1, Price = 4.98m, PriceLevelId = 1 });
                context.SaveChanges();
            }
        }
    }
}