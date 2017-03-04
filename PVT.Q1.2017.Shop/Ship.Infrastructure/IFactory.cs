namespace Shop.Infrastructure
{
    /// <summary>
    /// The common factory interface.
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Returns the instance of the specified type.
        /// </summary>
        /// <typeparam name="T">A model type to create.</typeparam>
        /// <returns>
        /// The instance of the specified type.
        /// </returns>
        T Create<T>();
    }
}