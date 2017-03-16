namespace Shop.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// </summary>
    public partial class _new : DbMigration
    {
        /// <summary>
        /// </summary>
        public override void Down()
        {
            this.AddColumn("dbo.tbArtists", "Photo", c => c.Binary());
        }

        /// <summary>
        /// </summary>
        public override void Up()
        {
            this.DropColumn("dbo.tbArtists", "Photo");
        }
    }
}