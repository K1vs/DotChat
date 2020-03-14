namespace K1vs.DotChat.SystemMessages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Events.Chats;
    using Events.Participants;
    using Messages;
    using Messages.Typed;
    using Participants;

    public interface ISystemMessagesBuilder<in TChat, in TChatInfo, in TParticipationResultCollection, in TParticipationResult, in TChatParticipantCollection, in TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChat: IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo: IChatInfo
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TChatUser: IChatUser
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TTextMessage: ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection: IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment: IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage: IContactMessage<TChatUser>
    {
        IReadOnlyCollection<TChatMessageInfo> BuildChatAddedMessage(IChatAddedEvent<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> @event);
        TChatMessageInfo BuildChatInfoEditedMessage(IChatInfoEditedEvent<TChatInfo> @event);
        IReadOnlyCollection<TChatMessageInfo> BuildBulkParticipantsAppendedMessages(IChatParticipantsAppendedEvent<TParticipationResultCollection, TParticipationResult, TChatParticipant> @event);
        TChatMessageInfo BuildChatParticipantAddedMessage(IChatParticipantAddedEvent<TChatParticipant> @event);
        TChatMessageInfo BuildChatParticipantAppliedMessage(IChatParticipantAppliedEvent<TChatParticipant> @event);
        TChatMessageInfo BuildChatParticipantBlockedMessage(IChatParticipantBlockedEvent<TChatParticipant> @event);
        TChatMessageInfo BuildChatParticipantInvitedMessage(IChatParticipantInvitedEvent<TChatParticipant> @event);
        TChatMessageInfo BuildChatParticipantRemovedMessage(IChatParticipantRemovedEvent<TChatParticipant> @event);
        TChatMessageInfo BuildParticipantTypeChangedMessage(IChatParticipantTypeChangedEvent<TChatParticipant> @event);
    }
}
