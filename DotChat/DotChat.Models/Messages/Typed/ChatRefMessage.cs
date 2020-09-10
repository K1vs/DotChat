namespace K1vs.DotChat.Models.Messages.Typed
{
    using System;
    using DotChat.Chats;
    using DotChat.Messages.Typed;

    public class ChatRefMessage: IChatRefMessage
    {
        public ChatRefMessage()
        {
        }

        public ChatRefMessage(Guid chatId, IChatInfo chatInfo, string style = null, string metadata = null)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
            Style = style;
            Metadata = metadata;
        }

        public Guid ChatId { get; set; }
        public IChatInfo ChatInfo { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
    }
}
