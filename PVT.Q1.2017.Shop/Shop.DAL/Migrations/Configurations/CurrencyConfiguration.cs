namespace Shop.DAL.Migrations.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using Common.Models;

    /// <summary>
    /// Currency configuration
    /// </summary>
    public class CurrencyConfiguration : EntityTypeConfiguration<Currency>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyConfiguration"/> class.
        /// </summary>
        public CurrencyConfiguration()
        {
            this.HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(new[] { new IndexAttribute("UniqueCurrencyName_Index") { IsUnique = true } }));
            Property(c => c.Code)
                .IsRequired()
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(new[] { new IndexAttribute("UniqueCurrencyCode_Index") { IsUnique = true } }));
            Property(c => c.FullName).IsOptional().IsUnicode().HasMaxLength(150);
            Property(c => c.Symbol).IsOptional().IsUnicode().HasMaxLength(10);
            this.Property(c => c.IsDeleted);

            this.ToTable("tbCurrencies");
        }
    }
}