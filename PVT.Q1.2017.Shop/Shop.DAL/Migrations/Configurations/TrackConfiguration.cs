namespace Shop.DAL.Migrations.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The <see cref="Track"/> configuration.
    /// </summary>
    public class TrackConfiguration : EntityTypeConfiguration<Track>    
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackConfiguration"/> class.
        /// </summary>
        public TrackConfiguration()
        {
            this.HasKey(t => t.Id);
            this.Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name).IsRequired().HasMaxLength(150).IsUnicode().IsVariableLength();
            this.HasOptional(t => t.Album).WithMany(a => a.Tracks).WillCascadeOnDelete(false);
            this.HasOptional(t => t.Artist).WithMany(a => a.Tracks).WillCascadeOnDelete(false);
            this.HasOptional(t => t.Genre).WithMany(a => a.Tracks).WillCascadeOnDelete(false);
            this.ToTable("tbTracks");
        }
    }
}