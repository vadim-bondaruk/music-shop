namespace Shop.DAL.Migrations.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Shop.Common.Models;

    /// <summary>
    ///     The <see cref="Album" /> configuration.
    /// </summary>
    public class UserDataConfiguration : EntityTypeConfiguration<UserData>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AlbumConfiguration" /> class.
        /// </summary>
        public UserDataConfiguration()
        {
            this.HasKey(t => t.Id);
            this.Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(u => u.Dicount).IsOptional();
            this.HasRequired(u => u.UserCurrency)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CurrencyId)
                .WillCascadeOnDelete(false);

            this.ToTable("tbUsersData");
        }
    }
}