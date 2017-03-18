namespace Shop.DAL.Migrations.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Shop.Common.Models;

    /// <summary>
    ///     The <see cref="Album" /> configuration.
    /// </summary>
    public class AlbumConfiguration : EntityTypeConfiguration<Album>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AlbumConfiguration" /> class.
        /// </summary>
        public AlbumConfiguration()
        {
            this.HasKey(a => a.Id);
            this.Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(a => a.Name).IsRequired().HasMaxLength(150).IsUnicode().IsVariableLength();
            this.Property(a => a.ReleaseDate).IsOptional();

           this.HasRequired(a => a.Artist).WithMany(a => a.Albums).WillCascadeOnDelete(false);
            this.ToTable("tbAlbums");
        }
    }
}