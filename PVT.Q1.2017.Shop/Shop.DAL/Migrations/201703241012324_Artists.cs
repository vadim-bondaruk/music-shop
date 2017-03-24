namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Artists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AlbumId = c.Int(nullable: false),
                        PriceLevelId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.PriceLevels", t => t.PriceLevelId, cascadeDelete: true)
                .Index(t => t.AlbumId)
                .Index(t => t.PriceLevelId)
                .Index(t => t.CurrencyId);
            
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Cover = c.Binary(),
                        ReleaseDate = c.DateTime(),
                        ArtistId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId)
                .ForeignKey("dbo.Carts", t => t.Cart_Id)
                .Index(t => t.ArtistId)
                .Index(t => t.Cart_Id);
            
            CreateTable(
                "dbo.Artists",
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
                "dbo.Tracks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        ReleaseDate = c.DateTime(),
                        Image = c.Binary(),
                        TrackFile = c.Binary(),
                        Duration = c.Time(precision: 7),
                        ArtistId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Carts", t => t.Cart_Id)
                .Index(t => t.ArtistId)
                .Index(t => t.GenreId)
                .Index(t => t.Cart_Id);
            
            CreateTable(
                "dbo.AlbumTrackRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlbumId = c.Int(nullable: false),
                        TrackId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Tracks", t => t.TrackId, cascadeDelete: true)
                .Index(t => new { t.AlbumId, t.TrackId }, unique: true, name: "UniqueRelation_Index");
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comments = c.String(nullable: false),
                        TrackId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tracks", t => t.TrackId, cascadeDelete: true)
                .ForeignKey("dbo.UsersData", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TrackId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UsersData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dicount = c.Double(),
                        CurrencyId = c.Int(nullable: false),
                        PriceLevelId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceLevels", t => t.PriceLevelId, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .Index(t => t.CurrencyId)
                .Index(t => t.PriceLevelId);
            
            CreateTable(
                "dbo.PriceLevels",
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
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TrackId = c.Int(nullable: false),
                        PriceLevelId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .ForeignKey("dbo.PriceLevels", t => t.PriceLevelId)
                .ForeignKey("dbo.Tracks", t => t.TrackId)
                .Index(t => t.TrackId)
                .Index(t => t.PriceLevelId)
                .Index(t => t.CurrencyId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortName = c.String(nullable: false, maxLength: 3),
                        FullName = c.String(maxLength: 150),
                        Code = c.Int(nullable: false),
                        Symbol = c.String(maxLength: 10),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ShortName, unique: true, name: "UniqueCurrencyName_Index")
                .Index(t => t.Code, unique: true, name: "UniqueCurrencyCode_Index");
            
            CreateTable(
                "dbo.CurrencyRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CrossCourse = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        TargetCurrencyId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .ForeignKey("dbo.Currencies", t => t.TargetCurrencyId)
                .Index(t => t.CurrencyId)
                .Index(t => t.TargetCurrencyId);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mark = c.Int(nullable: false),
                        TrackId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tracks", t => t.TrackId)
                .ForeignKey("dbo.UsersData", t => t.UserId)
                .Index(t => t.TrackId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(nullable: false, maxLength: 150),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 15),
                        LastName = c.String(nullable: false, maxLength: 25),
                        Login = c.String(nullable: false, maxLength: 15),
                        Password = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 40),
                        Sex = c.String(nullable: false),
                        BirthDate = c.DateTime(),
                        UserRoles = c.Int(nullable: false),
                        Country = c.String(maxLength: 15),
                        PhoneNumber = c.String(maxLength: 30),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tracks", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.Albums", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.AlbumPrices", "PriceLevelId", "dbo.PriceLevels");
            DropForeignKey("dbo.AlbumPrices", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.AlbumPrices", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.Albums", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Tracks", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Feedbacks", "UserId", "dbo.UsersData");
            DropForeignKey("dbo.Votes", "UserId", "dbo.UsersData");
            DropForeignKey("dbo.Votes", "TrackId", "dbo.Tracks");
            DropForeignKey("dbo.UsersData", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.UsersData", "PriceLevelId", "dbo.PriceLevels");
            DropForeignKey("dbo.TrackPrices", "TrackId", "dbo.Tracks");
            DropForeignKey("dbo.TrackPrices", "PriceLevelId", "dbo.PriceLevels");
            DropForeignKey("dbo.TrackPrices", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.CurrencyRates", "TargetCurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.CurrencyRates", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Feedbacks", "TrackId", "dbo.Tracks");
            DropForeignKey("dbo.Tracks", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.AlbumTrackRelations", "TrackId", "dbo.Tracks");
            DropForeignKey("dbo.AlbumTrackRelations", "AlbumId", "dbo.Albums");
            DropIndex("dbo.Votes", new[] { "UserId" });
            DropIndex("dbo.Votes", new[] { "TrackId" });
            DropIndex("dbo.CurrencyRates", new[] { "TargetCurrencyId" });
            DropIndex("dbo.CurrencyRates", new[] { "CurrencyId" });
            DropIndex("dbo.Currencies", "UniqueCurrencyCode_Index");
            DropIndex("dbo.Currencies", "UniqueCurrencyName_Index");
            DropIndex("dbo.TrackPrices", new[] { "CurrencyId" });
            DropIndex("dbo.TrackPrices", new[] { "PriceLevelId" });
            DropIndex("dbo.TrackPrices", new[] { "TrackId" });
            DropIndex("dbo.UsersData", new[] { "PriceLevelId" });
            DropIndex("dbo.UsersData", new[] { "CurrencyId" });
            DropIndex("dbo.Feedbacks", new[] { "UserId" });
            DropIndex("dbo.Feedbacks", new[] { "TrackId" });
            DropIndex("dbo.AlbumTrackRelations", "UniqueRelation_Index");
            DropIndex("dbo.Tracks", new[] { "Cart_Id" });
            DropIndex("dbo.Tracks", new[] { "GenreId" });
            DropIndex("dbo.Tracks", new[] { "ArtistId" });
            DropIndex("dbo.Albums", new[] { "Cart_Id" });
            DropIndex("dbo.Albums", new[] { "ArtistId" });
            DropIndex("dbo.AlbumPrices", new[] { "CurrencyId" });
            DropIndex("dbo.AlbumPrices", new[] { "PriceLevelId" });
            DropIndex("dbo.AlbumPrices", new[] { "AlbumId" });
            DropTable("dbo.Users");
            DropTable("dbo.Carts");
            DropTable("dbo.Genres");
            DropTable("dbo.Votes");
            DropTable("dbo.CurrencyRates");
            DropTable("dbo.Currencies");
            DropTable("dbo.TrackPrices");
            DropTable("dbo.PriceLevels");
            DropTable("dbo.UsersData");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.AlbumTrackRelations");
            DropTable("dbo.Tracks");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
            DropTable("dbo.AlbumPrices");
        }
    }
}
