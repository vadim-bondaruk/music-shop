namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneMoreMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbAlbumPrices", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tbTrackPrices", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbTrackPrices", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.tbAlbumPrices", "Price", c => c.Double(nullable: false));
        }
    }
}
