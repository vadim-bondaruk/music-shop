namespace Shop.DAL.Migrations.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using Common.Models;

    /// <summary>
    /// The <see cref="Genre"/> configuration.
    /// </summary>
    public class GenreConfiguration : EntityTypeConfiguration<Genre>    
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenreConfiguration"/> class.
        /// </summary>
        public GenreConfiguration()
        {
            this.ToTable("tbGenres");
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).IsRequired().HasMaxLength(150).IsVariableLength().IsUnicode();
        }
    }
}