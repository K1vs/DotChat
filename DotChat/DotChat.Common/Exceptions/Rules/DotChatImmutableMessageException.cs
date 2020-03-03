namespace K1vs.DotChat.Common.Exceptions.Rules
{
    using System;

    public class DotChatImmutableMessageException: DotChatException
    {
        public DotChatImmutableMessageException(ErrorInfo error) : base(error)
        {
        }

        public DotChatImmutableMessageException(ErrorInfo error, Exception innerException) : base(error, innerException)
        {
        }
    }
}
