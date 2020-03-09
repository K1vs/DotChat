namespace K1vs.DotChat.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bus;
    using CommandBuilders;
    using Configuration;
    using Participants;
    using Security;

    public class ChatParticipantsService<TDotChatConfiguration, TParticipationCandidateCollection, TParticipationCandidate> : ServiceBase<TDotChatConfiguration>, IChatParticipantsService<TParticipationCandidateCollection, TParticipationCandidate>
        where TDotChatConfiguration : IChatServicesConfiguration
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
    {
        private readonly IChatParticipantsPermissionValidator<TParticipationCandidateCollection, TParticipationCandidate> _chatParticipantsPermissionValidator;
        private readonly IChatParticipantsCommandBuilder<TParticipationCandidateCollection, TParticipationCandidate> _chatParticipantsCommandBuilder;
        private readonly IChatCommandSender _chatCommandSender;

        public ChatParticipantsService(TDotChatConfiguration chatServicesConfiguration, IChatParticipantsPermissionValidator<TParticipationCandidateCollection, TParticipationCandidate> chatParticipantsPermissionValidator, IChatParticipantsCommandBuilder<TParticipationCandidateCollection, TParticipationCandidate> chatParticipantsCommandBuilder, IChatCommandSender chatCommandSender) : base(chatServicesConfiguration)
        {
            _chatParticipantsPermissionValidator = chatParticipantsPermissionValidator;
            _chatParticipantsCommandBuilder = chatParticipantsCommandBuilder;
            _chatCommandSender = chatCommandSender;
        }

        public async Task Add(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null, string metadata = null)
        {
            await _chatParticipantsPermissionValidator.ValidateAdd(currentUserId, chatId, userId, chatParticipantType, style, metadata, ServiceName)
                .ConfigureAwait(false);
            var command = _chatParticipantsCommandBuilder.BuildAddChatParticipantCommand(currentUserId, chatId, userId, chatParticipantType, style, metadata);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
        }

        public async Task Invite(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null, string metadata = null)
        {
            await _chatParticipantsPermissionValidator.ValidateInvite(currentUserId, chatId, userId, chatParticipantType, style, metadata, ServiceName)
                .ConfigureAwait(false);
            var command = _chatParticipantsCommandBuilder.BuildInviteChatParticipantCommand(currentUserId, chatId, userId, chatParticipantType, style, metadata);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
        }

        public async Task Apply(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType, string style = null, string metadata = null)
        {
            await _chatParticipantsPermissionValidator.ValidateApply(currentUserId, chatId, chatParticipantType, style, metadata, ServiceName)
                .ConfigureAwait(false);
            var command = _chatParticipantsCommandBuilder.BuildApplyToChatCommand(currentUserId, chatId, chatParticipantType, style, metadata);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
        }

        public async Task Remove(Guid currentUserId, Guid chatId, Guid userId)
        {
            await _chatParticipantsPermissionValidator.ValidateRemove(currentUserId, chatId, userId, ServiceName)
                .ConfigureAwait(false);
            var command = _chatParticipantsCommandBuilder.BuildRemoveChatParticipantCommand(currentUserId, chatId, userId);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
        }

        public async Task Block(Guid currentUserId, Guid chatId, Guid userId)
        {
            await _chatParticipantsPermissionValidator.ValidateBlock(currentUserId, chatId, userId, ServiceName)
                .ConfigureAwait(false);
            var command = _chatParticipantsCommandBuilder.BuildBlockChatParticipantCommand(currentUserId, chatId, userId);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
        }

        public async Task ChangeType(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null, string metadata = null)
        {
            await _chatParticipantsPermissionValidator.ValidateChangeType(currentUserId, chatId, userId, chatParticipantType, style, metadata, ServiceName)
                .ConfigureAwait(false);
            var command = _chatParticipantsCommandBuilder.BuildChangeChatParticipantTypeCommand(currentUserId, chatId, userId, chatParticipantType, style, metadata);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
        }

        public async Task BulkAppendChatParticipants(Guid currentUserId, Guid chatId, TParticipationCandidateCollection addCandidates,
            TParticipationCandidateCollection inviteCandidates)
        {
            await _chatParticipantsPermissionValidator.ValidateBulkAppend(currentUserId, chatId, addCandidates, inviteCandidates, ServiceName)
                .ConfigureAwait(false);
            var command = _chatParticipantsCommandBuilder.BuildAppendChatParticipantCommand(currentUserId, chatId, addCandidates, inviteCandidates);
            await _chatCommandSender.Send(command).ConfigureAwait(false);
        }
    }
}
