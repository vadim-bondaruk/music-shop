namespace Shop.DAL.Migrations.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;

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
            this.HasKey(t => t.Id);
            this.Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Comments).IsRequired().IsUnicode().IsVariableLength();
            this.HasRequired(f => f.Track).WithMany(t => t.Feedbacks).WillCascadeOnDelete(true);
            this.HasRequired(f => f.User).WithMany(t => t.Feedbacks).WillCascadeOnDelete(true);

            this.ToTable("tbFeedbacks");
        }
    }
}