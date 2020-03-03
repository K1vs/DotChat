namespace K1vs.DotChat.Common.Exceptions.Rules
{
    using System;

    public class DotChatEditImmutableMessageException: DotChatImmutableMessageException
    {
        public DotChatEditImmutableMessageException(Guid messageId, Guid chatId, Guid userId) 
            : base(new ErrorInfo(new ErrorCode(ErrorType.RulesEditImmutable, ErrorModule.Worker, ErrorOperation.Edit, ErrorEntity.Message), $"Can't edit immutable message {messageId} in chat {chatId}", chatId, userId, null))
        {
        }
    }
}
