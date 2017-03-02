namespace Shop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForignKeys : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tbAlbums", name: "Artist_Id", newName: "ArtistId");
            RenameColumn(table: "dbo.tbTracks", name: "Album_Id", newName: "AlbumId");
            RenameColumn(table: "dbo.tbTracks", name: "Artist_Id", newName: "ArtistId");
            RenameColumn(table: "dbo.tbFeedbacks", name: "Track_Id", newName: "TrackId");
            RenameColumn(table: "dbo.tbTracks", name: "Genre_Id", newName: "GenreId");
            RenameColumn(table: "dbo.tbVotes", name: "Track_Id", newName: "TrackId");
            RenameColumn(table: "dbo.tbFeedbacks", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.tbVotes", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.tbAlbums", name: "IX_Artist_Id", newName: "IX_ArtistId");
            RenameIndex(table: "dbo.tbTracks", name: "IX_Album_Id", newName: "IX_AlbumId");
            RenameIndex(table: "dbo.tbTracks", name: "IX_Artist_Id", newName: "IX_ArtistId");
            RenameIndex(table: "dbo.tbTracks", name: "IX_Genre_Id", newName: "IX_GenreId");
            RenameIndex(table: "dbo.tbFeedbacks", name: "IX_Track_Id", newName: "IX_TrackId");
            RenameIndex(table: "dbo.tbFeedbacks", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.tbVotes", name: "IX_Track_Id", newName: "IX_TrackId");
            RenameIndex(table: "dbo.tbVotes", name: "IX_User_Id", newName: "IX_UserId");
            AlterColumn("dbo.tbCurrencyRates", "CrossCourse", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbCurrencyRates", "CrossCourse", c => c.Double(nullable: false));
            RenameIndex(table: "dbo.tbVotes", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.tbVotes", name: "IX_TrackId", newName: "IX_Track_Id");
            RenameIndex(table: "dbo.tbFeedbacks", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.tbFeedbacks", name: "IX_TrackId", newName: "IX_Track_Id");
            RenameIndex(table: "dbo.tbTracks", name: "IX_GenreId", newName: "IX_Genre_Id");
            RenameIndex(table: "dbo.tbTracks", name: "IX_ArtistId", newName: "IX_Artist_Id");
            RenameIndex(table: "dbo.tbTracks", name: "IX_AlbumId", newName: "IX_Album_Id");
            RenameIndex(table: "dbo.tbAlbums", name: "IX_ArtistId", newName: "IX_Artist_Id");
            RenameColumn(table: "dbo.tbVotes", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.tbFeedbacks", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.tbVotes", name: "TrackId", newName: "Track_Id");
            RenameColumn(table: "dbo.tbTracks", name: "GenreId", newName: "Genre_Id");
            RenameColumn(table: "dbo.tbFeedbacks", name: "TrackId", newName: "Track_Id");
            RenameColumn(table: "dbo.tbTracks", name: "ArtistId", newName: "Artist_Id");
            RenameColumn(table: "dbo.tbTracks", name: "AlbumId", newName: "Album_Id");
            RenameColumn(table: "dbo.tbAlbums", name: "ArtistId", newName: "Artist_Id");
        }
    }
}
