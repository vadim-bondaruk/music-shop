namespace Shop.DAL.Migrations.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;

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

            this.HasRequired(t => t.Artist).WithMany(a => a.Tracks).WillCascadeOnDelete(true);
            this.HasRequired(t => t.Genre).WithMany(a => a.Tracks).WillCascadeOnDelete(true);

            this.ToTable("tbTracks");
        }
    }
}