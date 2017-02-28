namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedSchema : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tbAlbums", name: "ArtistId", newName: "Artist_Id");
            RenameColumn(table: "dbo.tbTracks", name: "AlbumId", newName: "Album_Id");
            RenameColumn(table: "dbo.tbTracks", name: "ArtistId", newName: "Artist_Id");
            RenameColumn(table: "dbo.tbTracks", name: "GenreId", newName: "Genre_Id");
            RenameIndex(table: "dbo.tbAlbums", name: "IX_ArtistId", newName: "IX_Artist_Id");
            RenameIndex(table: "dbo.tbTracks", name: "IX_AlbumId", newName: "IX_Album_Id");
            RenameIndex(table: "dbo.tbTracks", name: "IX_ArtistId", newName: "IX_Artist_Id");
            RenameIndex(table: "dbo.tbTracks", name: "IX_GenreId", newName: "IX_Genre_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.tbTracks", name: "IX_Genre_Id", newName: "IX_GenreId");
            RenameIndex(table: "dbo.tbTracks", name: "IX_Artist_Id", newName: "IX_ArtistId");
            RenameIndex(table: "dbo.tbTracks", name: "IX_Album_Id", newName: "IX_AlbumId");
            RenameIndex(table: "dbo.tbAlbums", name: "IX_Artist_Id", newName: "IX_ArtistId");
            RenameColumn(table: "dbo.tbTracks", name: "Genre_Id", newName: "GenreId");
            RenameColumn(table: "dbo.tbTracks", name: "Artist_Id", newName: "ArtistId");
            RenameColumn(table: "dbo.tbTracks", name: "Album_Id", newName: "AlbumId");
            RenameColumn(table: "dbo.tbAlbums", name: "Artist_Id", newName: "ArtistId");
        }
    }
}
