namespace K1vs.DotChat.Tests.Integration.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class CheckMessageException: Exception
    {
        public CheckMessageException()
        {
        }

        protected CheckMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CheckMessageException(string message) : base(message)
        {
        }

        public CheckMessageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
