namespace Shop.DAL.Migrations.Configurations
{
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
            ToTable("tbTracks");
            HasKey(t => t.Id);
            Property(t => t.Name).IsRequired().HasMaxLength(150).IsUnicode().IsVariableLength();
            Property(t => t.Duration).IsOptional();
            Property(t => t.Image).IsOptional();
            Property(t => t.TrackFile).IsOptional();
        }
    }
}