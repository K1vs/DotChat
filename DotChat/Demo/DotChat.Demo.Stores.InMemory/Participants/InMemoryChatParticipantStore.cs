namespace K1vs.DotChat.Demo.Stores.InMemory.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DotChat.Participants;
    using DotChat.Stores.Participants;
    using K1vs.DotChat.Models.Participants;

    public class InMemoryChatParticipantStore: InMemoryReadChatParticipantStore, IChatParticipantStore
    {
        public InMemoryChatParticipantStore(InMemoryStore store) : base(store)
        {
        }

        public async Task<IChatParticipant> Set(Guid chatId, IChatUser chatUser, ChatParticipantType participantType, ChatParticipantStatus participantStatus,
            Guid setterId)
        {
            await Task.Yield();
            return SetUser(chatId, chatUser, participantType, participantStatus);
        }

        public async Task<IChatParticipant> Set(Guid chatId, IChatUser chatUser, ChatParticipantStatus participantStatus, Guid setterId)
        {
            await Task.Yield();
            var participants = (List<ChatParticipant>)Store.Chats[chatId].Participants;
            lock (participants)
            {
                var exist = participants.FirstOrDefault(r => r.UserId == chatUser.UserId);
                if (exist == null)
                {
                    exist = new ChatParticipant(ChatParticipantType.ReadOnlyParticipant, participantStatus, DateTime.MinValue, -1, 0, chatUser);
                    participants.Add(exist);
                }
                else
                {
                    exist.Version += 1;
                    exist.ChatParticipantStatus = participantStatus;
                }
                return exist;
            }
        }

        public async Task<IReadOnlyCollection<IChatParticipant>> Set(Guid chatId, IEnumerable<IChatUser> chatUsers, ChatParticipantType participantType,
            ChatParticipantStatus participantStatus, Guid setterId)
        {
            await Task.Yield();
            return chatUsers.Select(chatUser => SetUser(chatId, chatUser, participantType, participantStatus)).ToList();
        }

        public async Task<IChatParticipant> ChangeType(Guid chatId, Guid userId, ChatParticipantType participantType, Guid setterId)
        {
            await Task.Yield();
            var p = (ChatParticipant)Store.Chats[chatId].Participants.FirstOrDefault(r => r.UserId == userId);
            p.ChatParticipantType = participantType;
            p.Version += 1;
            return p;
        }

        private ChatParticipant SetUser(Guid chatId, IChatUser chatUser, ChatParticipantType participantType, ChatParticipantStatus participantStatus)
        {
            var participants = (List<ChatParticipant>)Store.Chats[chatId].Participants;
            lock (participants)
            {
                var exist = participants.FirstOrDefault(r => r.UserId == chatUser.UserId);
                if (exist == null)
                {
                    exist = new ChatParticipant(participantType, participantStatus, DateTime.MinValue, -1, 0, chatUser);
                    participants.Add(exist);
                }
                else
                {
                    exist.Version += 1; 
                    exist.ChatParticipantType = participantType;
                    exist.ChatParticipantStatus = participantStatus;
                }
                return exist;
            }
        }
    }
}
