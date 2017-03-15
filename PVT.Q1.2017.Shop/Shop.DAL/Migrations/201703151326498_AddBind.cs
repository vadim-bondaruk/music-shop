namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBind : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbUsers", "UserRoles", c => c.Int(nullable: false));
            DropColumn("dbo.tbUsers", "UserRole");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbUsers", "UserRole", c => c.Int(nullable: false));
            DropColumn("dbo.tbUsers", "UserRoles");
        }
    }
}
