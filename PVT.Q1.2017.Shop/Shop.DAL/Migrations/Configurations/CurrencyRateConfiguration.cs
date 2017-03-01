﻿
namespace Shop.DAL.Migrations.Configurations
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Currency mapping
    /// </summary>
    public class CurrencyRateConfiguration : EntityTypeConfiguration<CurrencyRate>
    {
        public CurrencyRateConfiguration()
        {
            HasKey(cr => cr.Id);
            Property(cr => cr.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(cr => cr.CrossCourse).IsRequired();
            Property(cr => cr.Date).IsRequired();
            HasRequired(cr => cr.Currency).WithMany(c => c.CurrencyRates).HasForeignKey(cr => cr.CurrencyId).WillCascadeOnDelete(false);
            HasRequired(cr => cr.TargetCurrency).WithMany(c => c.TargetCurrencyRates).HasForeignKey(cr => cr.TargetCurrencyId).WillCascadeOnDelete(false);

            ToTable("tbCurrencyRates");
        }
    }
}
