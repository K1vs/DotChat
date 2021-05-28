namespace K1vs.DotChat.Models.Messages.Typed
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Messages.Typed;

    public class ChatRefMessage: IChatRefMessage
    {
        public ChatRefMessage()
        {
        }

        public ChatRefMessage(Guid chatId, IChatInfo chatInfo, IReadOnlyList<string> styles = null)
        {
            ChatId = chatId;
            ChatInfo = chatInfo;
            Styles = styles;
        }

        public Guid ChatId { get; set; }
        public IChatInfo ChatInfo { get; set; }
        public IReadOnlyList<string> Styles { get; set; }
    }
}
