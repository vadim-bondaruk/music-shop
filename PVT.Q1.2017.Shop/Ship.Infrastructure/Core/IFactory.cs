namespace Shop.Infrastructure.Core
{
    /// <summary>
    /// Factory
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Create instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Create<T>() where T : class;
    }
}
