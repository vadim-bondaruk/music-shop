namespace Shop.BLL.Exceptions
{
    using System;

    /// <summary>
    /// Invalid Track ID Exception
    /// </summary>
    public class InvalidAlbumIdException : Exception
    {
        public InvalidAlbumIdException() : base() { }

        public InvalidAlbumIdException(string message) : base(message) { }

        public InvalidAlbumIdException(string message, Exception innerException) : base(message, innerException) { }

    }
}