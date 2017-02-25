namespace Shop.DAL.Migrations.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;

    /// <summary>
    /// The <see cref="Artist"/> configuration.
    /// </summary>
    public class ArtistConfiguration : EntityTypeConfiguration<Artist>    
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistConfiguration"/> class.
        /// </summary>
        public ArtistConfiguration()
        {
            this.ToTable("tbArtists");
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).IsRequired().HasMaxLength(150).IsUnicode().IsVariableLength();
        }
    }
}