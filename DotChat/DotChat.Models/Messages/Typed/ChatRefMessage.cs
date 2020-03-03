namespace K1vs.DotChat.Models.Messages.Typed
{
    using System;
    using DotChat.Chats;
    using DotChat.Messages.Typed;

    public class ChatRefMessage<TChatInfo> : IChatRefMessage<TChatInfo>
        where TChatInfo : IChatInfo
    {
        public ChatRefMessage()
        {
        }

        public ChatRefMessage(Guid chatId, TChatInfo chatInfo, string style = null, string metadata = null)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
            Style = style;
            Metadata = metadata;
        }

        public Guid ChatId { get; set; }
        public TChatInfo ChatInfo { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
    }
}
