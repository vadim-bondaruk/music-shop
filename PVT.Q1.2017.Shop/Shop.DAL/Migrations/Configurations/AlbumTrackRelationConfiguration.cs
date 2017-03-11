﻿namespace Shop.DAL.Migrations.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;

    /// <summary>
    /// The <see cref="AlbumTrackRelation"/> configuration.
    /// </summary>
    public class AlbumTrackRelationConfiguration : EntityTypeConfiguration<AlbumTrackRelation>
    {
        public AlbumTrackRelationConfiguration()
        {
            HasKey(r => r.Id);
            Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(r => r.Track).WithMany(t => t.Albums).WillCascadeOnDelete(true);
            HasRequired(r => r.Album).WithMany(a => a.Tracks).WillCascadeOnDelete(true);

            ToTable("tbAlbumTrackRelations");
        } 
    }
}