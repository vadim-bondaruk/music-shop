namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSettingTableForAdmin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Settings", "DefaultPriceLevelId", "dbo.PriceLevels");
            DropIndex("dbo.Settings", new[] { "DefaultPriceLevelId" });
            DropColumn("dbo.Settings", "DefaultPriceLevelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "DefaultPriceLevelId", c => c.Int(nullable: false));
            CreateIndex("dbo.Settings", "DefaultPriceLevelId");
            AddForeignKey("dbo.Settings", "DefaultPriceLevelId", "dbo.PriceLevels", "Id", cascadeDelete: true);
        }
    }
}
