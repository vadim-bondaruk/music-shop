using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Shop.BLL.Exceptions
{
    [Serializable]
    public class InvalidTrackException : Exception
    {
        #region Constructors

        public InvalidTrackException()
        {}

        public InvalidTrackException(String message) : base(message)
        {}

        public InvalidTrackException(String message, Exception inner) : base(message, inner)
        {}

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected InvalidTrackException(SerializationInfo info, StreamingContext context) : base(info, context)
        {}

        #endregion //Constructors
    }
}