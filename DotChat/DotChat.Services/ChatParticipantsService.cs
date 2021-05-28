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

    public class ChatParticipantsService<TDotChatConfiguration, TParticipationCandidateCollection, TParticipationCandidate> : ServiceBase, IChatParticipantsService
    {
        protected readonly IChatParticipantsPermissionValidator ChatParticipantsPermissionValidator;
        protected readonly IChatParticipantsCommandBuilder ChatParticipantsCommandBuilder;
        protected readonly IChatCommandSender ChatCommandSender;

        public ChatParticipantsService(IChatServicesConfiguration chatServicesConfiguration, IChatParticipantsPermissionValidator chatParticipantsPermissionValidator, IChatParticipantsCommandBuilder chatParticipantsCommandBuilder, IChatCommandSender chatCommandSender) : base(chatServicesConfiguration)
        {
            ChatParticipantsPermissionValidator = chatParticipantsPermissionValidator;
            ChatParticipantsCommandBuilder = chatParticipantsCommandBuilder;
            ChatCommandSender = chatCommandSender;
        }

        public virtual async Task Add(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null, string metadata = null)
        {
            await ChatParticipantsPermissionValidator.ValidateAdd(currentUserId, chatId, userId, chatParticipantType, style, metadata, ServiceName)
                .ConfigureAwait(false);
            var command = ChatParticipantsCommandBuilder.BuildAddChatParticipantCommand(currentUserId, chatId, userId, chatParticipantType, style, metadata);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
        }

        public virtual async Task Invite(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null, string metadata = null)
        {
            await ChatParticipantsPermissionValidator.ValidateInvite(currentUserId, chatId, userId, chatParticipantType, style, metadata, ServiceName)
                .ConfigureAwait(false);
            var command = ChatParticipantsCommandBuilder.BuildInviteChatParticipantCommand(currentUserId, chatId, userId, chatParticipantType, style, metadata);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
        }

        public virtual async Task Apply(Guid currentUserId, Guid chatId, ChatParticipantType chatParticipantType, string style = null, string metadata = null)
        {
            await ChatParticipantsPermissionValidator.ValidateApply(currentUserId, chatId, chatParticipantType, style, metadata, ServiceName)
                .ConfigureAwait(false);
            var command = ChatParticipantsCommandBuilder.BuildApplyToChatCommand(currentUserId, chatId, chatParticipantType, style, metadata);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
        }

        public virtual async Task Remove(Guid currentUserId, Guid chatId, Guid userId)
        {
            await ChatParticipantsPermissionValidator.ValidateRemove(currentUserId, chatId, userId, ServiceName)
                .ConfigureAwait(false);
            var command = ChatParticipantsCommandBuilder.BuildRemoveChatParticipantCommand(currentUserId, chatId, userId);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
        }

        public virtual async Task Block(Guid currentUserId, Guid chatId, Guid userId)
        {
            await ChatParticipantsPermissionValidator.ValidateBlock(currentUserId, chatId, userId, ServiceName)
                .ConfigureAwait(false);
            var command = ChatParticipantsCommandBuilder.BuildBlockChatParticipantCommand(currentUserId, chatId, userId);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
        }

        public virtual async Task ChangeType(Guid currentUserId, Guid chatId, Guid userId, ChatParticipantType chatParticipantType, string style = null, string metadata = null)
        {
            await ChatParticipantsPermissionValidator.ValidateChangeType(currentUserId, chatId, userId, chatParticipantType, style, metadata, ServiceName)
                .ConfigureAwait(false);
            var command = ChatParticipantsCommandBuilder.BuildChangeChatParticipantTypeCommand(currentUserId, chatId, userId, chatParticipantType, style, metadata);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
        }

        public virtual async Task Append(Guid currentUserId, Guid chatId, IReadOnlyCollection<IParticipationCandidate> addCandidates, IReadOnlyCollection<IParticipationCandidate> inviteCandidates)
        {
            await ChatParticipantsPermissionValidator.ValidateAppend(currentUserId, chatId, addCandidates, inviteCandidates, ServiceName)
                .ConfigureAwait(false);
            var command = ChatParticipantsCommandBuilder.BuildBulkAppendChatParticipantsCommand(currentUserId, chatId, addCandidates, inviteCandidates);
            await ChatCommandSender.Send(command).ConfigureAwait(false);
        }
    }
}
