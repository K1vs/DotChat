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

    public class ChatParticipantsHub<TChatParticipantsClient, TParticipationResultCollection, TParticipationResult, TChatParticipant, TParticipationCandidateCollection, TParticipationCandidate>
        : Hub<TChatParticipantsClient>
        where TChatParticipantsClient: class, IChatParticipantsClient<TParticipationResultCollection, TParticipationResult, TChatParticipant>
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
    {
        private readonly IChatParticipantsService<TParticipationCandidateCollection, TParticipationCandidate> _chatParticipantsService;

        public ChatParticipantsHub(IChatParticipantsService<TParticipationCandidateCollection, TParticipationCandidate> chatParticipantsService)
        {
            _chatParticipantsService = chatParticipantsService;
        }

        public async Task Add(Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null,
            string metadata = null)
        {
            await _chatParticipantsService.Add(CurrentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public async Task Invite(Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null,
            string metadata = null)
        {
            await _chatParticipantsService.Invite(CurrentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public async Task Apply(Guid chatId, ChatParticipantType chatParticipantType, string style = null,
            string metadata = null)
        {
            await _chatParticipantsService.Apply(CurrentUserId, chatId, chatParticipantType, style, metadata);
        }

        public async Task Remove(Guid chatId, Guid userId)
        {
            await _chatParticipantsService.Remove(CurrentUserId, chatId, userId);
        }

        public async Task Block(Guid chatId, Guid userId)
        {
            await _chatParticipantsService.Block(CurrentUserId, chatId, userId);
        }

        public async Task ChangeType(Guid chatId, Guid userId, ChatParticipantType chatParticipantType,
            string style = null, string metadata = null)
        {
            await _chatParticipantsService.ChangeType(CurrentUserId, chatId, userId, chatParticipantType, style, metadata);
        }

        public async Task Append(Guid chatId, TParticipationCandidateCollection addCandidates, TParticipationCandidateCollection inviteCandidates)
        {
            await _chatParticipantsService.Append(CurrentUserId, chatId, addCandidates, inviteCandidates);
        }

        protected virtual Guid CurrentUserId => Guid.Parse(Context.User.Identity.Name);
    }
}
