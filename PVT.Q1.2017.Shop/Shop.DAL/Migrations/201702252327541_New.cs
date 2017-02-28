namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
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
                        Biography = c.String(),
                        Birthday = c.DateTime(),
                        Name = c.String(nullable: false, maxLength: 150),
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
                        IsDeleted = c.Boolean(nullable: false),
                        Track_Id = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbTracks", t => t.Track_Id, cascadeDelete: true)
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
                        Track_Id = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbTracks", t => t.Track_Id, cascadeDelete: true)
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
            DropForeignKey("dbo.tbTracks", "GenreId", "dbo.tbGenres");
            DropForeignKey("dbo.tbFeedbacks", "User_Id", "dbo.Users");
            DropForeignKey("dbo.tbFeedbacks", "Track_Id", "dbo.tbTracks");
            DropForeignKey("dbo.tbTracks", "ArtistId", "dbo.tbArtists");
            DropForeignKey("dbo.tbTracks", "AlbumId", "dbo.tbAlbums");
            DropForeignKey("dbo.tbAlbums", "ArtistId", "dbo.tbArtists");
            DropForeignKey("dbo.AlbumPrices", "Album_Id", "dbo.tbAlbums");
            DropIndex("dbo.tbVotes", new[] { "User_Id" });
            DropIndex("dbo.tbVotes", new[] { "Track_Id" });
            DropIndex("dbo.TrackPrices", new[] { "Track_Id" });
            DropIndex("dbo.tbFeedbacks", new[] { "User_Id" });
            DropIndex("dbo.tbFeedbacks", new[] { "Track_Id" });
            DropIndex("dbo.tbTracks", new[] { "GenreId" });
            DropIndex("dbo.tbTracks", new[] { "ArtistId" });
            DropIndex("dbo.tbTracks", new[] { "AlbumId" });
            DropIndex("dbo.tbAlbums", new[] { "ArtistId" });
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
