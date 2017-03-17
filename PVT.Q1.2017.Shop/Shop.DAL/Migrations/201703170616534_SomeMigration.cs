namespace Shop.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// </summary>
    public partial class SomeMigration : DbMigration
    {
        /// <summary>
        /// </summary>
        public override void Down()
        {
            this.DropColumn("dbo.tbArtists", "Photo");
        }

        /// <summary>
        /// </summary>
        public override void Up()
        {
            this.AddColumn("dbo.tbArtists", "Photo", c => c.Binary());
        }
    }
}