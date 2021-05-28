namespace K1vs.DotChat.Basic.EventBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.EventBuilders;
    using DotChat.Events.Chat;
    using DotChat.Events.Chats;

    public class ChatsEventBuilder: IChatsEventBuilder
    {
        public IChatAddedEvent BuildChatAddedEvent(Guid initiatorUserId, IChat chat)
        {
            return new ChatAddedEvent(initiatorUserId, chat);
        }

        public IChatInfoEditedEvent BuildChatInfoEditedEvent(Guid initiatorUserId, Guid chatId, IChatInfo chatInfo)
        {
            return new ChatInfoEditedEvent(initiatorUserId, chatId, chatInfo);
        }

        public IChatRemovedEvent BuildChatRemovedEvent(Guid initiatorUserId, Guid chatId, IChatInfo chatInfo)
        {
            return new ChatRemovedEvent(initiatorUserId, chatId, chatInfo);
        }
    }
}
