namespace K1vs.DotChat.Basic.SystemMessages
{
    using System.Collections.Generic;
    using System.Linq;
    using Chats;
    using DotChat.Chats;
    using DotChat.Events.Chat;
    using DotChat.Events.Chats;
    using DotChat.Events.Participants;
    using DotChat.Messages;
    using DotChat.Messages.Typed;
    using DotChat.Participants;
    using DotChat.SystemMessages;
    using Messages;
    using Messages.Typed;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Participants;

    public class SystemMessagesBuilder: ISystemMessagesBuilder<Chat, ChatInfo, List<ParticipationResult>, ParticipationResult, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage>
    {
        public const string SystemMessageStyle = "System";

        public virtual string MessageStyle(string name)
        {
            return $"{SystemMessageStyle}.{name}";
        }

        public virtual IReadOnlyCollection<ChatMessageInfo> BuildChatAddedMessage(IChatAddedEvent<Chat, List<ChatParticipant>, ChatParticipant> @event)
        {
            var chatMessage = new ChatMessageInfo(MessageType.ChatRef, immutable:true, style: MessageStyle("ChatAdded"), chatRefs: new List<ChatRefMessage>() { new ChatRefMessage(@event.Chat.ChatId, @event.Chat)});
            var result = new List<ChatMessageInfo>{ chatMessage };

            var added = @event.Chat.Participants.Where(r => r.ChatParticipantStatus == ChatParticipantStatus.Active).ToList();
            if (added.Any())
            {
                var addedContactMessages = added.Select(r => new ContactMessage(r)).ToList();
                var addedMessage = new ChatMessageInfo(MessageType.Contact, immutable: true, style: MessageStyle("AddedInitialChatParticipants"), contacts: addedContactMessages);
                result.Add(addedMessage);
            }
            var invited = @event.Chat.Participants.Where(r => r.ChatParticipantStatus == ChatParticipantStatus.Active).ToList();
            if (invited.Any())
            {
                var invitedContactMessages = invited.Select(r => new ContactMessage(r)).ToList();
                var invitedMessage = new ChatMessageInfo(MessageType.Contact, immutable:true, style: MessageStyle("InvitedInitialChatParticipants"), contacts: invitedContactMessages);
                result.Add(invitedMessage);
            }

            return result;
        }

        public virtual ChatMessageInfo BuildChatInfoEditedMessage(IChatInfoEditedEvent<ChatInfo> @event)
        {
            return new ChatMessageInfo(MessageType.ChatRef, immutable: true, style: MessageStyle("ChatInfoEdited"), chatRefs : new List<ChatRefMessage>() { new ChatRefMessage(@event.ChatId, @event.ChatInfo) });
        }

        public virtual IReadOnlyCollection<ChatMessageInfo> BuildBulkParticipantsAppendedMessages(IChatParticipantsAppendedEvent<List<ParticipationResult>, ParticipationResult, ChatParticipant> @event)
        {
            var result = new List<ChatMessageInfo>();

            if (@event.Added.Any())
            {
                var addedContactMessages = @event.Added.Select(r => new ContactMessage(r.Participant)).ToList();
                var addedMessage = new ChatMessageInfo(MessageType.Contact, immutable: true, style: MessageStyle("ChatBulkAdded"), contacts: addedContactMessages);
                result.Add(addedMessage);
            }

            if (@event.Invited.Any())
            {
                var invitedContactMessages = @event.Invited.Select(r => new ContactMessage(r.Participant)).ToList();
                var invitedMessage = new ChatMessageInfo(MessageType.Contact, immutable: true, style: MessageStyle("ChatBulkInvited"), contacts: invitedContactMessages);
                result.Add(invitedMessage);
            }

            return result;
        }

        public virtual ChatMessageInfo BuildChatParticipantAddedMessage(IChatParticipantAddedEvent<ChatParticipant> @event)
        {
            return new ChatMessageInfo(MessageType.ChatRef, style: MessageStyle("ParticipantAdded"), contacts: new List<ContactMessage>() { new ContactMessage(@event.Participant) });
        }

        public virtual ChatMessageInfo BuildChatParticipantAppliedMessage(IChatParticipantAppliedEvent<ChatParticipant> @event)
        {
            return new ChatMessageInfo(MessageType.ChatRef, style: MessageStyle("ParticipantApplied"), contacts: new List<ContactMessage>() { new ContactMessage(@event.Participant) });
        }

        public virtual ChatMessageInfo BuildChatParticipantBlockedMessage(IChatParticipantBlockedEvent<ChatParticipant> @event)
        {
            return new ChatMessageInfo(MessageType.ChatRef, style: MessageStyle("ParticipantBlocked"), contacts: new List<ContactMessage>() { new ContactMessage(@event.Participant) });
        }

        public virtual ChatMessageInfo BuildChatParticipantInvitedMessage(IChatParticipantInvitedEvent<ChatParticipant> @event)
        {
            return new ChatMessageInfo(MessageType.ChatRef, style: MessageStyle("ParticipantInvited"), contacts: new List<ContactMessage>() { new ContactMessage(@event.Participant) });
        }

        public virtual ChatMessageInfo BuildChatParticipantRemovedMessage(IChatParticipantRemovedEvent<ChatParticipant> @event)
        {
            return new ChatMessageInfo(MessageType.ChatRef, style: MessageStyle("ParticipantInvited"), contacts: new List<ContactMessage>() { new ContactMessage(@event.Participant) });
        }

        public virtual ChatMessageInfo BuildParticipantTypeChangedMessage(IChatParticipantTypeChangedEvent<ChatParticipant> @event)
        {
            return new ChatMessageInfo(MessageType.ChatRef, style: MessageStyle("ParticipantTypeChanged"), contacts: new List<ContactMessage>() { new ContactMessage(@event.Participant) });
        }
    }
}
