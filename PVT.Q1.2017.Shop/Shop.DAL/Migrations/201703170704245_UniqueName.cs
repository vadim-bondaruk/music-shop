namespace Shop.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// </summary>
    public partial class UniqueName : DbMigration
    {
        /// <summary>
        /// </summary>
        public override void Down()
        {
            this.DropIndex("dbo.tbArtists", "UniqueArtistName_Index");
        }

        /// <summary>
        /// </summary>
        public override void Up()
        {
            this.CreateIndex("dbo.tbArtists", "Name", true, "UniqueArtistName_Index");
        }
    }
}