namespace K1vs.DotChat.Tests.Integration.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class CheckChatException: Exception
    {
        public CheckChatException()
        {
        }

        protected CheckChatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CheckChatException(string message) : base(message)
        {
        }

        public CheckChatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
