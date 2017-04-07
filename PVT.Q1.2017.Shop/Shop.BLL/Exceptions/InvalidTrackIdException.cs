namespace Shop.BLL.Exceptions
{
    using System;

    /// <summary>
    /// Invalid Track ID Exception
    /// </summary>
    public class InvalidTrackIdException : Exception
    {
        public InvalidTrackIdException() : base() { }

        public InvalidTrackIdException(string message) : base(message) { }

        public InvalidTrackIdException(string message, Exception innerException) : base (message, innerException) { }

    }
}
