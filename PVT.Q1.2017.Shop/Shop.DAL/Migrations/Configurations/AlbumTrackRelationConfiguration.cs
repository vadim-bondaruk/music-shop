namespace Shop.DAL.Migrations.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using Shop.Common.Models;

    /// <summary>
    ///     The <see cref="AlbumTrackRelation" /> configuration.
    /// </summary>
    public class AlbumTrackRelationConfiguration : EntityTypeConfiguration<AlbumTrackRelation>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AlbumTrackRelationConfiguration" /> class.
        /// </summary>
        public AlbumTrackRelationConfiguration()
        {
            this.HasKey(r => r.Id);
            this.Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(r => r.AlbumId)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(
                        new[] { new IndexAttribute("UniqueRelation_Index") { IsUnique = true, Order = 0 } }));
            this.Property(r => r.TrackId)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(
                        new[] { new IndexAttribute("UniqueRelation_Index") { IsUnique = true, Order = 1 } }));

            this.HasRequired(r => r.Track)
                .WithMany(t => t.Albums)
                .HasForeignKey(r => r.TrackId)
                .WillCascadeOnDelete(true);
            this.HasRequired(r => r.Album)
                .WithMany(a => a.Tracks)
                .HasForeignKey(r => r.AlbumId)
                .WillCascadeOnDelete(true);

            this.ToTable("tbAlbumTrackRelations");
        }
    }
}