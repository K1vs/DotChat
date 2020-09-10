namespace K1vs.DotChat.Services
{
    using System.Collections.Generic;
    using Chats;
    using Common.Filters;
    using Common.Paging;
    using Configuration;
    using DotChat;
    using Messages;
    using Messages.Typed;
    using Participants;

    public class DotChatFacade: IDotChat
    {
        public DotChatFacade(IChatsService chats, IChatParticipantsService chatParticipants, IChatMessagesService chatMessages)
        {
            Chats = chats;
            ChatParticipants = chatParticipants;
            ChatMessages = chatMessages;
        }

        public IChatsService Chats { get; }
        public IChatParticipantsService ChatParticipants { get; }
        public IChatMessagesService ChatMessages { get; }
    }
}
