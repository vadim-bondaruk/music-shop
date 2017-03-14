namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginTesting : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbTracksAndAlbumsRelations", "TrackID", "dbo.tbTracks");
            DropForeignKey("dbo.tbTracksAndAlbumsRelations", "AlbumID", "dbo.tbAlbums");
            DropIndex("dbo.tbTracksAndAlbumsRelations", new[] { "TrackID" });
            DropIndex("dbo.tbTracksAndAlbumsRelations", new[] { "AlbumID" });
            CreateTable(
                "dbo.tbAlbumTrackRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlbumId = c.Int(nullable: false),
                        TrackId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbAlbums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.tbTracks", t => t.TrackId, cascadeDelete: true)
                .Index(t => new { t.AlbumId, t.TrackId }, unique: true, name: "UniqueRelation_Index");
            
            AddColumn("dbo.tbCurrencyRates", "Date", c => c.DateTime());
            AlterColumn("dbo.tbUsers", "FirstName", c => c.String());
            AlterColumn("dbo.tbUsers", "LastName", c => c.String());
            AlterColumn("dbo.tbUsers", "Login", c => c.String());
            AlterColumn("dbo.tbUsers", "Password", c => c.String());
            AlterColumn("dbo.tbUsers", "Email", c => c.String());
            AlterColumn("dbo.tbUsers", "Sex", c => c.String());
            AlterColumn("dbo.tbUsers", "Country", c => c.String());
            AlterColumn("dbo.tbUsers", "PhoneNumber", c => c.String());
            DropColumn("dbo.tbAlbums", "TrackId");
            DropColumn("dbo.tbTracks", "AlbumId");
            DropTable("dbo.tbTracksAndAlbumsRelations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tbTracksAndAlbumsRelations",
                c => new
                    {
                        TrackID = c.Int(nullable: false),
                        AlbumID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrackID, t.AlbumID });
            
            AddColumn("dbo.tbTracks", "AlbumId", c => c.Int());
            AddColumn("dbo.tbAlbums", "TrackId", c => c.Int());
            DropForeignKey("dbo.tbAlbumTrackRelations", "TrackId", "dbo.tbTracks");
            DropForeignKey("dbo.tbAlbumTrackRelations", "AlbumId", "dbo.tbAlbums");
            DropIndex("dbo.tbAlbumTrackRelations", "UniqueRelation_Index");
            AlterColumn("dbo.tbUsers", "PhoneNumber", c => c.String(maxLength: 30));
            AlterColumn("dbo.tbUsers", "Country", c => c.String(maxLength: 15));
            AlterColumn("dbo.tbUsers", "Sex", c => c.String(nullable: false));
            AlterColumn("dbo.tbUsers", "Email", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.tbUsers", "Password", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.tbUsers", "Login", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.tbUsers", "LastName", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.tbUsers", "FirstName", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.tbCurrencyRates", "Date");
            DropTable("dbo.tbAlbumTrackRelations");
            CreateIndex("dbo.tbTracksAndAlbumsRelations", "AlbumID");
            CreateIndex("dbo.tbTracksAndAlbumsRelations", "TrackID");
            AddForeignKey("dbo.tbTracksAndAlbumsRelations", "AlbumID", "dbo.tbAlbums", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tbTracksAndAlbumsRelations", "TrackID", "dbo.tbTracks", "Id", cascadeDelete: true);
        }
    }
}
