namespace Shop.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// </summary>
    public partial class new_artist_field : DbMigration
    {
        /// <summary>
        /// </summary>
        public override void Down()
        {
            this.DropColumn("dbo.Artists", "IsCreation");
        }

        /// <summary>
        /// </summary>
        public override void Up()
        {
            this.AddColumn("dbo.Artists", "IsCreation", c => c.Boolean(false));
        }
    }
}