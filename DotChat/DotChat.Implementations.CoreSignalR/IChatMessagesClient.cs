namespace K1vs.DotChat.Implementations.CoreSignalR
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Events.Messages;
    using Messages;
    using Messages.Typed;
    using Notifications.Messages;
    using Participants;

    public interface IChatMessagesClient<in TChatInfo, in TChatUser, in TChatMessage, in TChatMessageInfo, in TTextMessage, in TQuoteMessage,
        in TMessageAttachmentCollection, in TMessageAttachment, in TChatRefMessageCollection, in TChatRefMessage, in TContactMessageCollection, in TContactMessage>
        where TChatInfo : IChatInfo
        where TChatUser : IChatUser
        where TChatMessage : IChatMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage,
            TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage,
            TContactMessageCollection, TContactMessage>
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage,
            TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage,
            TContactMessageCollection, TContactMessage>
        where TTextMessage : ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage,
            TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage,
            TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection : IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment : IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage : IContactMessage<TChatUser>
    {
        Task ChatMessageAdded(IChatMessageAddedNotification<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage,
                TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection,
                TChatRefMessage, TContactMessageCollection, TContactMessage> notification);

        Task ChatMessageEdited(IChatMessageEditedNotification<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage,
                TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection,
                TChatRefMessage, TContactMessageCollection, TContactMessage> notification);

        Task ChatMessageRemoved(IChatMessageRemovedNotification<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage,
                TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection,
                TChatRefMessage, TContactMessageCollection, TContactMessage> notification);
        Task ChatMessagesRead(IChatMessagesReadNotification notification);
    }
}
