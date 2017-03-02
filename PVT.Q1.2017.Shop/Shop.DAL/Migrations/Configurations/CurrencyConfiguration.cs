
namespace Shop.DAL.Migrations.Configurations
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Currency mapping
    /// </summary>
    public class CurrencyConfiguration : EntityTypeConfiguration<Currency>
    {
        public CurrencyConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.ShortName).IsRequired().HasMaxLength(3);
            Property(c => c.FullName).IsRequired().HasMaxLength(150);
            Property(c => c.Code).IsRequired();
            Property(c => c.IsDeleted);

            ToTable("tbCurrencies");
        }
    }
}
