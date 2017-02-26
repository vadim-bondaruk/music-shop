namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbAlbumPrices", "PriceLevelId", c => c.Int(nullable: false));
            AddColumn("dbo.tbTrackPrices", "PriceLevelId", c => c.Int(nullable: false));
            AddColumn("dbo.tbTrackPrices", "Price", c => c.Double(nullable: false));
            CreateIndex("dbo.tbAlbumPrices", "PriceLevelId");
            CreateIndex("dbo.tbTrackPrices", "PriceLevelId");
            AddForeignKey("dbo.tbTrackPrices", "PriceLevelId", "dbo.tbPriceLevels", "Id");
            AddForeignKey("dbo.tbAlbumPrices", "PriceLevelId", "dbo.tbPriceLevels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbAlbumPrices", "PriceLevelId", "dbo.tbPriceLevels");
            DropForeignKey("dbo.tbTrackPrices", "PriceLevelId", "dbo.tbPriceLevels");
            DropIndex("dbo.tbTrackPrices", new[] { "PriceLevelId" });
            DropIndex("dbo.tbAlbumPrices", new[] { "PriceLevelId" });
            DropColumn("dbo.tbTrackPrices", "Price");
            DropColumn("dbo.tbTrackPrices", "PriceLevelId");
            DropColumn("dbo.tbAlbumPrices", "PriceLevelId");
        }
    }
}
