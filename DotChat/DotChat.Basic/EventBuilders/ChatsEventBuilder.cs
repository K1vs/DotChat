namespace K1vs.DotChat.Basic.EventBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using DotChat.Chats;
    using DotChat.EventBuilders;
    using DotChat.Events.Chat;
    using Events.Chat;
    using Models.Chats;
    using Models.Participants;

    public class ChatsEventBuilder: IChatsEventBuilder<Chat, ChatInfo, List<ChatParticipant>, ChatParticipant>
    {
        public IChatAddedEvent<Chat, List<ChatParticipant>, ChatParticipant> BuildChatAddedEvent(Guid initiatorUserId, Chat chat)
        {
            return new ChatAddedEvent(initiatorUserId, chat);
        }

        public IChatInfoEditedEvent<ChatInfo> BuildChatInfoEditedEvent(Guid initiatorUserId, Guid chatId, ChatInfo chatInfo)
        {
            return new ChatInfoEditedEvent(initiatorUserId, chatId, chatInfo);
        }

        public IChatRemovedEvent<ChatInfo> BuildChatRemovedEvent(Guid initiatorUserId, Guid chatId, ChatInfo chatInfo)
        {
            return new ChatRemovedEvent(initiatorUserId, chatId, chatInfo);
        }
    }
}
