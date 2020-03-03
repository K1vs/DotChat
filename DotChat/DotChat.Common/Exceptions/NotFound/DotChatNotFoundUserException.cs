namespace K1vs.DotChat.Common.Exceptions.NotFound
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DotChatNotFoundUserException : DotChatNotFoundException
    {
        public DotChatNotFoundUserException(ErrorModule module, ErrorOperation operation, Guid? chatId, Guid? userId, Exception innerException = null)
            : base(new ErrorInfo(new ErrorCode(ErrorType.NotFound, module, operation, ErrorEntity.User), $"User {chatId} not found", chatId, userId, null), innerException)
        {
        }
    }
}
