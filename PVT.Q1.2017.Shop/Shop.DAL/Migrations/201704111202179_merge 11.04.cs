namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class merge1104 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderTracks", "TrackId", "dbo.Tracks");
            AddColumn("dbo.OrderTracks", "Track_Id", c => c.Int());
            AddColumn("dbo.OrderTracks", "Track_Id1", c => c.Int());
            CreateIndex("dbo.OrderTracks", "Track_Id");
            CreateIndex("dbo.OrderTracks", "Track_Id1");
            AddForeignKey("dbo.OrderTracks", "Track_Id", "dbo.Tracks", "Id");
            AddForeignKey("dbo.OrderTracks", "Track_Id1", "dbo.Tracks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderTracks", "Track_Id1", "dbo.Tracks");
            DropForeignKey("dbo.OrderTracks", "Track_Id", "dbo.Tracks");
            DropIndex("dbo.OrderTracks", new[] { "Track_Id1" });
            DropIndex("dbo.OrderTracks", new[] { "Track_Id" });
            DropColumn("dbo.OrderTracks", "Track_Id1");
            DropColumn("dbo.OrderTracks", "Track_Id");
            AddForeignKey("dbo.OrderTracks", "TrackId", "dbo.Tracks", "Id", cascadeDelete: true);
        }
    }
}
