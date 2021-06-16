namespace K1vs.DotChat.Implementations.AspNetCore.SignalR
{
    using K1vs.DotChat.Chats;
    using K1vs.DotChat.Common.Filters;
    using K1vs.DotChat.Common.Paging;
    using K1vs.DotChat.Configuration;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Participants;
    using K1vs.DotChat.Services;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class ChatParticipantsHub<TChatParticipantsClient, TParticipationResultCollection, TParticipationResult, TChatParticipant, TParticipationCandidateCollection, TParticipationCandidate>
        : Hub<TChatParticipantsClient>
        where TChatParticipantsClient: class, IChatParticipantsClient<TParticipationResultCollection, TParticipationResult, TChatParticipant>
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
    {
        protected readonly IChatParticipantsService<TParticipationCandidateCollection, TParticipationCandidate> ChatParticipantsService;

        public ChatParticipantsHub(IChatParticipantsService<TParticipationCandidateCollection, TParticipationCandidate> chatParticipantsService)
        {
            ChatParticipantsService = chatParticipantsService;
        }

        public virtual async Task Add(Guid chatId, Guid userId, ChatParticipantType chatParticipantType)
        {
            await ChatParticipantsService.Add(CurrentUserId, chatId, userId, chatParticipantType, null, null);
        }

        public virtual async Task Invite(Guid chatId, Guid userId, ChatParticipantType chatParticipantType)
        {
            await ChatParticipantsService.Invite(CurrentUserId, chatId, userId, chatParticipantType, null, null);
        }

        public virtual async Task Apply(Guid chatId, ChatParticipantType chatParticipantType)
        {
            await ChatParticipantsService.Apply(CurrentUserId, chatId, chatParticipantType, null, null);
        }

        public virtual async Task Remove(Guid chatId, Guid userId)
        {
            await ChatParticipantsService.Remove(CurrentUserId, chatId, userId);
        }

        public virtual async Task Block(Guid chatId, Guid userId)
        {
            await ChatParticipantsService.Block(CurrentUserId, chatId, userId);
        }

        public virtual async Task ChangeType(Guid chatId, Guid userId, ChatParticipantType chatParticipantType)
        {
            await ChatParticipantsService.ChangeType(CurrentUserId, chatId, userId, chatParticipantType, null, null);
        }

        public virtual async Task Append(Guid chatId, TParticipationCandidateCollection addCandidates, TParticipationCandidateCollection inviteCandidates)
        {
            await ChatParticipantsService.Append(CurrentUserId, chatId, addCandidates, inviteCandidates);
        }

        protected virtual Guid CurrentUserId => Guid.Parse(Context.User.Identity.Name);
    }
}
