namespace Shop.BLL.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// </summary>
    public class TrackNotFoundException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackNotFoundException"/> class.
        /// </summary>
        public TrackNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public TrackNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner exception.
        /// </param>
        public TrackNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackNotFoundException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected TrackNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion //Constructors
    }
}