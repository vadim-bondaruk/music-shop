namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Albums", "Cart_Id", c => c.Int());
            AddColumn("dbo.Tracks", "Cart_Id", c => c.Int());
            CreateIndex("dbo.Albums", "Cart_Id");
            CreateIndex("dbo.Tracks", "Cart_Id");
            AddForeignKey("dbo.Albums", "Cart_Id", "dbo.Carts", "Id");
            AddForeignKey("dbo.Tracks", "Cart_Id", "dbo.Carts", "Id");
        }

        
        public override void Down()
        {
            DropForeignKey("dbo.Tracks", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.Albums", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.Tracks", new[] { "Cart_Id" });
            DropIndex("dbo.Albums", new[] { "Cart_Id" });
            DropColumn("dbo.Tracks", "Cart_Id");
            DropColumn("dbo.Albums", "Cart_Id");
            DropTable("dbo.Carts");
        }
    }
}
