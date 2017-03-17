namespace Shop.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// </summary>
    public partial class ReconfigureTracksContent : DbMigration
    {
        /// <summary>
        /// </summary>
        public override void Down()
        {
            this.AddColumn("dbo.tbUsersData", "IdentityKey", c => c.String(false, 128));
            this.DropForeignKey("dbo.tbTracks", "GenreId", "dbo.tbGenres");
            this.DropForeignKey("dbo.tbTracks", "ArtistId", "dbo.tbArtists");
            this.DropIndex("dbo.tbTracks", new[] { "GenreId" });
            this.DropIndex("dbo.tbTracks", new[] { "ArtistId" });
            this.DropIndex("dbo.tbAlbums", new[] { "ArtistId" });
            this.AlterColumn("dbo.tbCurrencyRates", "Date", c => c.DateTime());
            this.AlterColumn("dbo.tbTracks", "GenreId", c => c.Int());
            this.AlterColumn("dbo.tbTracks", "ArtistId", c => c.Int());
            this.AlterColumn("dbo.tbAlbums", "ArtistId", c => c.Int());
            this.CreateIndex("dbo.tbTracks", "GenreId");
            this.CreateIndex("dbo.tbTracks", "ArtistId");
            this.CreateIndex("dbo.tbAlbums", "ArtistId");
            this.AddForeignKey("dbo.tbTracks", "GenreId", "dbo.tbGenres", "Id");
            this.AddForeignKey("dbo.tbTracks", "ArtistId", "dbo.tbArtists", "Id");
            this.RenameTable("dbo.tbUsersData", "tbUsers");
        }

        /// <summary>
        /// </summary>
        public override void Up()
        {
            this.RenameTable("dbo.tbUsers", "tbUsersData");
            this.DropForeignKey("dbo.tbTracks", "ArtistId", "dbo.tbArtists");
            this.DropForeignKey("dbo.tbTracks", "GenreId", "dbo.tbGenres");
            this.DropIndex("dbo.tbAlbums", new[] { "ArtistId" });
            this.DropIndex("dbo.tbTracks", new[] { "ArtistId" });
            this.DropIndex("dbo.tbTracks", new[] { "GenreId" });
            this.AlterColumn("dbo.tbAlbums", "ArtistId", c => c.Int(false));
            this.AlterColumn("dbo.tbTracks", "ArtistId", c => c.Int(false));
            this.AlterColumn("dbo.tbTracks", "GenreId", c => c.Int(false));
            this.AlterColumn("dbo.tbCurrencyRates", "Date", c => c.DateTime(false));
            this.CreateIndex("dbo.tbAlbums", "ArtistId");
            this.CreateIndex("dbo.tbTracks", "ArtistId");
            this.CreateIndex("dbo.tbTracks", "GenreId");
            this.AddForeignKey("dbo.tbTracks", "ArtistId", "dbo.tbArtists", "Id", true);
            this.AddForeignKey("dbo.tbTracks", "GenreId", "dbo.tbGenres", "Id", true);
            this.DropColumn("dbo.tbUsersData", "IdentityKey");
        }
    }
}