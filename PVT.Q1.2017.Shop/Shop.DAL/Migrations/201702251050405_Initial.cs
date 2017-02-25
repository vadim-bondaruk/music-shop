namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Album_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbAlbums", t => t.Album_Id)
                .Index(t => t.Album_Id);
            
            CreateTable(
                "dbo.tbAlbums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cover = c.Binary(),
                        Name = c.String(nullable: false, maxLength: 150),
                        ReleaseDate = c.DateTime(nullable: false),
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
                        Duration = c.Time(precision: 7),
                        Image = c.Binary(),
                        TrackFile = c.Binary(),
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
                        Track_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbTracks", t => t.Track_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Track_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrackPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Track_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbTracks", t => t.Track_Id)
                .Index(t => t.Track_Id);
            
            CreateTable(
                "dbo.tbVotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mark = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Track_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbTracks", t => t.Track_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Track_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.CurrencyRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CrossCourse = c.Double(nullable: false),
                        Currency = c.Int(nullable: false),
                        TargetCurrency = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbVotes", "User_Id", "dbo.Users");
            DropForeignKey("dbo.tbVotes", "Track_Id", "dbo.tbTracks");
            DropForeignKey("dbo.TrackPrices", "Track_Id", "dbo.tbTracks");
            DropForeignKey("dbo.tbTracks", "Genre_Id", "dbo.tbGenres");
            DropForeignKey("dbo.tbFeedbacks", "User_Id", "dbo.Users");
            DropForeignKey("dbo.tbFeedbacks", "Track_Id", "dbo.tbTracks");
            DropForeignKey("dbo.tbTracks", "Artist_Id", "dbo.tbArtists");
            DropForeignKey("dbo.tbTracks", "Album_Id", "dbo.tbAlbums");
            DropForeignKey("dbo.tbAlbums", "Artist_Id", "dbo.tbArtists");
            DropForeignKey("dbo.AlbumPrices", "Album_Id", "dbo.tbAlbums");
            DropIndex("dbo.tbVotes", new[] { "User_Id" });
            DropIndex("dbo.tbVotes", new[] { "Track_Id" });
            DropIndex("dbo.TrackPrices", new[] { "Track_Id" });
            DropIndex("dbo.tbFeedbacks", new[] { "User_Id" });
            DropIndex("dbo.tbFeedbacks", new[] { "Track_Id" });
            DropIndex("dbo.tbTracks", new[] { "Genre_Id" });
            DropIndex("dbo.tbTracks", new[] { "Artist_Id" });
            DropIndex("dbo.tbTracks", new[] { "Album_Id" });
            DropIndex("dbo.tbAlbums", new[] { "Artist_Id" });
            DropIndex("dbo.AlbumPrices", new[] { "Album_Id" });
            DropTable("dbo.CurrencyRates");
            DropTable("dbo.tbVotes");
            DropTable("dbo.TrackPrices");
            DropTable("dbo.tbGenres");
            DropTable("dbo.Users");
            DropTable("dbo.tbFeedbacks");
            DropTable("dbo.tbTracks");
            DropTable("dbo.tbArtists");
            DropTable("dbo.tbAlbums");
            DropTable("dbo.AlbumPrices");
        }
    }
}
