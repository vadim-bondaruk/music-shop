namespace Shop.DAL.Migrations.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Shop.Common.Models;

    /// <summary>
    ///     The <see cref="PriceLevel" /> configuration.
    /// </summary>
    public class PriceLevelConfiguration : EntityTypeConfiguration<PriceLevel>
    {
        /// <summary>
        ///     C'tor
        /// </summary>
        public PriceLevelConfiguration()
        {
            this.HasKey(t => t.Id);
            this.Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name).IsRequired().HasMaxLength(150).IsUnicode().IsVariableLength();

            this.ToTable("tbPriceLevels");
        }
    }
}