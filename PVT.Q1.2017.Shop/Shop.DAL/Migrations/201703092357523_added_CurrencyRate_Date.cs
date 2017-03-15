namespace Shop.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// </summary>
    public partial class Added_CurrencyRate_Date : DbMigration
    {
        /// <summary>
        /// </summary>
        public override void Down()
        {
            this.DropColumn("dbo.tbCurrencyRates", "Date");
        }

        /// <summary>
        /// </summary>
        public override void Up()
        {
            this.AddColumn("dbo.tbCurrencyRates", "Date", c => c.DateTime(false));
        }
    }
}