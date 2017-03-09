namespace Shop.DAL.Migrations.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The <see cref="Album"/> configuration.
    /// </summary>
    public class UserConfiguration : EntityTypeConfiguration<User>    
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumConfiguration"/> class.
        /// </summary>
        public UserConfiguration()
        {
            HasKey(t => t.Id);
            Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(u => u.IdentityKey).HasMaxLength(128).IsRequired();
            Property(u => u.Dicount).IsOptional();
            HasRequired(u => u.UserCurrency).WithMany(c => c.Users).HasForeignKey(u => u.CurrencyId).WillCascadeOnDelete(false);


            ToTable("tbUsers");
        }
    }
}