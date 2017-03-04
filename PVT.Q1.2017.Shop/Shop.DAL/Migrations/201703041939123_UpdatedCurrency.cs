namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCurrency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbCurrencies", "Symbol", c => c.String(maxLength: 10));
            AddColumn("dbo.tbCurrencies", "Name", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.tbCurrencies", "FullName", c => c.String(maxLength: 150));
            DropColumn("dbo.tbCurrencies", "ShortName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbCurrencies", "ShortName", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.tbCurrencies", "FullName", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.tbCurrencies", "Name");
            DropColumn("dbo.tbCurrencies", "Symbol");
        }
    }
}
