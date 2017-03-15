namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRolesArray : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tbUsers", "UserRoles");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbUsers", "UserRoles", c => c.Int(nullable: false));
        }
    }
}
