namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updated : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tbVotes", name: "TrackId", newName: "Track_Id");
            RenameColumn(table: "dbo.tbVotes", name: "UserId", newName: "User_Id");
            RenameIndex(table: "dbo.tbVotes", name: "IX_TrackId", newName: "IX_Track_Id");
            RenameIndex(table: "dbo.tbVotes", name: "IX_UserId", newName: "IX_User_Id");
            DropColumn("dbo.tbVotes", "MarkId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbVotes", "MarkId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.tbVotes", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.tbVotes", name: "IX_Track_Id", newName: "IX_TrackId");
            RenameColumn(table: "dbo.tbVotes", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.tbVotes", name: "Track_Id", newName: "TrackId");
        }
    }
}
