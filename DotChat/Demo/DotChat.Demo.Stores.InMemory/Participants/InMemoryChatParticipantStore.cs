namespace K1vs.DotChat.Demo.Stores.InMemory.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DotChat.Participants;
    using DotChat.Stores.Participants;
    using K1vs.DotChat.Models.Participants;

    public class InMemoryChatParticipantStore: InMemoryReadChatParticipantStore, IChatParticipantStore<ChatParticipant, ChatUser>
    {
        public InMemoryChatParticipantStore(InMemoryStore store) : base(store)
        {
        }

        public async Task<ChatParticipant> Set(Guid chatId, ChatUser chatUser, ChatParticipantType participantType, ChatParticipantStatus participantStatus,
            Guid setterId)
        {
            await Task.Yield();
            return SetUser(chatId, chatUser, participantType, participantStatus);
        }

        public async Task<ChatParticipant> Set(Guid chatId, ChatUser chatUser, ChatParticipantStatus participantStatus, Guid setterId)
        {
            await Task.Yield();
            var participants = Store.Chats[chatId].Participants;
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
                    exist.ChatParticipantStatus = participantStatus;
                }
                return exist;
            }
        }

        public async Task<IReadOnlyCollection<ChatParticipant>> Set(Guid chatId, IEnumerable<ChatUser> chatUsers, ChatParticipantType participantType,
            ChatParticipantStatus participantStatus, Guid setterId)
        {
            await Task.Yield();
            return chatUsers.Select(chatUser => SetUser(chatId, chatUser, participantType, participantStatus)).ToList();
        }

        public async Task<ChatParticipant> ChangeType(Guid chatId, Guid userId, ChatParticipantType participantType, Guid setterId)
        {
            await Task.Yield();
            var p = Store.Chats[chatId].Participants.FirstOrDefault(r => r.UserId == userId);
            p.ChatParticipantType = participantType;
            return p;
        }

        private ChatParticipant SetUser(Guid chatId, ChatUser chatUser, ChatParticipantType participantType, ChatParticipantStatus participantStatus)
        {
            var participants = Store.Chats[chatId].Participants;
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
                    exist.ChatParticipantType = participantType;
                    exist.ChatParticipantStatus = participantStatus;
                }
                return exist;
            }
        }
    }
}
