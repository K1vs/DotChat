namespace K1vs.DotChat.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Bus;
    using Chats;
    using Commands.Participants;
    using Common;
    using Configuration;
    using EventBuilders;
    using Participants;
    using Security;
    using Stores.Participants;
    using Stores.Users;

    public class ChatParticipantsWorker<TChatWorkersConfiguration, TParticipationResultCollection, TParticipationResult, TChatParticipant, TParticipationCandidateCollection, TParticipationCandidate, TChatUser> : WorkerBase<TChatWorkersConfiguration>, 
        IChatParticipantsWorker<TParticipationCandidateCollection, TParticipationCandidate>
        where TChatWorkersConfiguration : IChatWorkersConfiguration
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
        where TChatUser : IChatUser
    {
        private readonly IChatParticipantsPermissionValidator<TParticipationCandidateCollection, TParticipationCandidate> _chatParticipantsPermissionValidator;

        private readonly IChatParticipantStore<TChatParticipant, TChatUser> _chatParticipantStore;

        private readonly IReadUserStore<TChatUser> _readUserStore;

        private readonly IChatParticipantsEventBuilder<TParticipationResultCollection, TParticipationResult, TChatParticipant> _chatParticipantsEventBuilder;

        protected ChatParticipantsWorker(TChatWorkersConfiguration chatWorkersConfiguration, IChatParticipantsPermissionValidator<TParticipationCandidateCollection, TParticipationCandidate> chatParticipantsPermissionValidator, IChatParticipantStore<TChatParticipant, TChatUser> chatParticipantStore, IReadUserStore<TChatUser> readUserStore, IChatParticipantsEventBuilder<TParticipationResultCollection, TParticipationResult, TChatParticipant> chatParticipantsEventBuilder) : base(chatWorkersConfiguration)
        {
            _chatParticipantsPermissionValidator = chatParticipantsPermissionValidator;
            _chatParticipantStore = chatParticipantStore;
            _readUserStore = readUserStore;
            _chatParticipantsEventBuilder = chatParticipantsEventBuilder;
        }

        public async Task Handle(IAddChatParticipantCommand command, IChatBusContext chatEventPublisher)
        {
            await _chatParticipantsPermissionValidator.ValidateAdd(command.InitiatorUserId, command.ChatId, command.UserId, command.ChatParticipantType, command.Style, command.Metadata, WorkerName).ConfigureAwait(false);
            var currentParticipant = await _chatParticipantStore.Retrieve(command.ChatId, command.UserId).ConfigureAwait(false);
            var participant = await SetParticipationCandidate(command, ChatParticipantStatus.Active).ConfigureAwait(false);
            var @event = _chatParticipantsEventBuilder.BuildChatParticipantAddedEvent(command.InitiatorUserId, command.ChatId, participant, currentParticipant?.ChatParticipantStatus);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public async Task Handle(IApplyToChatCommand command, IChatBusContext chatEventPublisher)
        {
            await _chatParticipantsPermissionValidator.ValidateApply(command.InitiatorUserId, command.ChatId, command.ChatParticipantType, command.Style, command.Metadata, WorkerName).ConfigureAwait(false);
            var currentParticipant = await _chatParticipantStore.Retrieve(command.ChatId, command.InitiatorUserId).ConfigureAwait(false);
            var participant = await SetParticipationCandidate(command.ChatId, command.InitiatorUserId, command.ChatParticipantType, ChatParticipantStatus.Applied, command.Style, command.Metadata, command.InitiatorUserId).ConfigureAwait(false);
            var @event = _chatParticipantsEventBuilder.BuildChatParticipantAppliedEvent(command.InitiatorUserId, command.ChatId, participant, currentParticipant?.ChatParticipantStatus);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public async Task Handle(IInviteChatParticipantCommand command, IChatBusContext chatEventPublisher)
        {
            await _chatParticipantsPermissionValidator.ValidateInvite(command.InitiatorUserId, command.ChatId, command.UserId, command.ChatParticipantType, command.Style, command.Metadata, WorkerName).ConfigureAwait(false);
            var currentParticipant = await _chatParticipantStore.Retrieve(command.ChatId, command.UserId).ConfigureAwait(false);
            var participant = await SetParticipationCandidate(command, ChatParticipantStatus.Invited).ConfigureAwait(false);
            var @event = _chatParticipantsEventBuilder.BuildChatParticipantInvitedEvent(command.InitiatorUserId, command.ChatId, participant, currentParticipant?.ChatParticipantStatus);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public async Task Handle(IRemoveChatParticipantCommand command, IChatBusContext chatEventPublisher)
        {
            await _chatParticipantsPermissionValidator.ValidateRemove(command.InitiatorUserId, command.ChatId, command.UserId, WorkerName).ConfigureAwait(false);
            var currentParticipant = await _chatParticipantStore.Retrieve(command.ChatId, command.UserId).ConfigureAwait(false);
            var participant = await SetUser(command, ChatParticipantStatus.Removed).ConfigureAwait(false);
            var @event = _chatParticipantsEventBuilder.BuildChatParticipantRemovedEvent(command.InitiatorUserId, command.ChatId, participant, currentParticipant?.ChatParticipantStatus);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public async Task Handle(IBlockChatParticipantCommand command, IChatBusContext chatEventPublisher)
        {
            await _chatParticipantsPermissionValidator.ValidateRemove(command.InitiatorUserId, command.ChatId, command.UserId, WorkerName).ConfigureAwait(false);
            var currentParticipant = await _chatParticipantStore.Retrieve(command.ChatId, command.UserId).ConfigureAwait(false);
            var participant = await SetUser(command, ChatParticipantStatus.Blocked).ConfigureAwait(false);
            var @event = _chatParticipantsEventBuilder.BuildChatParticipantBlockedEvent(command.InitiatorUserId, command.ChatId, participant, currentParticipant?.ChatParticipantStatus);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public async Task Handle(IChangeChatParticipantTypeCommand command, IChatBusContext chatEventPublisher)
        {
            await _chatParticipantsPermissionValidator.ValidateChangeType(command.InitiatorUserId, command.ChatId, command.UserId, command.ChatParticipantType, command.Style, command.Metadata, WorkerName).ConfigureAwait(false);
            var currentParticipant = await _chatParticipantStore.Retrieve(command.ChatId, command.UserId).ConfigureAwait(false);
            var participant = await _chatParticipantStore.ChangeType(command.ChatId, command.UserId, command.ChatParticipantType, command.InitiatorUserId).ConfigureAwait(false);
            var @event = _chatParticipantsEventBuilder.BuildChatParticipantTypeChangedEvent(command.InitiatorUserId, command.ChatId, participant, currentParticipant?.ChatParticipantStatus);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public async Task Handle(IAppendChatParticipantsCommand<TParticipationCandidateCollection, TParticipationCandidate> command, IChatBusContext chatEventPublisher)
        {
            await _chatParticipantsPermissionValidator.ValidateAppend(command.InitiatorUserId, command.ChatId, command.ToAdd, command.ToInvite, WorkerName).ConfigureAwait(false);
            var currentParticipants = await _chatParticipantStore.RetrieveList(command.ChatId, command.ToAdd.Concat(command.ToInvite).Select(r => r.UserId));
            var append = command.ToAdd.Concat(command.ToInvite);
            var users = await _readUserStore.Retrieve(append.Select(r => r.UserId));
            users = users.Join(append, r => r.UserId, r => r.UserId, (u, c) => new {User = u, Candidate = c})
                .Select(r => _readUserStore.Customize(r.User, r.Candidate.Style, r.Candidate.Metadata))
                .ToList();

            async Task<IReadOnlyCollection<TParticipationResult>> SetStatusGroup(IReadOnlyCollection<TParticipationCandidate> candidates, ChatParticipantStatus status)
            {
                IEnumerable<TChatParticipant> chatParticipants = Enumerable.Empty<TChatParticipant>();
                foreach (var group in candidates.GroupBy(r => r.ChatParticipantType))
                {
                    var participants = await _chatParticipantStore.Set(command.ChatId,
                        users.Join(@group, r => r.UserId, r => r.UserId, (u, p) => u),
                        @group.Key, status, command.InitiatorUserId).ConfigureAwait(false);
                    chatParticipants = chatParticipants.Concat(participants);
                }
                return chatParticipants
                    .GroupJoin(currentParticipants, r => r.UserId, r => r.UserId, 
                        (p, cp) => new { p, cp}).SelectMany(r => r.cp.DefaultIfEmpty(), (tmp, cp) => _chatParticipantsEventBuilder.BuildParticipationResult(tmp.p, cp?.ChatParticipantStatus))
                    .ToList();
                
            }

            var added = await SetStatusGroup(command.ToAdd, ChatParticipantStatus.Active);
            var invited = await SetStatusGroup(command.ToInvite, ChatParticipantStatus.Invited);
            var @event = _chatParticipantsEventBuilder.BuildChatParticipantsAppendedEvent(command.InitiatorUserId, command.ChatId, added, invited);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        private async Task<TChatParticipant> SetParticipationCandidate(Guid chatId, Guid userId, ChatParticipantType participantType, ChatParticipantStatus participantStatus, string style, string metadata, Guid setterId)
        {
            var user = await _readUserStore.Retrieve(userId).ConfigureAwait(false);
            user = _readUserStore.Customize(user, style, metadata);
            return await _chatParticipantStore.Set(chatId, user, participantType,
                participantStatus, setterId).ConfigureAwait(false);
        }

        private Task<TChatParticipant> SetParticipationCandidate<TCommand>(TCommand command, ChatParticipantStatus participantStatus)
            where TCommand : IHasInitiator, IChatRelated, IParticipationCandidate
        {
            return SetParticipationCandidate(command.ChatId, command.UserId, command.ChatParticipantType, participantStatus, command.Style, command.Metadata,
                command.InitiatorUserId);
        }

        private async Task<TChatParticipant> SetUser(Guid chatId, Guid userId, ChatParticipantStatus participantStatus, Guid setterId)
        {
            var user = await _readUserStore.Retrieve(userId).ConfigureAwait(false);
            return await _chatParticipantStore.Set(chatId, user, participantStatus, setterId).ConfigureAwait(false);
        }

        private Task<TChatParticipant> SetUser<TCommand>(TCommand command, ChatParticipantStatus participantStatus)
            where TCommand : IHasInitiator, IChatRelated, IUserRelated
        {
            return SetUser(command.ChatId, command.UserId, participantStatus, command.InitiatorUserId);
        }
    }
}
