namespace Shop.DAL.Migrations.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;

    /// <summary>
    /// The <see cref="User"/> configuration.
    /// </summary>
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// 
        /// </summary>
        public UserConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.BirthDate).IsOptional();
            Property(p => p.Country).IsOptional().HasMaxLength(15);
            Property(p => p.Email).IsRequired().HasMaxLength(40);
            Property(p => p.FirstName).IsRequired().HasMaxLength(15).IsUnicode();
            Property(p => p.LastName).IsRequired().HasMaxLength(25).IsUnicode();
            Property(p => p.Login).IsRequired().HasMaxLength(15).IsUnicode();
            Property(p => p.Password).IsRequired().HasMaxLength(50);
            Property(p => p.PhoneNumber).IsOptional().HasMaxLength(30);
            Property(p => p.Sex).IsRequired();
            Property(p => p.UserRoles).IsRequired();

            ToTable("Users");
        }
    }
}