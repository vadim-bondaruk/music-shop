namespace Shop.DAL.Migrations.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;

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
            HasKey(t => t.Id);
            Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired().HasMaxLength(150).IsUnicode().IsVariableLength();
            Property(t => t.ReleaseDate).IsOptional();

            HasOptional(a => a.Artist).WithMany(a => a.Albums).WillCascadeOnDelete(false);

            ToTable("tbAlbums");
        }
    }
}