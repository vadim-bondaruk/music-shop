namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Common.Models;
    using Context;
    using Infrastructure.Enums;

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
            AddDefaultUsers(context);

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

        private void AddDefaultUsers(Context.ShopContext context)
        {
            if (!context.Set<User>().Any())
            {
                context.Set<User>().AddOrUpdate(
                        new User[]{
new User{FirstName = "Lucy", LastName = "Newman", Email = "Lucy.NEWM7632@mail2web.com", Login = "lunewman7617", Password = "9D40E02DF18047E7E2E939D41F115538618DABA3", Sex = "M", Country = "France", PhoneNumber = "(559) 302-5126", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("21.7.1997", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Amy", LastName = "Rivas", Email = "A.RIV3294@hushmail.com", Login = "amri6192", Password = "462156D34A58FEFFA2E7898D2E957F4829958CCF", Sex = "W", Country = "Germany", PhoneNumber = "(872) 543-1897", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("10.9.1972", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Randall", LastName = "Bridges", Email = "Randa.BRI6673@gmail.com", Login = "randallbri8552", Password = "D8685BAE08FAE44674398F1295E2B6DD1000ED2C", Sex = "W", Country = "France", PhoneNumber = "(229) 646-9680", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("14.10.1973", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Bridger", LastName = "Hayes", Email = "Bridger.HAYES3120@mail2web.com", Login = "bridhay4912", Password = "BBD073E98D23D47A2C8D30F714C94E127A6B5598", Sex = "W", Country = "Germany", PhoneNumber = "(256) 571-8385", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("26.7.1977", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Ansley", LastName = "Reed", Email = "Ansley.RE5375@yahoo.com", Login = "ansre4082", Password = "B71885A2DEF63C285823025E443E41D6C3923417", Sex = "M", Country = "Belarus", PhoneNumber = "(916) 504-0199", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("25.11.1995", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Brennen", LastName = "Hammond", Email = "Brenne.HAM8704@hushmail.com", Login = "brennenham6680", Password = "02780744136A9AF73F2932887B5C0328AFAEA5BA", Sex = "W", Country = "Belarus", PhoneNumber = "(216) 244-1594", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("5.7.1996", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Kolton", LastName = "Humphrey", Email = "Kol.HUMP6010@live.com", Login = "kolthumph1261", Password = "AD59769F0342B79BA55E164F690A855F1866D323", Sex = "W", Country = "England", PhoneNumber = "(312) 828-6815", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("9.11.1992", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Shawn", LastName = "Hester", Email = "Sha.HESTER7451@gmail.com", Login = "shahester1871", Password = "5644705DE35866C155801653230E5E1A79308856", Sex = "M", Country = "Australia", PhoneNumber = "(434) 968-0624", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("19.3.1986", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Luke", LastName = "Church", Email = "Luke.CHURC2472@gmail.com", Login = "lukchur6739", Password = "FDBDEFA65D8E5AA4EFBBEABCD410902CF5809A70", Sex = "W", Country = "Germany", PhoneNumber = "(828) 872-8031", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("25.5.1979", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Chris", LastName = "Glover", Email = "Chri.GLOVER8922@hushmail.com", Login = "chglover7812", Password = "C268BF6DB178101A6DD7A0A1BD88D8D3D58E62CA", Sex = "W", Country = "Australia", PhoneNumber = "(614) 818-0424", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("4.4.1996", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Zavier", LastName = "Chapman", Email = "Zavi.CHAPM8958@gmail.com", Login = "zavichapm2283", Password = "3BE142E8C94FA80DA77DB9E10A086B7DC5B9AB14", Sex = "W", Country = "Germany", PhoneNumber = "(231) 129-7359", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("18.8.1997", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Lennox", LastName = "Wolf", Email = "Len.WO6341@yahoo.com", Login = "lenwolf7991", Password = "3A7A2B4B2210AF76F6D2A95F81389097DC8B17D1", Sex = "M", Country = "Australia", PhoneNumber = "(952) 668-1401", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("15.6.1983", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Joel", LastName = "Bentley", Email = "Jo.BENTL3269@yahoo.com", Login = "joebentley2505", Password = "DE971109E69BA285987A14DE98F932B83EA6181C", Sex = "W", Country = "Belarus", PhoneNumber = "(732) 808-6315", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("10.5.1988", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Jagger", LastName = "Curry", Email = "Jag.CU8065@mail2web.com", Login = "jaggercurr8007", Password = "8C1C9F8E7CB8313D499B254CA7FE155895ACD35F", Sex = "W", Country = "Australia", PhoneNumber = "(432) 906-5582", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("3.5.1992", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Raelynn", LastName = "Ayala", Email = "Raely.AYALA6313@hushmail.com", Login = "raelynayala550", Password = "4B70410A7760752694806EB813EAC25B3FB17CCF", Sex = "W", Country = "England", PhoneNumber = "(313) 422-6097", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("27.10.1983", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Kolten", LastName = "Fuentes", Email = "Kolte.FUENTE4895@yahoo.com", Login = "koltefuent9338", Password = "87866A3C04CB4075C214B1B8FFAAA81E5EBE0584", Sex = "M", Country = "Belarus", PhoneNumber = "(684) 783-5431", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("1.4.1984", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Tyrese", LastName = "Alvarez", Email = "Tyrese.ALVAR7497@mail2web.com", Login = "tyresealvar996", Password = "03B0489191255F5361CE606D4C250A498E6D646E", Sex = "W", Country = "Belarus", PhoneNumber = "(253) 432-6551", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("12.2.1974", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Jonas", LastName = "Reid", Email = "Jonas.REID8439@live.com", Login = "jorei8636", Password = "57E330E5307257BD130FAB9D658A48B228B7760F", Sex = "M", Country = "Germany", PhoneNumber = "(310) 296-4768", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("15.5.1989", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Zane", LastName = "Bray", Email = "Zan.BRA5633@yahoo.com", Login = "zanbray3595", Password = "82DE003A674FBFA57ACCF4D7F360147A30DE4E9F", Sex = "M", Country = "England", PhoneNumber = "(805) 197-4646", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("27.6.1993", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Arielle", LastName = "Sawyer", Email = "Ariel.SAWY8081@mail2web.com", Login = "arisawyer3158", Password = "1517D20DF090DB72F758B97B94E7C0C56A4F9A08", Sex = "W", Country = "Australia", PhoneNumber = "(703) 411-8956", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("13.8.1998", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Israel", LastName = "Lamb", Email = "Isra.LAM6564@hushmail.com", Login = "israelamb8125", Password = "768E9985DA7A126E0DD3DBE14268CC19F0F8EC8D", Sex = "M", Country = "England", PhoneNumber = "(442) 407-6868", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("11.11.1982", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Zain", LastName = "Sparks", Email = "Za.SPARKS8559@hushmail.com", Login = "zaispa6086", Password = "4D6E08F2FC4D251A381672F2D87C214B387D29EC", Sex = "M", Country = "Belarus", PhoneNumber = "(651) 451-8773", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("18.10.1994", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Luz", LastName = "Rowe", Email = "L.RO8814@hushmail.com", Login = "luzro3209", Password = "1FA3B20DEB0F859DAF86835FA03AE0CF909FC47A", Sex = "W", Country = "England", PhoneNumber = "(719) 952-5315", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("3.7.1979", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Zion", LastName = "Hull", Email = "Zio.HUL9855@gmail.com", Login = "zionhul7455", Password = "2555CBB2E6AD9F9250121DEA6DCF35D6943F4D22", Sex = "M", Country = "Germany", PhoneNumber = "(954) 281-1568", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("24.1.1980", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Deven", LastName = "Maddox", Email = "Deven.MADD8080@live.com", Login = "devenmadd3132", Password = "903C2383BCFDBDA8BA353C06C6E44CCDED5A7872", Sex = "W", Country = "Australia", PhoneNumber = "(585) 939-2709", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("9.8.1991", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Samson", LastName = "Witt", Email = "Sam.WITT2872@yahoo.com", Login = "samsonwit6692", Password = "C949E24F039E1E4EC50A0ED476DBB51CDDF102D8", Sex = "M", Country = "England", PhoneNumber = "(248) 367-7672", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("20.8.1972", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Charlie", LastName = "Contreras", Email = "Charl.CONT3430@live.com", Login = "charcontre4583", Password = "7F4B3B33AB286CCF65F52ED02D6EA5A0F147CEB8", Sex = "M", Country = "England", PhoneNumber = "(727) 304-0621", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("1.10.1995", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Jaylynn", LastName = "Little", Email = "Jay.LITT1396@gmail.com", Login = "jaylylittle908", Password = "C85BF8C007FB474B8FD47B72653FCA7BACC69E7A", Sex = "W", Country = "France", PhoneNumber = "(828) 310-0253", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("11.11.1982", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Emilie", LastName = "Harrison", Email = "Emil.HARRISON2206@hushmail.com", Login = "emilieharrison", Password = "A61DFBEFA9C99BAFADB8D22B8D7118F32B50E721", Sex = "W", Country = "Germany", PhoneNumber = "(208) 484-0145", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("19.8.1982", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Vihaan", LastName = "Snyder", Email = "Vihaa.SNY2374@hushmail.com", Login = "vihsny3651", Password = "68F13943838884BC192CF6D45876B7E8FCAD1675", Sex = "M", Country = "Australia", PhoneNumber = "(731) 668-0048", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("4.10.1998", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Preston", LastName = "Bryant", Email = "Pre.BRY7126@live.com", Login = "prestbryant885", Password = "DB6C1C97A6B9739112C9EC0E2A1D615ACC1D9404", Sex = "M", Country = "England", PhoneNumber = "(804) 274-6196", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("16.6.1985", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Angeline", LastName = "Burton", Email = "Angeline.BURT7420@live.com", Login = "angelibur3952", Password = "29CF10899F89CA32DF497B84C205F35FCAD35091", Sex = "W", Country = "France", PhoneNumber = "(901) 298-3714", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("6.6.1980", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Liliana", LastName = "French", Email = "Lil.FRE3510@hushmail.com", Login = "lilianafrenc51", Password = "243D7C788706C30F98D3CE3A4163A742E31156B8", Sex = "M", Country = "Australia", PhoneNumber = "(319) 368-5935", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("13.5.1989", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Melany", LastName = "Ratliff", Email = "Mel.RATLI9465@mail2web.com", Login = "melratl8339", Password = "658F25D120ED51B0F3477FFEE7A95404C246053D", Sex = "W", Country = "France", PhoneNumber = "(816) 678-8976", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("6.2.1981", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Monica", LastName = "Pope", Email = "Mon.PO2187@gmail.com", Login = "monipo3924", Password = "8AEC4773FB5DBC6C41C97455584595CABE4570A9", Sex = "W", Country = "Belarus", PhoneNumber = "(432) 792-5372", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("8.3.1976", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Ansley", LastName = "Molina", Email = "Ansl.MOL6315@live.com", Login = "anslmolin2696", Password = "EB580EEB23065C584B23005CEE47E50F3385BEAB", Sex = "W", Country = "Australia", PhoneNumber = "(682) 256-5614", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("14.5.1995", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Carmelo", LastName = "Valencia", Email = "Carmel.VALENCIA6645@hushmail.com", Login = "carmevalenci90", Password = "2B80DB86A92DDD38D0F46A0A69659B04C3160C6D", Sex = "W", Country = "France", PhoneNumber = "(803) 862-7041", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("17.4.1975", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Kaelynn", LastName = "Dawson", Email = "Kaelyn.DAWSO1809@yahoo.com", Login = "kaelyndaws5236", Password = "12906A2790B903D2067E4412C18E84314DF1E8F5", Sex = "M", Country = "Belarus", PhoneNumber = "(317) 667-1760", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("17.4.1979", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Krish", LastName = "Maynard", Email = "Krish.MAYNARD4522@hushmail.com", Login = "krishmaynard26", Password = "34BC906ABCFF203CA09EA4F466B5EB817CAEB2A8", Sex = "M", Country = "Germany", PhoneNumber = "(786) 981-4746", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("22.1.1987", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Raegan", LastName = "Espinoza", Email = "Rae.ESPINOZ1158@hushmail.com", Login = "raegaespinoza8", Password = "83926DD4722D759CE9F493E50E4AC128168DDDC5", Sex = "W", Country = "Australia", PhoneNumber = "(276) 364-0085", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("24.7.1972", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Layton", LastName = "Roman", Email = "Layto.ROMAN8340@yahoo.com", Login = "layroma2879", Password = "54993445325C1B87F93D2E00EC5C1D9D0A2541C6", Sex = "W", Country = "Belarus", PhoneNumber = "(615) 228-9216", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("15.3.1989", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Crosby", LastName = "Lowe", Email = "Cros.LO8763@gmail.com", Login = "croslowe2437", Password = "7BA36DDCCA550735FC96B464E36822C884548CA6", Sex = "M", Country = "Australia", PhoneNumber = "(551) 433-8782", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("9.2.1983", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Lena", LastName = "Keith", Email = "Lena.KEIT8127@mail2web.com", Login = "lenakeit6440", Password = "D64CE902641AA396DE01374AC3E4F47AA9BC853E", Sex = "W", Country = "France", PhoneNumber = "(539) 882-0070", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("10.11.1985", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Turner", LastName = "Sellers", Email = "Turn.SELLER4149@hushmail.com", Login = "turneseller197", Password = "3F32A7645E190DD2F7BA88D4FD3B844144E981CC", Sex = "W", Country = "England", PhoneNumber = "(458) 423-1497", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("13.2.1987", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Lillie", LastName = "Mack", Email = "Lil.MA4185@yahoo.com", Login = "lillimac5541", Password = "6B215E4981322BB6311DAB7D773C6F9F84B3D0F1", Sex = "W", Country = "Belarus", PhoneNumber = "(765) 430-0238", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("25.3.1970", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Turner", LastName = "Sullivan", Email = "Turne.SULLIV7308@hushmail.com", Login = "tursullivan686", Password = "775DB3693DE4F626A4D7806797E2D58996494ACE", Sex = "W", Country = "Australia", PhoneNumber = "(865) 277-2224", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("12.7.1997", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Braylon", LastName = "Beck", Email = "Braylon.BE5466@mail2web.com", Login = "braylbe6464", Password = "C7D508C8CBB5E32DA3D686CA183ACF534941F1D9", Sex = "M", Country = "Australia", PhoneNumber = "(626) 548-9429", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("17.7.1994", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Santino", LastName = "Cummings", Email = "Santi.CUMMIN4063@hushmail.com", Login = "santincumming7", Password = "BD00485C8F06A4532DC3B362B5965BA04B87DDB3", Sex = "M", Country = "Germany", PhoneNumber = "(517) 516-0282", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("19.7.1980", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Lilia", LastName = "Burch", Email = "Lilia.BURCH8421@live.com", Login = "lilburch1327", Password = "C9B8B6D7637F71CCE20C87335C945CA57371502D", Sex = "M", Country = "Germany", PhoneNumber = "(910) 540-0650", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("10.11.1980", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "London", LastName = "Emerson", Email = "London.EMERSO9825@hushmail.com", Login = "londoeme3609", Password = "DE7D735617E4CC11BC7ADD1074431B04AAC33541", Sex = "M", Country = "England", PhoneNumber = "(956) 171-3306", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("25.6.1996", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Joey", LastName = "Bradshaw", Email = "Jo.BRAD9503@mail2web.com", Login = "jobrads3096", Password = "43792721864D83618A00FCFDF73953FEC7B307EE", Sex = "W", Country = "Australia", PhoneNumber = "(251) 214-9809", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("3.4.1970", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Arturo", LastName = "Morrow", Email = "Art.MORRO7437@live.com", Login = "artumorr6001", Password = "CB541EACB4B05AA1A83FE96C3E8B2431C63F2E00", Sex = "M", Country = "Germany", PhoneNumber = "(513) 956-9771", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("7.10.1970", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Braylee", LastName = "Dennis", Email = "Braylee.DENNIS9102@live.com", Login = "braydenni8376", Password = "5262BF67CB268E8FF9828D9EB9903456CF1D5F21", Sex = "M", Country = "Belarus", PhoneNumber = "(830) 235-0722", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("15.9.1987", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Khloe", LastName = "Brady", Email = "Khl.BRADY8409@mail2web.com", Login = "khlbrad1771", Password = "430CD3C406EECFA5942814FFA83C2EBCFE13EA49", Sex = "M", Country = "England", PhoneNumber = "(949) 715-3167", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("12.10.1992", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Sonia", LastName = "Spears", Email = "So.SPE1519@hushmail.com", Login = "sospears5641", Password = "32FBA45E5DCD97BCF64ADD808F788AE7553CC90E", Sex = "M", Country = "Belarus", PhoneNumber = "(432) 154-7128", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("6.3.1994", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Renee", LastName = "Parker", Email = "Re.PARKE7585@gmail.com", Login = "repark3071", Password = "D0DCFF7D8A4102657DF9FB14EEDA979068171825", Sex = "W", Country = "Belarus", PhoneNumber = "(425) 519-8460", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("25.11.1985", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Leonard", LastName = "Stokes", Email = "Leo.STOKE3947@live.com", Login = "leostoke9830", Password = "9D275A9D8E23187BBCF9D41F9E274570EECC909D", Sex = "M", Country = "France", PhoneNumber = "(574) 128-6392", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("12.9.1978", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Sylvia", LastName = "Norris", Email = "Syl.NORRI7128@mail2web.com", Login = "sylvianorr5131", Password = "6BA20EFD393188B2602C299B0DB695DF3CE849CF", Sex = "M", Country = "Germany", PhoneNumber = "(607) 945-0796", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("5.10.1992", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Madelynn", LastName = "Roth", Email = "Madel.RO1086@live.com", Login = "maderot4064", Password = "D64CCE81E42686BA0BC45184A448C6BFF51B7FE2", Sex = "W", Country = "Germany", PhoneNumber = "(920) 542-2836", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("27.10.1978", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Virginia", LastName = "Perry", Email = "Virgi.PERRY6471@mail2web.com", Login = "virginiperr528", Password = "7A2426130F697A4E233BE1E903F9BA6B95D3D4AD", Sex = "M", Country = "Belarus", PhoneNumber = "(252) 529-8787", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("9.1.1983", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Louis", LastName = "Douglas", Email = "Lou.DOUG2533@mail2web.com", Login = "loudou1484", Password = "62542C52831BC3667A934E5BD6DC50F390B1444F", Sex = "W", Country = "France", PhoneNumber = "(630) 447-2101", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("23.2.1980", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Blakely", LastName = "Morales", Email = "Blakely.MORAL6277@hushmail.com", Login = "blakemorales44", Password = "DA0654977BF314B014353FF21B3BAC2A4CFCFBD9", Sex = "M", Country = "Germany", PhoneNumber = "(518) 614-2719", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("23.9.1972", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Phoebe", LastName = "Reid", Email = "Phoe.REI2270@gmail.com", Login = "phoebreid6853", Password = "FE2D73B76E1139D60809DE645075C1E8A249F424", Sex = "W", Country = "France", PhoneNumber = "(260) 551-6530", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("21.4.1986", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Presley", LastName = "Ellison", Email = "Pres.ELLI2740@hushmail.com", Login = "presleyellison", Password = "EE4ADABDED66CF2AAC7DBB9C3AB7E9A0F8851405", Sex = "W", Country = "England", PhoneNumber = "(574) 351-7873", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("14.1.1978", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Rivka", LastName = "Mcdowell", Email = "Rivka.MCDOW2475@hushmail.com", Login = "rivmcdow6221", Password = "0CA17718835CF9B8CF0DD0781A6D2679C895EFC5", Sex = "M", Country = "Australia", PhoneNumber = "(636) 837-8565", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("3.3.1983", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Matias", LastName = "Holland", Email = "Mat.HOLLA4405@live.com", Login = "matiashollan64", Password = "99C258F135C004D44E2CFB40FF99F106B1371FB6", Sex = "W", Country = "France", PhoneNumber = "(206) 358-9151", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("20.7.1983", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Kaylyn", LastName = "Hancock", Email = "Kaylyn.HANCOC1797@gmail.com", Login = "kaylhan6775", Password = "7BB968526BB41D1DF289B5E220EA868369DFA2EB", Sex = "M", Country = "Belarus", PhoneNumber = "(865) 283-8208", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("17.11.1987", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Michaela", LastName = "Howard", Email = "Micha.HOWAR7077@live.com", Login = "michaehoward20", Password = "BD0C159C160FFE5C021DBD505E96AB9B39CBB79F", Sex = "W", Country = "France", PhoneNumber = "(559) 824-2944", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("17.10.1996", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Roman", LastName = "Wagner", Email = "Rom.WAGNE3232@gmail.com", Login = "romanwag3392", Password = "481C7694944A97891B60F58302E3EDBE76A86056", Sex = "W", Country = "Australia", PhoneNumber = "(504) 520-4283", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("13.3.1996", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Jaylyn", LastName = "Cortez", Email = "Jaylyn.CORTEZ5145@gmail.com", Login = "jaycortez4800", Password = "5CA743EE30BB68CD5AC1EB813983D404CAC39D00", Sex = "W", Country = "Germany", PhoneNumber = "(917) 488-0209", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("23.3.1973", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Kaylen", LastName = "Mcleod", Email = "Kaylen.MCLEO6387@live.com", Login = "kaylemcleo2855", Password = "EC71F56F9E6067B7B36D2C870EE8F29BE55AF2A7", Sex = "W", Country = "Germany", PhoneNumber = "(660) 188-4899", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("3.11.1997", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Aliyah", LastName = "Gonzales", Email = "Aliyah.GONZALE5360@gmail.com", Login = "aliyahgonzale8", Password = "A00A691E0BD55DCDEF7FB65F5F42FEE61B22FD52", Sex = "M", Country = "Australia", PhoneNumber = "(325) 703-2160", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("7.6.1973", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Rene", LastName = "Finley", Email = "Ren.FINLE3307@gmail.com", Login = "refin3034", Password = "32B1464EF3CE241CB00E80A94045545B75FA6D1E", Sex = "W", Country = "Australia", PhoneNumber = "(309) 644-5485", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("3.10.1980", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Laney", LastName = "Frye", Email = "Lane.FRYE1785@live.com", Login = "lafry3283", Password = "052462375F2AA7DC84275E5214A49570BF0B4113", Sex = "M", Country = "France", PhoneNumber = "(406) 622-7150", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("13.1.1999", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Jackson", LastName = "Dejesus", Email = "Jacks.DEJ8583@yahoo.com", Login = "jackdejes7933", Password = "164C3AE05E68504A0228166C58FEC96BADD45FA1", Sex = "W", Country = "England", PhoneNumber = "(970) 612-2126", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("2.2.1982", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Kylie", LastName = "Cook", Email = "Kyli.CO5361@live.com", Login = "kylicook4655", Password = "D4C02B5BA02D89FA86D12D1CA2B3967A6A0D12CE", Sex = "M", Country = "France", PhoneNumber = "(339) 276-8726", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("17.11.1975", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Rivka", LastName = "Rowland", Email = "Rivka.ROWLA7037@mail2web.com", Login = "rivkrowlan4499", Password = "35BE04BCD29A3DAA763CBCEE6E8421C9958A5DC9", Sex = "M", Country = "England", PhoneNumber = "(412) 345-7951", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("22.11.1989", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Semaj", LastName = "Robbins", Email = "Sem.ROB5290@gmail.com", Login = "serob7528", Password = "AA1C012EC950C4FD80F5A31C7186147749B49EF4", Sex = "W", Country = "England", PhoneNumber = "(410) 349-3933", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("26.2.1988", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Arely", LastName = "Phelps", Email = "Ar.PHELP3209@live.com", Login = "arephelp1098", Password = "2B3266B1F3C7396648F2D50879757FBEA2E48D7E", Sex = "W", Country = "Germany", PhoneNumber = "(320) 327-0957", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("9.7.1981", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Charlie", LastName = "Hoover", Email = "Charli.HOO1318@mail2web.com", Login = "charhoov6215", Password = "7F710F97DAC20F7974239CAFD4286E91CAE087F0", Sex = "W", Country = "England", PhoneNumber = "(325) 118-3016", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("16.3.1986", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Ariana", LastName = "Holland", Email = "Ariana.HOLLAN8232@live.com", Login = "arianhol4044", Password = "87EC3A400B16119017DFE62DD0F1644AE2F8BE39", Sex = "M", Country = "Belarus", PhoneNumber = "(732) 197-8730", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("15.5.1992", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Edith", LastName = "Mcmahon", Email = "Edith.MCMAHON5269@hushmail.com", Login = "edimcmah7262", Password = "FEB174ADB373F4666184AFAFCBA5887B19009177", Sex = "W", Country = "Belarus", PhoneNumber = "(239) 694-1077", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("22.4.1993", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Sergio", LastName = "Curtis", Email = "Serg.CURTI3319@live.com", Login = "sergicurtis513", Password = "DD35A3E33E802059E095CDB228997842911772BF", Sex = "M", Country = "England", PhoneNumber = "(939) 453-2105", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("26.9.1993", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Temperance", LastName = "Lowery", Email = "Tempe.LOW5437@mail2web.com", Login = "tempelower7098", Password = "8460036AC5A5D0426FEB0BEC531FA5182911D46D", Sex = "M", Country = "Belarus", PhoneNumber = "(207) 186-7455", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("9.9.1976", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Wyatt", LastName = "Wong", Email = "Wya.WO4057@mail2web.com", Login = "wyawon1238", Password = "D35541CE6B58DBD46E847211EDCD10BA990ECAA1", Sex = "W", Country = "England", PhoneNumber = "(228) 769-0021", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("23.5.1979", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Skylar", LastName = "Stewart", Email = "Sky.STEW6239@yahoo.com", Login = "skylstewart643", Password = "85C09C2CE5F654977BA9168E3B524572BBAE53D6", Sex = "M", Country = "Australia", PhoneNumber = "(334) 812-2283", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("17.3.1990", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Damarion", LastName = "Warren", Email = "Damarion.WARREN3550@live.com", Login = "damarwarren485", Password = "B2DB010444186340599D19EFC347DE7F2A3EF422", Sex = "M", Country = "Australia", PhoneNumber = "(218) 431-6539", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("24.10.1999", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Gia", LastName = "Solomon", Email = "G.SOLO2457@live.com", Login = "giasolo3919", Password = "1C2F8C2D3EE30AF036EB7E698FB4398866057E3A", Sex = "M", Country = "England", PhoneNumber = "(408) 687-8407", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("5.11.1992", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Carleigh", LastName = "Delgado", Email = "Carlei.DELGAD9220@gmail.com", Login = "carldelgad3092", Password = "A5A87538F7180EC42E2617424DB61DA6CD6ED6F7", Sex = "M", Country = "France", PhoneNumber = "(931) 271-9591", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("9.11.1986", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Sophie", LastName = "Zamora", Email = "Sophi.ZAM8845@live.com", Login = "sophiezam6682", Password = "71D9538B2D57DE5551472A5B0CFA6F601ED213F8", Sex = "M", Country = "Australia", PhoneNumber = "(850) 909-8182", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("25.7.1983", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Jagger", LastName = "Perez", Email = "Jagge.PE7617@mail2web.com", Login = "jaggpere5958", Password = "E132924DEE02E4C3FB34336C1B411259B8291DF7", Sex = "M", Country = "Belarus", PhoneNumber = "(831) 334-2982", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("8.11.1976", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Masen", LastName = "Hodge", Email = "Masen.HODG1687@gmail.com", Login = "mahodg9727", Password = "711AE724631B4FD034E0747D73982ED8CAB9E442", Sex = "W", Country = "France", PhoneNumber = "(858) 740-3306", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("27.11.1975", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Alena", LastName = "James", Email = "Alena.JA4906@mail2web.com", Login = "alejames6804", Password = "711A904EAB5DD7785261F3FB433144C9E84E701A", Sex = "M", Country = "England", PhoneNumber = "(712) 332-1113", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("6.4.1979", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Kennedy", LastName = "Potter", Email = "Ken.POTTER2880@hushmail.com", Login = "kenpott6578", Password = "F19909712E7062662978A5EDD4971FD78586D3EB", Sex = "W", Country = "Germany", PhoneNumber = "(903) 511-1287", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("11.6.1982", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Lucille", LastName = "Oneal", Email = "Lucill.ONEA3748@hushmail.com", Login = "lucioneal9464", Password = "04A6AEB2115B9A56DC0F66967D468C205DD9C9C3", Sex = "M", Country = "Belarus", PhoneNumber = "(636) 296-5462", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("1.4.1994", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Ezekiel", LastName = "Santos", Email = "Ezekie.SANT5762@yahoo.com", Login = "ezekiesantos29", Password = "71656C2062D92DBD37E8CB9C5799F479E204963A", Sex = "W", Country = "Belarus", PhoneNumber = "(747) 176-9086", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("2.5.1984", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Finley", LastName = "Trujillo", Email = "Finle.TRUJIL3397@mail2web.com", Login = "finltrujil8466", Password = "6CEE1DFC0793B98EC94E1A406F86E23C9AB65FBC", Sex = "W", Country = "Australia", PhoneNumber = "(817) 531-9253", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("13.9.1987", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Romeo", LastName = "Meyers", Email = "Rome.MEY1155@yahoo.com", Login = "romey7443", Password = "D9C75AB1201361F6D5E7F146F9D604606BA72F71", Sex = "W", Country = "France", PhoneNumber = "(458) 579-4933", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("15.6.1982", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Jazlynn", LastName = "Albert", Email = "Jazly.ALBER7602@hushmail.com", Login = "jazlynnalber94", Password = "F2EC2D9B18E561B00BE7054672FFC742AFA5FEA7", Sex = "M", Country = "France", PhoneNumber = "(785) 500-1518", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("25.6.1984", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "Omar", LastName = "Carroll", Email = "Oma.CARROLL6931@mail2web.com", Login = "omarcarro5105", Password = "2C3EFA2549824661B1EC83B299275193C68D3ED7", Sex = "M", Country = "England", PhoneNumber = "(646) 476-8357", IsDeleted = false, UserRole = 0, ConfirmedEmail = true, BirthDate= DateTime.Parse("2.9.1997", new System.Globalization.CultureInfo("ru-ru", true))},
new User{FirstName = "test_user", LastName = "test_user", Email = "test_user@gmail.com", Login = "test_user", Password = "DCF05BF73C524F3E32C08ACF55BE3EC4170FFED5", Sex = "M", Country = "Belarus", PhoneNumber = "(29) 227 02 83", IsDeleted = false, UserRole = UserRoles.Customer, ConfirmedEmail = true},
new User{FirstName = "test_admin", LastName = "test_admin", Email = "test_admin@gmail.com", Login = "test_admin", Password = "AA7130C1A9D96562ADE20DC00E38360B1C402975", Sex = "M", Country = "Belarus", PhoneNumber = "(29) 227 02 83", IsDeleted = false, UserRole = UserRoles.Admin, ConfirmedEmail = true}
        }); 
            }

            context.SaveChanges();
        }
    }
}
