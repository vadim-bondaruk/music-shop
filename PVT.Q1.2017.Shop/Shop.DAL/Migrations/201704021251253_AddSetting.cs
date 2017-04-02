namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSetting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DefaultCurrencyId = c.Int(nullable: false),
                        DefaultPriceLevelId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.DefaultCurrencyId)
                .ForeignKey("dbo.PriceLevels", t => t.DefaultPriceLevelId, cascadeDelete: true)
                .Index(t => t.DefaultCurrencyId)
                .Index(t => t.DefaultPriceLevelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Settings", "DefaultPriceLevelId", "dbo.PriceLevels");
            DropForeignKey("dbo.Settings", "DefaultCurrencyId", "dbo.Currencies");
            DropIndex("dbo.Settings", new[] { "DefaultPriceLevelId" });
            DropIndex("dbo.Settings", new[] { "DefaultCurrencyId" });
            DropTable("dbo.Settings");
        }
    }
}
