//namespace K1vs.DotChat.Basic.SystemMessages
//{
//    using System.Collections.Generic;
//    using System.Linq;
//    using Chats;
//    using DotChat.Chats;
//    using DotChat.Events.Chat;
//    using DotChat.Events.Chats;
//    using DotChat.Events.Participants;
//    using DotChat.Messages;
//    using DotChat.Messages.Typed;
//    using DotChat.Participants;
//    using DotChat.SystemMessages;
//    using K1vs.DotChat.Models.Messages;
//    using Messages;
//    using Messages.Typed;
//    using Models.Chats;
//    using Models.Messages.Typed;
//    using Models.Participants;
//    using Participants;

//    public class SystemMessagesBuilder: ISystemMessagesBuilder
//    {
//        public const string SystemMessageStyle = "System";

//        public virtual string MessageStyle(string name)
//        {
//            return $"{SystemMessageStyle}.{name}";
//        }

//        public virtual IReadOnlyCollection<IChatMessageInfo> BuildChatAddedMessage(IChatAddedEvent @event)
//        {
//            var chatMessage = new ChatMessageInfo(MessageType.ChatRef, 0, immutable:true, style: MessageStyle("ChatAdded"), chatRefs: new List<ChatRefMessage>() { new ChatRefMessage(@event.Chat.ChatId, @event.Chat)});
//            var result = new List<ChatMessageInfo>{ chatMessage };

//            var added = @event.Chat.Participants.Where(r => r.ChatParticipantStatus == ChatParticipantStatus.Active).ToList();
//            if (added.Any())
//            {
//                var addedContactMessages = added.Select(r => new ContactMessage(r)).ToList();
//                var addedMessage = new ChatMessageInfo(MessageType.Contact, 0, immutable: true, style: MessageStyle("AddedInitialChatParticipants"), contacts: addedContactMessages);
//                result.Add(addedMessage);
//            }
//            var invited = @event.Chat.Participants.Where(r => r.ChatParticipantStatus == ChatParticipantStatus.Invited).ToList();
//            if (invited.Any())
//            {
//                var invitedContactMessages = invited.Select(r => new ContactMessage(r)).ToList();
//                var invitedMessage = new ChatMessageInfo(MessageType.Contact, 0, immutable:true, style: MessageStyle("InvitedInitialChatParticipants"), contacts: invitedContactMessages);
//                result.Add(invitedMessage);
//            }

//            return result;
//        }

//        public virtual IChatMessageInfo BuildChatInfoEditedMessage(IChatInfoEditedEvent @event)
//        {
//            return new ChatMessageInfo(MessageType.ChatRef, 0, immutable: true, style: MessageStyle("ChatInfoEdited"), chatRefs : new List<ChatRefMessage>() { new ChatRefMessage(@event.ChatId, @event.ChatInfo) });
//        }

//        public virtual IReadOnlyCollection<IChatMessageInfo> BuildBulkParticipantsAppendedMessages(IChatParticipantBulkAddedInvitedEvent @event)
//        {
//            var result = new List<ChatMessageInfo>();

//            if (@event.Added.Any())
//            {
//                var addedContactMessages = @event.Added.Select(r => new ContactMessage(r.Participant)).ToList();
//                var addedMessage = new ChatMessageInfo(MessageType.Contact, 0, immutable: true, style: MessageStyle("ChatBulkAdded"), contacts: addedContactMessages);
//                result.Add(addedMessage);
//            }

//            if (@event.Invited.Any())
//            {
//                var invitedContactMessages = @event.Invited.Select(r => new ContactMessage(r.Participant)).ToList();
//                var invitedMessage = new ChatMessageInfo(MessageType.Contact, 0, immutable: true, style: MessageStyle("ChatBulkInvited"), contacts: invitedContactMessages);
//                result.Add(invitedMessage);
//            }

//            return result;
//        }

//        public virtual IChatMessageInfo BuildChatParticipantAddedMessage(IChatParticipantAddedEvent @event)
//        {
//            return new ChatMessageInfo(MessageType.ChatRef, 0, style: MessageStyle("ParticipantAdded"), contacts: new List<ContactMessage>() { new ContactMessage(@event.Participant) });
//        }

//        public virtual IChatMessageInfo BuildChatParticipantAppliedMessage(IChatParticipantAppliedEvent @event)
//        {
//            return new ChatMessageInfo(MessageType.ChatRef, 0, style: MessageStyle("ParticipantApplied"), contacts: new List<ContactMessage>() { new ContactMessage(@event.Participant) });
//        }

//        public virtual IChatMessageInfo BuildChatParticipantBlockedMessage(IChatParticipantBlockedEvent @event)
//        {
//            return new ChatMessageInfo(MessageType.ChatRef, 0, style: MessageStyle("ParticipantBlocked"), contacts: new List<ContactMessage>() { new ContactMessage(@event.Participant) });
//        }

//        public virtual IChatMessageInfo BuildChatParticipantInvitedMessage(IChatParticipantInvitedEvent @event)
//        {
//            return new ChatMessageInfo(MessageType.ChatRef, 0, style: MessageStyle("ParticipantInvited"), contacts: new List<ContactMessage>() { new ContactMessage(@event.Participant) });
//        }

//        public virtual IChatMessageInfo BuildChatParticipantRemovedMessage(IChatParticipantRemovedEvent @event)
//        {
//            return new ChatMessageInfo(MessageType.ChatRef, 0, style: MessageStyle("ParticipantInvited"), contacts: new List<ContactMessage>() { new ContactMessage(@event.Participant) });
//        }

//        public virtual IChatMessageInfo BuildParticipantTypeChangedMessage(IChatParticipantTypeChangedEvent @event)
//        {
//            return new ChatMessageInfo(MessageType.ChatRef, 0, style: MessageStyle("ParticipantTypeChanged"), contacts: new List<ContactMessage>() { new ContactMessage(@event.Participant) });
//        }
//    }
//}
