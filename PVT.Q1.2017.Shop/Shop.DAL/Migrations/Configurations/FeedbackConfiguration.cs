﻿namespace Shop.DAL.Migrations.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;

    /// <summary>
    /// The <see cref="Feedback"/> configuration.
    /// </summary>
    public class FeedbackConfiguration : EntityTypeConfiguration<Feedback>    
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackConfiguration"/> class.
        /// </summary>
        public FeedbackConfiguration()
        {
            this.ToTable("tbFeedbacks");
            this.HasKey(t => t.Id);
            this.Property(t => t.Comments).IsRequired().IsUnicode().IsVariableLength();
        }
    }
}