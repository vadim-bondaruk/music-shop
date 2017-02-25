namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAlbumReleaseDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbAlbums", "ReleaseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbAlbums", "ReleaseDate", c => c.DateTime(nullable: false));
        }
    }
}
