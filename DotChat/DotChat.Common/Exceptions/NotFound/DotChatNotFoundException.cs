namespace K1vs.DotChat.Common.Exceptions.NotFound
{
    using System;

    public class DotChatNotFoundException: DotChatException
    {
        public DotChatNotFoundException(ErrorInfo error) : base(error)
        {
        }

        public DotChatNotFoundException(ErrorInfo error, Exception innerException) : base(error, innerException)
        {
        }
    }
}
