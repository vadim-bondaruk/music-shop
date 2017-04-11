namespace Shop.BLL.Exceptions
{
    using System;

    public class InvalidPaymentOperation : Exception
    {
        public InvalidPaymentOperation() : base() { }

        public InvalidPaymentOperation(string message) : base(message) { }

        public InvalidPaymentOperation(string message, Exception innerException) : base (message, innerException) { }
    }
}
