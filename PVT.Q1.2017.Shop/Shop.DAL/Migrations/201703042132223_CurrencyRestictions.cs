namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CurrencyRestictions : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.tbCurrencies", "Code", unique: true, name: "UniqueCurrencyCode_Index");
            CreateIndex("dbo.tbCurrencies", "Name", unique: true, name: "UniqueCurrencyName_Index");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tbCurrencies", "UniqueCurrencyName_Index");
            DropIndex("dbo.tbCurrencies", "UniqueCurrencyCode_Index");
        }
    }
}
