namespace Shop.DAL.Migrations.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The <see cref="Vote"/> configuration.
    /// </summary>
    public class VoteConfiguration : EntityTypeConfiguration<Vote>    
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoteConfiguration"/> class.
        /// </summary>
        public VoteConfiguration()
        {
            this.HasKey(t => t.Id);
            this.Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Mark).IsRequired();
            this.HasRequired(v => v.Track).WithMany(t => t.Votes).HasForeignKey(v => v.TrackId).WillCascadeOnDelete(false);
            this.HasRequired(v => v.User).WithMany(t => t.Votes).HasForeignKey(v => v.UserId).WillCascadeOnDelete(false);

            this.ToTable("tbVotes");
        }
    }
}