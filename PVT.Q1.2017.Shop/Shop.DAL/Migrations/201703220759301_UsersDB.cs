namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 15),
                        LastName = c.String(nullable: false, maxLength: 25),
                        Login = c.String(nullable: false, maxLength: 15),
                        Password = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 40),
                        Sex = c.String(nullable: false),
                        BirthDate = c.DateTime(),
                        UserRoles = c.Int(nullable: false),
                        Country = c.String(maxLength: 15),
                        PhoneNumber = c.String(maxLength: 30),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UsersData", "FirstName", c => c.String());
            AddColumn("dbo.UsersData", "LastName", c => c.String());
            AddColumn("dbo.UsersData", "Login", c => c.String());
            AddColumn("dbo.UsersData", "Password", c => c.String());
            AddColumn("dbo.UsersData", "Email", c => c.String());
            AddColumn("dbo.UsersData", "Sex", c => c.String());
            AddColumn("dbo.UsersData", "BirthDate", c => c.DateTime());
            AddColumn("dbo.UsersData", "Country", c => c.String());
            AddColumn("dbo.UsersData", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsersData", "PhoneNumber");
            DropColumn("dbo.UsersData", "Country");
            DropColumn("dbo.UsersData", "BirthDate");
            DropColumn("dbo.UsersData", "Sex");
            DropColumn("dbo.UsersData", "Email");
            DropColumn("dbo.UsersData", "Password");
            DropColumn("dbo.UsersData", "Login");
            DropColumn("dbo.UsersData", "LastName");
            DropColumn("dbo.UsersData", "FirstName");
            DropTable("dbo.Users");
        }
    }
}
