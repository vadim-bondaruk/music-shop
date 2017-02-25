namespace Shop.DAL.Migrations.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;

    /// <summary>
    /// The <see cref="Album"/> configuration.
    /// </summary>
    public class AlbumConfiguration : EntityTypeConfiguration<Album>    
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumConfiguration"/> class.
        /// </summary>
        public AlbumConfiguration()
        {
            this.ToTable("tbAlbums");
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).IsRequired().HasMaxLength(150).IsUnicode().IsVariableLength();
            this.Property(t => t.ReleaseDate).IsOptional();
        }
    }
}