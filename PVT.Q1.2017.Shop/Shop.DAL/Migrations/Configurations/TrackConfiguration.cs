﻿namespace Shop.DAL.Migrations.Configurations
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

            // TODO: uncomment this when track management will be ready
            // this.Property(t => t.TrackFile).IsRequired(); 

            this.HasRequired(t => t.Artist).WithMany(a => a.Tracks).WillCascadeOnDelete(true);
            this.HasRequired(t => t.Genre).WithMany(a => a.Tracks).WillCascadeOnDelete(true);

            HasOptional(t => t.Owner).WithMany().HasForeignKey(t => t.OwnerId).WillCascadeOnDelete(false);

            this.ToTable("Tracks");
        }
    }
}