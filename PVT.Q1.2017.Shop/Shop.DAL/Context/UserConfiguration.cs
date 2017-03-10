namespace Shop.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Determines configuration of model
    /// </summary>
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// Contains specific model settings (User) 
        /// </summary>
        public UserConfiguration()
        {
            Property(p => p.BirthDate).IsOptional();

            Property(p => p.Country).IsOptional().HasMaxLength(15);

            Property(p => p.Email).IsRequired().HasMaxLength(40);

            Property(p => p.FirstName).IsRequired().HasMaxLength(15).IsUnicode();

            Property(p => p.LastName).IsRequired().HasMaxLength(25).IsUnicode();

            Property(p => p.Login).IsRequired().HasMaxLength(15).IsUnicode();

            Property(p => p.Password).IsRequired().HasMaxLength(50);

            Property(p => p.PhoneNumber).IsOptional().HasMaxLength(28);

            Property(p => p.Sex).IsRequired();

            Property(p => p.UserRole).IsRequired();

            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
