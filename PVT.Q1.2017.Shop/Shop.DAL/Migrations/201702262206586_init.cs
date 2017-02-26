namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbAlbumPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrencyId = c.Int(nullable: false),
                        AlbumId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbAlbums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.tbCurrencies", t => t.CurrencyId, cascadeDelete: true)
                .Index(t => t.CurrencyId)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.tbAlbums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cover = c.Binary(),
                        Name = c.String(nullable: false, maxLength: 150),
                        ReleaseDate = c.DateTime(),
                        ArtistId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbArtists", t => t.ArtistId)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.tbArtists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Biography = c.String(),
                        Birthday = c.DateTime(),
                        Photo = c.Binary(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbTracks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        AlbumId = c.Int(),
                        ArtistId = c.Int(),
                        Duration = c.Time(precision: 7),
                        GenreId = c.Int(),
                        Image = c.Binary(),
                        TrackFile = c.Binary(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbAlbums", t => t.AlbumId)
                .ForeignKey("dbo.tbArtists", t => t.ArtistId)
                .ForeignKey("dbo.tbGenres", t => t.GenreId)
                .Index(t => t.AlbumId)
                .Index(t => t.ArtistId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.tbFeedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comments = c.String(nullable: false),
                        TrackId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbTracks", t => t.TrackId, cascadeDelete: true)
                .ForeignKey("dbo.tbUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TrackId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tbUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrencyId = c.Int(nullable: false),
                        PriceLevelId = c.Int(nullable: false),
                        IdentityKey = c.String(nullable: false, maxLength: 128),
                        Dicount = c.Double(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbPriceLevels", t => t.PriceLevelId, cascadeDelete: true)
                .ForeignKey("dbo.tbCurrencies", t => t.CurrencyId)
                .Index(t => t.CurrencyId)
                .Index(t => t.PriceLevelId);
            
            CreateTable(
                "dbo.tbPriceLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbCurrencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortName = c.String(nullable: false, maxLength: 3),
                        FullName = c.String(nullable: false, maxLength: 150),
                        Code = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbCurrencyRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CrossCourse = c.Double(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        TargetCurrencyId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbCurrencies", t => t.CurrencyId)
                .ForeignKey("dbo.tbCurrencies", t => t.TargetCurrencyId)
                .Index(t => t.CurrencyId)
                .Index(t => t.TargetCurrencyId);
            
            CreateTable(
                "dbo.tbTrackPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrencyId = c.Int(nullable: false),
                        TrackId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbCurrencies", t => t.CurrencyId)
                .ForeignKey("dbo.tbTracks", t => t.TrackId)
                .Index(t => t.CurrencyId)
                .Index(t => t.TrackId);
            
            CreateTable(
                "dbo.tbVotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MarkId = c.Int(nullable: false),
                        TrackId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Mark = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbTracks", t => t.TrackId)
                .ForeignKey("dbo.tbUsers", t => t.UserId)
                .Index(t => t.TrackId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tbGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbAlbumPrices", "CurrencyId", "dbo.tbCurrencies");
            DropForeignKey("dbo.tbAlbumPrices", "AlbumId", "dbo.tbAlbums");
            DropForeignKey("dbo.tbAlbums", "ArtistId", "dbo.tbArtists");
            DropForeignKey("dbo.tbTracks", "GenreId", "dbo.tbGenres");
            DropForeignKey("dbo.tbFeedbacks", "UserId", "dbo.tbUsers");
            DropForeignKey("dbo.tbVotes", "UserId", "dbo.tbUsers");
            DropForeignKey("dbo.tbVotes", "TrackId", "dbo.tbTracks");
            DropForeignKey("dbo.tbUsers", "CurrencyId", "dbo.tbCurrencies");
            DropForeignKey("dbo.tbTrackPrices", "TrackId", "dbo.tbTracks");
            DropForeignKey("dbo.tbTrackPrices", "CurrencyId", "dbo.tbCurrencies");
            DropForeignKey("dbo.tbCurrencyRates", "TargetCurrencyId", "dbo.tbCurrencies");
            DropForeignKey("dbo.tbCurrencyRates", "CurrencyId", "dbo.tbCurrencies");
            DropForeignKey("dbo.tbUsers", "PriceLevelId", "dbo.tbPriceLevels");
            DropForeignKey("dbo.tbFeedbacks", "TrackId", "dbo.tbTracks");
            DropForeignKey("dbo.tbTracks", "ArtistId", "dbo.tbArtists");
            DropForeignKey("dbo.tbTracks", "AlbumId", "dbo.tbAlbums");
            DropIndex("dbo.tbVotes", new[] { "UserId" });
            DropIndex("dbo.tbVotes", new[] { "TrackId" });
            DropIndex("dbo.tbTrackPrices", new[] { "TrackId" });
            DropIndex("dbo.tbTrackPrices", new[] { "CurrencyId" });
            DropIndex("dbo.tbCurrencyRates", new[] { "TargetCurrencyId" });
            DropIndex("dbo.tbCurrencyRates", new[] { "CurrencyId" });
            DropIndex("dbo.tbUsers", new[] { "PriceLevelId" });
            DropIndex("dbo.tbUsers", new[] { "CurrencyId" });
            DropIndex("dbo.tbFeedbacks", new[] { "UserId" });
            DropIndex("dbo.tbFeedbacks", new[] { "TrackId" });
            DropIndex("dbo.tbTracks", new[] { "GenreId" });
            DropIndex("dbo.tbTracks", new[] { "ArtistId" });
            DropIndex("dbo.tbTracks", new[] { "AlbumId" });
            DropIndex("dbo.tbAlbums", new[] { "ArtistId" });
            DropIndex("dbo.tbAlbumPrices", new[] { "AlbumId" });
            DropIndex("dbo.tbAlbumPrices", new[] { "CurrencyId" });
            DropTable("dbo.tbGenres");
            DropTable("dbo.tbVotes");
            DropTable("dbo.tbTrackPrices");
            DropTable("dbo.tbCurrencyRates");
            DropTable("dbo.tbCurrencies");
            DropTable("dbo.tbPriceLevels");
            DropTable("dbo.tbUsers");
            DropTable("dbo.tbFeedbacks");
            DropTable("dbo.tbTracks");
            DropTable("dbo.tbArtists");
            DropTable("dbo.tbAlbums");
            DropTable("dbo.tbAlbumPrices");
        }
    }
}
