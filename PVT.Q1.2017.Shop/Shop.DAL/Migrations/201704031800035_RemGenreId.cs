namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemGenreId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tracks", name: "GenreId", newName: "Genre_Id");
            RenameIndex(table: "dbo.Tracks", name: "IX_GenreId", newName: "IX_Genre_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Tracks", name: "IX_Genre_Id", newName: "IX_GenreId");
            RenameColumn(table: "dbo.Tracks", name: "Genre_Id", newName: "GenreId");
        }
    }
}
