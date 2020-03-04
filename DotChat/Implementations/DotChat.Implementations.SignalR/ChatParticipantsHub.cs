namespace K1vs.DotChat.Implementations.SignalR
{
    using K1vs.DotChat.Chats;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using K1vs.DotChat.Configuration;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Participants;
    using K1vs.DotChat.Services;
    using Microsoft.AspNet.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ChatParticipantsHub<TParticipationCandidateCollection, TParticipationCandidate>
        : Hub, IChatParticipantsService<TParticipationCandidateCollection, TParticipationCandidate>
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
    {
        private readonly IChatParticipantsService<TParticipationCandidateCollection, TParticipationCandidate> _chatParticipantsService;

        public ChatParticipantsHub(IChatParticipantsService<TParticipationCandidateCollection, TParticipationCandidate> chatParticipantsService)
        {
            _chatParticipantsService = chatParticipantsService;
        }

        public async Task Add(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null,
            string metadata = null)
        {
            await _chatParticipantsService.Add(currentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public async Task Invite(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null,
            string metadata = null)
        {
            await _chatParticipantsService.Invite(currentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public async Task Apply(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType, string style = null,
            string metadata = null)
        {
            await _chatParticipantsService.Apply(currentUserId, chatId, chatParticipantType, style, metadata);
        }

        public async Task Remove(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null,
            string metadata = null)
        {
            await _chatParticipantsService.Remove(currentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public async Task Block(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null,
            string metadata = null)
        {
            await _chatParticipantsService.Block(currentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public async Task ChangeType(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType,
            string style = null, string metadata = null)
        {
            await _chatParticipantsService.ChangeType(currentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public async Task BulkAppendChatParticipants(Guid currentUserId, Guid chatId, TParticipationCandidateCollection addCandidates,
            TParticipationCandidateCollection inviteCandidates)
        {
            await _chatParticipantsService.BulkAppendChatParticipants(currentUserId, chatId, addCandidates, inviteCandidates);
        }
    }
}
