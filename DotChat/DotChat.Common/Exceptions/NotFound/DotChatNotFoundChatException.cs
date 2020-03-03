namespace K1vs.DotChat.Common.Exceptions.NotFound
{
    using System;

    public class DotChatNotFoundChatException: DotChatNotFoundException
    {
        public DotChatNotFoundChatException(ErrorModule module, ErrorOperation operation, Guid? chatId, Guid? userId, Exception innerException = null)
            : base(new ErrorInfo(new ErrorCode(ErrorType.NotFound, module, operation, ErrorEntity.Chat), $"Chat {chatId} not found", chatId, userId, null), innerException)
        {
        }
    }
}
