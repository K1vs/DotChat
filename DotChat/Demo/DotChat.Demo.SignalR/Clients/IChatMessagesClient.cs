namespace K1vs.DotChat.Demo.SignalR.Clients
{
    using System.Collections.Generic;
    using Basic.Messages;
    using Basic.Messages.Typed;
    using Implementations.SignalR;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;

    public interface IChatMessagesClient: IChatMessagesClient<ChatInfo, ChatUser, ChatMessage, ChatMessageInfo, TextMessage, QuoteMessage,
        List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
    }
}
