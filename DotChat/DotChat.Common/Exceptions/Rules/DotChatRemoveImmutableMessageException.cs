namespace K1vs.DotChat.Common.Exceptions.Rules
{
    using System;

    public class DotChatRemoveImmutableMessageException: DotChatImmutableMessageException
    {
        public DotChatRemoveImmutableMessageException(Guid messageId, Guid chatId, Guid userId)
            : base(new ErrorInfo(new ErrorCode(ErrorType.RulesEditImmutable, ErrorModule.Worker, ErrorOperation.Edit, ErrorEntity.Message), $"Can't remove immutable message {messageId} from chat {chatId}", chatId, userId, null))
        {
        }
    }
}
