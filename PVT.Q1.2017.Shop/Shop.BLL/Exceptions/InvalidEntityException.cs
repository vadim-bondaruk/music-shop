namespace Shop.BLL.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// The invalid entity exception.
    /// </summary>
    [Serializable]
    public class InvalidEntityException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEntityException"/> class.
        /// </summary>
        public InvalidEntityException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEntityException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public InvalidEntityException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEntityException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner exception.
        /// </param>
        public InvalidEntityException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEntityException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected InvalidEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion //Constructors
    }
}