namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_CurrencyRate_Date : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbCurrencyRates", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbCurrencyRates", "Date");
        }
    }
}
