namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbAlbumPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlbumId = c.Int(nullable: false),
                        PriceLevelId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbAlbums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.tbCurrencies", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.tbPriceLevels", t => t.PriceLevelId, cascadeDelete: true)
                .Index(t => t.AlbumId)
                .Index(t => t.PriceLevelId)
                .Index(t => t.CurrencyId);
            
            CreateTable(
                "dbo.tbAlbums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cover = c.Binary(),
                        ReleaseDate = c.DateTime(),
                        Name = c.String(nullable: false, maxLength: 150),
                        IsDeleted = c.Boolean(nullable: false),
                        Artist_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbArtists", t => t.Artist_Id)
                .Index(t => t.Artist_Id);
            
            CreateTable(
                "dbo.tbArtists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Biography = c.String(),
                        Birthday = c.DateTime(),
                        Photo = c.Binary(),
                        Name = c.String(nullable: false, maxLength: 150),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbTracks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.Binary(),
                        TrackFile = c.Binary(),
                        Duration = c.Time(precision: 7),
                        Name = c.String(nullable: false, maxLength: 150),
                        IsDeleted = c.Boolean(nullable: false),
                        Album_Id = c.Int(),
                        Artist_Id = c.Int(),
                        Genre_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbAlbums", t => t.Album_Id)
                .ForeignKey("dbo.tbArtists", t => t.Artist_Id)
                .ForeignKey("dbo.tbGenres", t => t.Genre_Id)
                .Index(t => t.Album_Id)
                .Index(t => t.Artist_Id)
                .Index(t => t.Genre_Id);
            
            CreateTable(
                "dbo.tbFeedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comments = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Track_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbTracks", t => t.Track_Id, cascadeDelete: true)
                .ForeignKey("dbo.tbUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Track_Id)
                .Index(t => t.User_Id);
            
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
                "dbo.tbTrackPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrackId = c.Int(nullable: false),
                        PriceLevelId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbCurrencies", t => t.CurrencyId)
                .ForeignKey("dbo.tbPriceLevels", t => t.PriceLevelId)
                .ForeignKey("dbo.tbTracks", t => t.TrackId)
                .Index(t => t.TrackId)
                .Index(t => t.PriceLevelId)
                .Index(t => t.CurrencyId);
            
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
            DropForeignKey("dbo.tbAlbumPrices", "PriceLevelId", "dbo.tbPriceLevels");
            DropForeignKey("dbo.tbAlbumPrices", "CurrencyId", "dbo.tbCurrencies");
            DropForeignKey("dbo.tbAlbumPrices", "AlbumId", "dbo.tbAlbums");
            DropForeignKey("dbo.tbAlbums", "Artist_Id", "dbo.tbArtists");
            DropForeignKey("dbo.tbTracks", "Genre_Id", "dbo.tbGenres");
            DropForeignKey("dbo.tbFeedbacks", "User_Id", "dbo.tbUsers");
            DropForeignKey("dbo.tbVotes", "UserId", "dbo.tbUsers");
            DropForeignKey("dbo.tbVotes", "TrackId", "dbo.tbTracks");
            DropForeignKey("dbo.tbUsers", "CurrencyId", "dbo.tbCurrencies");
            DropForeignKey("dbo.tbUsers", "PriceLevelId", "dbo.tbPriceLevels");
            DropForeignKey("dbo.tbTrackPrices", "TrackId", "dbo.tbTracks");
            DropForeignKey("dbo.tbTrackPrices", "PriceLevelId", "dbo.tbPriceLevels");
            DropForeignKey("dbo.tbTrackPrices", "CurrencyId", "dbo.tbCurrencies");
            DropForeignKey("dbo.tbCurrencyRates", "TargetCurrencyId", "dbo.tbCurrencies");
            DropForeignKey("dbo.tbCurrencyRates", "CurrencyId", "dbo.tbCurrencies");
            DropForeignKey("dbo.tbFeedbacks", "Track_Id", "dbo.tbTracks");
            DropForeignKey("dbo.tbTracks", "Artist_Id", "dbo.tbArtists");
            DropForeignKey("dbo.tbTracks", "Album_Id", "dbo.tbAlbums");
            DropIndex("dbo.tbVotes", new[] { "UserId" });
            DropIndex("dbo.tbVotes", new[] { "TrackId" });
            DropIndex("dbo.tbCurrencyRates", new[] { "TargetCurrencyId" });
            DropIndex("dbo.tbCurrencyRates", new[] { "CurrencyId" });
            DropIndex("dbo.tbTrackPrices", new[] { "CurrencyId" });
            DropIndex("dbo.tbTrackPrices", new[] { "PriceLevelId" });
            DropIndex("dbo.tbTrackPrices", new[] { "TrackId" });
            DropIndex("dbo.tbUsers", new[] { "PriceLevelId" });
            DropIndex("dbo.tbUsers", new[] { "CurrencyId" });
            DropIndex("dbo.tbFeedbacks", new[] { "User_Id" });
            DropIndex("dbo.tbFeedbacks", new[] { "Track_Id" });
            DropIndex("dbo.tbTracks", new[] { "Genre_Id" });
            DropIndex("dbo.tbTracks", new[] { "Artist_Id" });
            DropIndex("dbo.tbTracks", new[] { "Album_Id" });
            DropIndex("dbo.tbAlbums", new[] { "Artist_Id" });
            DropIndex("dbo.tbAlbumPrices", new[] { "CurrencyId" });
            DropIndex("dbo.tbAlbumPrices", new[] { "PriceLevelId" });
            DropIndex("dbo.tbAlbumPrices", new[] { "AlbumId" });
            DropTable("dbo.tbGenres");
            DropTable("dbo.tbVotes");
            DropTable("dbo.tbCurrencyRates");
            DropTable("dbo.tbCurrencies");
            DropTable("dbo.tbTrackPrices");
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
