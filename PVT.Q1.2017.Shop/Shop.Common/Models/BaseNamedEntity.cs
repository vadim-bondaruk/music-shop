namespace Shop.Common.Models
{
    using Infrastructure.Models;

    /// <summary>
    /// The base entity with name.
    /// </summary>
    public abstract class BaseNamedEntity : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
