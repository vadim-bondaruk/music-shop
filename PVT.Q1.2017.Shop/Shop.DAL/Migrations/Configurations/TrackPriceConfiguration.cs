namespace Shop.DAL.Migrations.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;

    /// <summary>
    /// The <see cref="TrackPrice"/> configuration.
    /// </summary>
    public class TrackPriceConfiguration : EntityTypeConfiguration<TrackPrice>    
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackConfiguration"/> class.
        /// </summary>
        public TrackPriceConfiguration()
        {
            this.HasKey(t => t.Id);
            this.Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.IsDeleted);
            this.HasRequired(t => t.PriceLevel).WithMany(t => t.TrackPriceLevels).HasForeignKey(t => t.PriceLevelId).WillCascadeOnDelete(false);
            this.HasRequired(t => t.Track).WithMany(t => t.TrackPrices).HasForeignKey(t => t.TrackId).WillCascadeOnDelete(false);
            this.HasRequired(t => t.Currency).WithMany(t => t.TrackPrices).HasForeignKey(t => t.CurrencyId).WillCascadeOnDelete(false);
            this.ToTable("tbTrackPrices");
        }
    }
}