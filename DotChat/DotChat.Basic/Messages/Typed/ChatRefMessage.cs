namespace K1vs.DotChat.Basic.Messages.Typed
{
    using System;
    using DotChat.Chats;
    using DotChat.Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;

    public class ChatRefMessage : ChatRefMessage<ChatInfo>
    {
        public ChatRefMessage()
        {
        }

        public ChatRefMessage(Guid chatId, ChatInfo chatInfo, string style = null, string metadata = null) : base(chatId, chatInfo, style, metadata)
        {
        }
    }
}
