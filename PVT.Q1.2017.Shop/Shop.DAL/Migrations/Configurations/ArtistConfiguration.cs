namespace Shop.DAL.Migrations.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Shop.Common.Models;

    /// <summary>
    ///     The <see cref="Artist" /> configuration.
    /// </summary>
    public class ArtistConfiguration : EntityTypeConfiguration<Artist>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArtistConfiguration" /> class.
        /// </summary>
        public ArtistConfiguration()
        {
            this.HasKey(t => t.Id);
            this.Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name).IsRequired().HasMaxLength(150).IsUnicode().IsVariableLength();

            // .HasColumnAnnotation(
            // "Index",
            // new IndexAnnotation(new[] { new IndexAttribute("UniqueArtistName_Index") { IsUnique = true } }));
            this.Property(a => a.Biography).IsMaxLength();
            this.Property(a => a.Birthday).IsOptional();
            this.ToTable("Artists");
        }
    }
}