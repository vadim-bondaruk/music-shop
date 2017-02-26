namespace Shop.DAL.Migrations.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;

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
            this.ToTable("tbVotes");
            this.HasKey(t => t.Id);
            this.Property(t => t.Mark).IsRequired();

            this.HasRequired(v => v.Track).WithMany(t => t.Votes);
        }
    }
}