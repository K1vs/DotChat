namespace K1vs.DotChat.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Events.Chats;
    using Events.Participants;
    using Handlers;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using Participants;

    public interface IChatSystemMessagesWorker<in TChat, in TChatInfo, in TParticipationResultCollection, in TParticipationResult, in TChatParticipantCollection, in TChatParticipant, in TChatUser, in TChatMessageInfo, in TTextMessage, in TQuoteMessage, in TMessageAttachmentCollection, in TMessageAttachment, in TChatRefMessageCollection, in TChatRefMessage, in TContactMessageCollection, in TContactMessage> :
        IHandleEvent<IChatParticipantAddedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantInvitedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantAppliedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantRemovedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantBlockedEvent<TChatParticipant>>,
        IHandleEvent<IChatParticipantsAppendedEvent<TParticipationResultCollection, TParticipationResult, TChatParticipant>>,
        IHandleEvent<IChatAddedEvent<TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>>,
        IHandleEvent<IChatInfoEditedEvent<TChatInfo>>
        where TChat : IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo : IChatInfo
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TChatUser : IChatUser
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TTextMessage : ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection : IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment : IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage : IContactMessage<TChatUser>
    {
    }
}
