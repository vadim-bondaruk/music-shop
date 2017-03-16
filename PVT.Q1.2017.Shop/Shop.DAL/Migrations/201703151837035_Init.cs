namespace Shop.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// </summary>
    public partial class Init : DbMigration
    {
        /// <summary>
        /// </summary>
        public override void Down()
        {
            this.DropForeignKey("dbo.tbAlbumPrices", "PriceLevelId", "dbo.tbPriceLevels");
            this.DropForeignKey("dbo.tbAlbumPrices", "CurrencyId", "dbo.tbCurrencies");
            this.DropForeignKey("dbo.tbAlbumPrices", "AlbumId", "dbo.tbAlbums");
            this.DropForeignKey("dbo.tbAlbums", "ArtistId", "dbo.tbArtists");
            this.DropForeignKey("dbo.tbTracks", "GenreId", "dbo.tbGenres");
            this.DropForeignKey("dbo.tbFeedbacks", "UserId", "dbo.tbUsers");
            this.DropForeignKey("dbo.tbVotes", "UserId", "dbo.tbUsers");
            this.DropForeignKey("dbo.tbVotes", "TrackId", "dbo.tbTracks");
            this.DropForeignKey("dbo.tbUsers", "CurrencyId", "dbo.tbCurrencies");
            this.DropForeignKey("dbo.tbUsers", "PriceLevelId", "dbo.tbPriceLevels");
            this.DropForeignKey("dbo.tbTrackPrices", "TrackId", "dbo.tbTracks");
            this.DropForeignKey("dbo.tbTrackPrices", "PriceLevelId", "dbo.tbPriceLevels");
            this.DropForeignKey("dbo.tbTrackPrices", "CurrencyId", "dbo.tbCurrencies");
            this.DropForeignKey("dbo.tbCurrencyRates", "TargetCurrencyId", "dbo.tbCurrencies");
            this.DropForeignKey("dbo.tbCurrencyRates", "CurrencyId", "dbo.tbCurrencies");
            this.DropForeignKey("dbo.tbFeedbacks", "TrackId", "dbo.tbTracks");
            this.DropForeignKey("dbo.tbTracks", "ArtistId", "dbo.tbArtists");
            this.DropForeignKey("dbo.tbAlbumTrackRelations", "TrackId", "dbo.tbTracks");
            this.DropForeignKey("dbo.tbAlbumTrackRelations", "AlbumId", "dbo.tbAlbums");
            this.DropIndex("dbo.tbVotes", new[] { "UserId" });
            this.DropIndex("dbo.tbVotes", new[] { "TrackId" });
            this.DropIndex("dbo.tbCurrencyRates", new[] { "TargetCurrencyId" });
            this.DropIndex("dbo.tbCurrencyRates", new[] { "CurrencyId" });
            this.DropIndex("dbo.tbCurrencies", "UniqueCurrencyCode_Index");
            this.DropIndex("dbo.tbCurrencies", "UniqueCurrencyName_Index");
            this.DropIndex("dbo.tbTrackPrices", new[] { "CurrencyId" });
            this.DropIndex("dbo.tbTrackPrices", new[] { "PriceLevelId" });
            this.DropIndex("dbo.tbTrackPrices", new[] { "TrackId" });
            this.DropIndex("dbo.tbUsers", new[] { "PriceLevelId" });
            this.DropIndex("dbo.tbUsers", new[] { "CurrencyId" });
            this.DropIndex("dbo.tbFeedbacks", new[] { "UserId" });
            this.DropIndex("dbo.tbFeedbacks", new[] { "TrackId" });
            this.DropIndex("dbo.tbAlbumTrackRelations", "UniqueRelation_Index");
            this.DropIndex("dbo.tbTracks", new[] { "GenreId" });
            this.DropIndex("dbo.tbTracks", new[] { "ArtistId" });
            this.DropIndex("dbo.tbAlbums", new[] { "ArtistId" });
            this.DropIndex("dbo.tbAlbumPrices", new[] { "CurrencyId" });
            this.DropIndex("dbo.tbAlbumPrices", new[] { "PriceLevelId" });
            this.DropIndex("dbo.tbAlbumPrices", new[] { "AlbumId" });
            this.DropTable("dbo.tbGenres");
            this.DropTable("dbo.tbVotes");
            this.DropTable("dbo.tbCurrencyRates");
            this.DropTable("dbo.tbCurrencies");
            this.DropTable("dbo.tbTrackPrices");
            this.DropTable("dbo.tbPriceLevels");
            this.DropTable("dbo.tbUsers");
            this.DropTable("dbo.tbFeedbacks");
            this.DropTable("dbo.tbAlbumTrackRelations");
            this.DropTable("dbo.tbTracks");
            this.DropTable("dbo.tbArtists");
            this.DropTable("dbo.tbAlbums");
            this.DropTable("dbo.tbAlbumPrices");
        }

        /// <summary>
        /// </summary>
        public override void Up()
        {
            this.CreateTable(
                    "dbo.tbAlbumPrices",
                    c =>
                        new
                            {
                                Id = c.Int(false, true),
                                Price = c.Decimal(false, 18, 2),
                                AlbumId = c.Int(false),
                                PriceLevelId = c.Int(false),
                                CurrencyId = c.Int(false),
                                IsDeleted = c.Boolean(false)
                            })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbAlbums", t => t.AlbumId, true)
                .ForeignKey("dbo.tbCurrencies", t => t.CurrencyId, true)
                .ForeignKey("dbo.tbPriceLevels", t => t.PriceLevelId, true)
                .Index(t => t.AlbumId)
                .Index(t => t.PriceLevelId)
                .Index(t => t.CurrencyId);

            this.CreateTable(
                    "dbo.tbAlbums",
                    c =>
                        new
                            {
                                Id = c.Int(false, true),
                                Name = c.String(false, 150),
                                Cover = c.Binary(),
                                ReleaseDate = c.DateTime(),
                                ArtistId = c.Int(),
                                IsDeleted = c.Boolean(false)
                            })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbArtists", t => t.ArtistId)
                .Index(t => t.ArtistId);

            this.CreateTable(
                "dbo.tbArtists",
                c =>
                    new
                        {
                            Id = c.Int(false, true),
                            Name = c.String(false, 150),
                            Biography = c.String(),
                            Birthday = c.DateTime(),
                            Photo = c.Binary(),
                            IsDeleted = c.Boolean(false)
                        }).PrimaryKey(t => t.Id);

            this.CreateTable(
                    "dbo.tbTracks",
                    c =>
                        new
                            {
                                Id = c.Int(false, true),
                                Name = c.String(false, 150),
                                ReleaseDate = c.DateTime(),
                                Image = c.Binary(),
                                TrackFile = c.Binary(),
                                Duration = c.Time(precision: 7),
                                ArtistId = c.Int(),
                                GenreId = c.Int(),
                                IsDeleted = c.Boolean(false)
                            })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbArtists", t => t.ArtistId)
                .ForeignKey("dbo.tbGenres", t => t.GenreId)
                .Index(t => t.ArtistId)
                .Index(t => t.GenreId);

            this.CreateTable(
                    "dbo.tbAlbumTrackRelations",
                    c =>
                        new
                            {
                                Id = c.Int(false, true),
                                AlbumId = c.Int(false),
                                TrackId = c.Int(false),
                                IsDeleted = c.Boolean(false)
                            })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbAlbums", t => t.AlbumId, true)
                .ForeignKey("dbo.tbTracks", t => t.TrackId, true)
                .Index(t => new { t.AlbumId, t.TrackId }, unique: true, name: "UniqueRelation_Index");

            this.CreateTable(
                    "dbo.tbFeedbacks",
                    c =>
                        new
                            {
                                Id = c.Int(false, true),
                                Comments = c.String(false),
                                TrackId = c.Int(false),
                                UserId = c.Int(false),
                                IsDeleted = c.Boolean(false)
                            })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbTracks", t => t.TrackId, true)
                .ForeignKey("dbo.tbUsers", t => t.UserId, true)
                .Index(t => t.TrackId)
                .Index(t => t.UserId);

            this.CreateTable(
                    "dbo.tbUsers",
                    c =>
                        new
                            {
                                Id = c.Int(false, true),
                                IdentityKey = c.String(false, 128),
                                Dicount = c.Double(),
                                CurrencyId = c.Int(false),
                                PriceLevelId = c.Int(false),
                                IsDeleted = c.Boolean(false)
                            })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbPriceLevels", t => t.PriceLevelId, true)
                .ForeignKey("dbo.tbCurrencies", t => t.CurrencyId)
                .Index(t => t.CurrencyId)
                .Index(t => t.PriceLevelId);

            this.CreateTable(
                    "dbo.tbPriceLevels",
                    c => new { Id = c.Int(false, true), Name = c.String(false, 150), IsDeleted = c.Boolean(false) })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
                    "dbo.tbTrackPrices",
                    c =>
                        new
                            {
                                Id = c.Int(false, true),
                                Price = c.Decimal(false, 18, 2),
                                TrackId = c.Int(false),
                                PriceLevelId = c.Int(false),
                                CurrencyId = c.Int(false),
                                IsDeleted = c.Boolean(false)
                            })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbCurrencies", t => t.CurrencyId)
                .ForeignKey("dbo.tbPriceLevels", t => t.PriceLevelId)
                .ForeignKey("dbo.tbTracks", t => t.TrackId)
                .Index(t => t.TrackId)
                .Index(t => t.PriceLevelId)
                .Index(t => t.CurrencyId);

            this.CreateTable(
                    "dbo.tbCurrencies",
                    c =>
                        new
                            {
                                Id = c.Int(false, true),
                                ShortName = c.String(false, 3),
                                FullName = c.String(maxLength: 150),
                                Code = c.Int(false),
                                Symbol = c.String(maxLength: 10),
                                IsDeleted = c.Boolean(false)
                            })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ShortName, unique: true, name: "UniqueCurrencyName_Index")
                .Index(t => t.Code, unique: true, name: "UniqueCurrencyCode_Index");

            this.CreateTable(
                    "dbo.tbCurrencyRates",
                    c =>
                        new
                            {
                                Id = c.Int(false, true),
                                CrossCourse = c.Decimal(false, 18, 2),
                                Date = c.DateTime(false),
                                CurrencyId = c.Int(false),
                                TargetCurrencyId = c.Int(false),
                                IsDeleted = c.Boolean(false)
                            })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbCurrencies", t => t.CurrencyId)
                .ForeignKey("dbo.tbCurrencies", t => t.TargetCurrencyId)
                .Index(t => t.CurrencyId)
                .Index(t => t.TargetCurrencyId);

            this.CreateTable(
                    "dbo.tbVotes",
                    c =>
                        new
                            {
                                Id = c.Int(false, true),
                                Mark = c.Int(false),
                                TrackId = c.Int(false),
                                UserId = c.Int(false),
                                IsDeleted = c.Boolean(false)
                            })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbTracks", t => t.TrackId)
                .ForeignKey("dbo.tbUsers", t => t.UserId)
                .Index(t => t.TrackId)
                .Index(t => t.UserId);

            this.CreateTable(
                "dbo.tbGenres",
                c =>
                    new
                        {
                            Id = c.Int(false, true),
                            Description = c.String(),
                            Name = c.String(false, 150),
                            IsDeleted = c.Boolean(false)
                        }).PrimaryKey(t => t.Id);
        }
    }
}