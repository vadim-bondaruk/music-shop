namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ConfirmedEmail", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ConfirmedEmail");
        }
    }
}
