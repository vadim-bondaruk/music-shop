namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPaymentTransactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.UsersData", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CurrencyId);
            
            AddColumn("dbo.Albums", "PaymentTransaction_Id", c => c.Int());
            AddColumn("dbo.Tracks", "PaymentTransaction_Id", c => c.Int());
            CreateIndex("dbo.Albums", "PaymentTransaction_Id");
            CreateIndex("dbo.Tracks", "PaymentTransaction_Id");
            AddForeignKey("dbo.Albums", "PaymentTransaction_Id", "dbo.PaymentTransactions", "Id");
            AddForeignKey("dbo.Tracks", "PaymentTransaction_Id", "dbo.PaymentTransactions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentTransactions", "UserId", "dbo.UsersData");
            DropForeignKey("dbo.Tracks", "PaymentTransaction_Id", "dbo.PaymentTransactions");
            DropForeignKey("dbo.PaymentTransactions", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Albums", "PaymentTransaction_Id", "dbo.PaymentTransactions");
            DropIndex("dbo.PaymentTransactions", new[] { "CurrencyId" });
            DropIndex("dbo.PaymentTransactions", new[] { "UserId" });
            DropIndex("dbo.Tracks", new[] { "PaymentTransaction_Id" });
            DropIndex("dbo.Albums", new[] { "PaymentTransaction_Id" });
            DropColumn("dbo.Tracks", "PaymentTransaction_Id");
            DropColumn("dbo.Albums", "PaymentTransaction_Id");
            DropTable("dbo.PaymentTransactions");
        }
    }
}
