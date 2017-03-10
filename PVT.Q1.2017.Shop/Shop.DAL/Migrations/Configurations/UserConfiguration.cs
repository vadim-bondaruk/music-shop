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
            Property(p => p.BirthDate).IsOptional();
            Property(p => p.Country).IsOptional().HasMaxLength(15);
            Property(p => p.Email).IsRequired().HasMaxLength(40);
            Property(p => p.FirstName).IsRequired().HasMaxLength(15).IsUnicode();
            Property(p => p.LastName).IsRequired().HasMaxLength(25).IsUnicode();
            Property(p => p.Login).IsRequired().HasMaxLength(15).IsUnicode();
            Property(p => p.Password).IsRequired().HasMaxLength(50);
            Property(p => p.PhoneNumber).IsOptional().HasMaxLength(30);
            Property(p => p.Sex).IsRequired();
            Property(p => p.UserRole).IsRequired();

            ToTable("tbUsers");
        }
    }
}