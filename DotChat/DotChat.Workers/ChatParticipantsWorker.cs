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
        protected readonly IChatParticipantsPermissionValidator<TParticipationCandidateCollection, TParticipationCandidate> ChatParticipantsPermissionValidator;

        protected readonly IChatParticipantStore<TChatParticipant, TChatUser> ChatParticipantStore;

        protected readonly IReadUserStore<TChatUser> ReadUserStore;

        protected readonly IChatParticipantsEventBuilder<TParticipationResultCollection, TParticipationResult, TChatParticipant> ChatParticipantsEventBuilder;

        protected ChatParticipantsWorker(TChatWorkersConfiguration chatWorkersConfiguration, IChatParticipantsPermissionValidator<TParticipationCandidateCollection, TParticipationCandidate> chatParticipantsPermissionValidator, IChatParticipantStore<TChatParticipant, TChatUser> chatParticipantStore, IReadUserStore<TChatUser> readUserStore, IChatParticipantsEventBuilder<TParticipationResultCollection, TParticipationResult, TChatParticipant> chatParticipantsEventBuilder) : base(chatWorkersConfiguration)
        {
            ChatParticipantsPermissionValidator = chatParticipantsPermissionValidator;
            ChatParticipantStore = chatParticipantStore;
            ReadUserStore = readUserStore;
            ChatParticipantsEventBuilder = chatParticipantsEventBuilder;
        }

        public virtual async Task Handle(IAddChatParticipantCommand command, IChatBusContext chatEventPublisher)
        {
            await ChatParticipantsPermissionValidator.ValidateAdd(command.InitiatorUserId, command.ChatId, command.UserId, command.ChatParticipantType, command.Style, command.Metadata, WorkerName).ConfigureAwait(false);
            var currentParticipant = await ChatParticipantStore.Retrieve(command.ChatId, command.UserId).ConfigureAwait(false);
            var participant = await SetParticipationCandidate(command, ChatParticipantStatus.Active).ConfigureAwait(false);
            var @event = ChatParticipantsEventBuilder.BuildChatParticipantAddedEvent(command.InitiatorUserId, command.ChatId, participant, currentParticipant?.ChatParticipantStatus);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public virtual async Task Handle(IApplyToChatCommand command, IChatBusContext chatEventPublisher)
        {
            await ChatParticipantsPermissionValidator.ValidateApply(command.InitiatorUserId, command.ChatId, command.ChatParticipantType, command.Style, command.Metadata, WorkerName).ConfigureAwait(false);
            var currentParticipant = await ChatParticipantStore.Retrieve(command.ChatId, command.InitiatorUserId).ConfigureAwait(false);
            var participant = await SetParticipationCandidate(command.ChatId, command.InitiatorUserId, command.ChatParticipantType, ChatParticipantStatus.Applied, command.Style, command.Metadata, command.InitiatorUserId).ConfigureAwait(false);
            var @event = ChatParticipantsEventBuilder.BuildChatParticipantAppliedEvent(command.InitiatorUserId, command.ChatId, participant, currentParticipant?.ChatParticipantStatus);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public virtual async Task Handle(IInviteChatParticipantCommand command, IChatBusContext chatEventPublisher)
        {
            await ChatParticipantsPermissionValidator.ValidateInvite(command.InitiatorUserId, command.ChatId, command.UserId, command.ChatParticipantType, command.Style, command.Metadata, WorkerName).ConfigureAwait(false);
            var currentParticipant = await ChatParticipantStore.Retrieve(command.ChatId, command.UserId).ConfigureAwait(false);
            var participant = await SetParticipationCandidate(command, ChatParticipantStatus.Invited).ConfigureAwait(false);
            var @event = ChatParticipantsEventBuilder.BuildChatParticipantInvitedEvent(command.InitiatorUserId, command.ChatId, participant, currentParticipant?.ChatParticipantStatus);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public virtual async Task Handle(IRemoveChatParticipantCommand command, IChatBusContext chatEventPublisher)
        {
            await ChatParticipantsPermissionValidator.ValidateRemove(command.InitiatorUserId, command.ChatId, command.UserId, WorkerName).ConfigureAwait(false);
            var currentParticipant = await ChatParticipantStore.Retrieve(command.ChatId, command.UserId).ConfigureAwait(false);
            var participant = await SetUser(command, ChatParticipantStatus.Removed).ConfigureAwait(false);
            var @event = ChatParticipantsEventBuilder.BuildChatParticipantRemovedEvent(command.InitiatorUserId, command.ChatId, participant, currentParticipant?.ChatParticipantStatus);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public virtual async Task Handle(IBlockChatParticipantCommand command, IChatBusContext chatEventPublisher)
        {
            await ChatParticipantsPermissionValidator.ValidateRemove(command.InitiatorUserId, command.ChatId, command.UserId, WorkerName).ConfigureAwait(false);
            var currentParticipant = await ChatParticipantStore.Retrieve(command.ChatId, command.UserId).ConfigureAwait(false);
            var participant = await SetUser(command, ChatParticipantStatus.Blocked).ConfigureAwait(false);
            var @event = ChatParticipantsEventBuilder.BuildChatParticipantBlockedEvent(command.InitiatorUserId, command.ChatId, participant, currentParticipant?.ChatParticipantStatus);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public virtual async Task Handle(IChangeChatParticipantTypeCommand command, IChatBusContext chatEventPublisher)
        {
            await ChatParticipantsPermissionValidator.ValidateChangeType(command.InitiatorUserId, command.ChatId, command.UserId, command.ChatParticipantType, command.Style, command.Metadata, WorkerName).ConfigureAwait(false);
            var currentParticipant = await ChatParticipantStore.Retrieve(command.ChatId, command.UserId).ConfigureAwait(false);
            var participant = await ChatParticipantStore.ChangeType(command.ChatId, command.UserId, command.ChatParticipantType, command.InitiatorUserId).ConfigureAwait(false);
            var @event = ChatParticipantsEventBuilder.BuildChatParticipantTypeChangedEvent(command.InitiatorUserId, command.ChatId, participant, currentParticipant?.ChatParticipantStatus);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        public virtual async Task Handle(IAppendChatParticipantsCommand<TParticipationCandidateCollection, TParticipationCandidate> command, IChatBusContext chatEventPublisher)
        {
            await ChatParticipantsPermissionValidator.ValidateAppend(command.InitiatorUserId, command.ChatId, command.ToAdd, command.ToInvite, WorkerName).ConfigureAwait(false);
            var currentParticipants = await ChatParticipantStore.RetrieveList(command.ChatId, command.ToAdd.Concat(command.ToInvite).Select(r => r.UserId));
            var append = command.ToAdd.Concat(command.ToInvite);
            var users = await ReadUserStore.Retrieve(append.Select(r => r.UserId));
            users = users.Join(append, r => r.UserId, r => r.UserId, (u, c) => new {User = u, Candidate = c})
                .Select(r => ReadUserStore.Customize(r.User, r.Candidate.Style, r.Candidate.Metadata))
                .ToList();

            async Task<IReadOnlyCollection<TParticipationResult>> SetStatusGroup(IReadOnlyCollection<TParticipationCandidate> candidates, ChatParticipantStatus status)
            {
                IEnumerable<TChatParticipant> chatParticipants = Enumerable.Empty<TChatParticipant>();
                foreach (var group in candidates.GroupBy(r => r.ChatParticipantType))
                {
                    var participants = await ChatParticipantStore.Set(command.ChatId,
                        users.Join(@group, r => r.UserId, r => r.UserId, (u, p) => u),
                        @group.Key, status, command.InitiatorUserId).ConfigureAwait(false);
                    chatParticipants = chatParticipants.Concat(participants);
                }
                return chatParticipants
                    .GroupJoin(currentParticipants, r => r.UserId, r => r.UserId, 
                        (p, cp) => new { p, cp}).SelectMany(r => r.cp.DefaultIfEmpty(), (tmp, cp) => ChatParticipantsEventBuilder.BuildParticipationResult(tmp.p, cp?.ChatParticipantStatus))
                    .ToList();
                
            }

            var added = await SetStatusGroup(command.ToAdd, ChatParticipantStatus.Active);
            var invited = await SetStatusGroup(command.ToInvite, ChatParticipantStatus.Invited);
            var @event = ChatParticipantsEventBuilder.BuildChatParticipantsAppendedEvent(command.InitiatorUserId, command.ChatId, added, invited);
            await chatEventPublisher.EventPublisher.Publish(@event).ConfigureAwait(false);
        }

        protected virtual async Task<TChatParticipant> SetParticipationCandidate(Guid chatId, Guid userId, ChatParticipantType participantType, ChatParticipantStatus participantStatus, string style, string metadata, Guid setterId)
        {
            var user = await ReadUserStore.Retrieve(userId).ConfigureAwait(false);
            user = ReadUserStore.Customize(user, style, metadata);
            return await ChatParticipantStore.Set(chatId, user, participantType,
                participantStatus, setterId).ConfigureAwait(false);
        }

        protected virtual Task<TChatParticipant> SetParticipationCandidate<TCommand>(TCommand command, ChatParticipantStatus participantStatus)
            where TCommand : IHasInitiator, IChatRelated, IParticipationCandidate
        {
            return SetParticipationCandidate(command.ChatId, command.UserId, command.ChatParticipantType, participantStatus, command.Style, command.Metadata,
                command.InitiatorUserId);
        }

        protected virtual async Task<TChatParticipant> SetUser(Guid chatId, Guid userId, ChatParticipantStatus participantStatus, Guid setterId)
        {
            var user = await ReadUserStore.Retrieve(userId).ConfigureAwait(false);
            return await ChatParticipantStore.Set(chatId, user, participantStatus, setterId).ConfigureAwait(false);
        }

        protected virtual Task<TChatParticipant> SetUser<TCommand>(TCommand command, ChatParticipantStatus participantStatus)
            where TCommand : IHasInitiator, IChatRelated, IUserRelated
        {
            return SetUser(command.ChatId, command.UserId, participantStatus, command.InitiatorUserId);
        }
    }
}
