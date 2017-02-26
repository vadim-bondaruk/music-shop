// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidTrackException.cs" company="PVT.Q1.2017">
//   PVT.Q1.2017
// </copyright>
// <summary>
//   Defines the InvalidTrackException type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Shop.BLL.Exceptions
{
    #region

    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    #endregion

    /// <summary>
    /// The invalid track exception.
    /// </summary>
    [Serializable]
    public class InvalidTrackException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="InvalidTrackException" /> class.
        /// </summary>
        public InvalidTrackException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="InvalidTrackException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InvalidTrackException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="InvalidTrackException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public InvalidTrackException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="InvalidTrackException" /> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected InvalidTrackException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}