namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CurrencyEdited : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbCurrencyRates", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tbCurrencyRates", "CrossCourse", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbCurrencyRates", "CrossCourse", c => c.Double(nullable: false));
            DropColumn("dbo.tbCurrencyRates", "Date");
        }
    }
}
