namespace K1vs.DotChat.Implementations.SignalR
{
    using K1vs.DotChat.Notifications;
    using K1vs.DotChat.Notifiers;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chats;
    using Messages;
    using Messages.Typed;
    using Microsoft.AspNet.SignalR.Infrastructure;
    using Notifications.Chats;
    using Notifications.Messages;
    using Notifications.Participants;
    using Participants;

    public class SignalRNotificationSender<TChatsHub, TChatsClient, TChatParticipantsHub, TChatParticipantsClient, TChatMessagesHub, TChatMessagesClient, TPersonalizedChat, TChat,
        TChatInfo, TParticipationResultCollection, TParticipationResult, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, 
        TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> : INotificationSender
        where TChatsHub : Hub<TChatsClient>
        where TChatParticipantsHub : Hub<TChatParticipantsClient>
        where TChatMessagesHub : Hub<TChatMessagesClient>
        where TChatsClient: class, IChatsClient<TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatParticipantsClient: class, IChatParticipantsClient<TParticipationResultCollection, TParticipationResult, TChatParticipant>
        where TChatMessagesClient: class, IChatMessagesClient<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, 
            TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TPersonalizedChat : IPersonalizedChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChat : IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo : IChatInfo
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
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
        private readonly Lazy<IHubContext<TChatsClient>> _lazyChatsHubContext;

        private readonly Lazy<IHubContext<TChatParticipantsClient>> _lazyChatParticipantsHubContext;

        private readonly Lazy<IHubContext<TChatMessagesClient>> _lazyChatMessagesHubContext;

        public SignalRNotificationSender(IConnectionManagerAccessor connectionManagerAccessor)
        {
            var connectionManager = connectionManagerAccessor.ConnectionManager;
            _lazyChatsHubContext = new Lazy<IHubContext<TChatsClient>>(connectionManager.GetHubContext<TChatsHub, TChatsClient>);
            _lazyChatParticipantsHubContext = new Lazy<IHubContext<TChatParticipantsClient>>(connectionManager.GetHubContext<TChatParticipantsHub, TChatParticipantsClient>);
            _lazyChatMessagesHubContext = new Lazy<IHubContext<TChatMessagesClient>>(connectionManager.GetHubContext<TChatMessagesHub, TChatMessagesClient>);
        }

        public virtual bool SupportNotifyChatParticipants => false;

        protected IHubContext<TChatsClient> ChatsHubContext => _lazyChatsHubContext.Value;
        protected IHubContext<TChatParticipantsClient> ChatParticipantsHubContext => _lazyChatParticipantsHubContext.Value;
        protected IHubContext<TChatMessagesClient> ChatMessagesHubContext => _lazyChatMessagesHubContext.Value;

        public virtual Task NotifyChatParticipants<TNotificationBase>(TNotificationBase notification, Guid chatId) where TNotificationBase : INotification
        {
            throw new NotImplementedException();
        }

        public virtual async Task NotifyUsers<TNotificationBase>(TNotificationBase notification, IEnumerable<Guid> userIds) where TNotificationBase : INotification
        {
            var stringUserId = userIds.Select(r => r.ToString()).ToList();

            await SendNotifications(notification, stringUserId);
        }

        private async Task SendNotifications<TNotificationBase>(TNotificationBase notification, List<string> stringUserId)
            where TNotificationBase : INotification
        {
            if (notification is IChatsNotification)
            {
                var chatsClient = ChatsHubContext.Clients.Users(stringUserId);
                await SendChatsNotifications(notification, chatsClient);
            }

            if (notification is IChatParticipantsNotification)
            {
                var chatParticipantsClient = ChatParticipantsHubContext.Clients.Users(stringUserId);
                await SendChatParticipantsNotifications(notification, chatParticipantsClient);
            }

            if (notification is IChatMessagesNotification)
            {
                var chatMessagesClient = ChatMessagesHubContext.Clients.Users(stringUserId);
                await SendChatMessagesNotifications(notification, chatMessagesClient);
            }
        }

        protected virtual async Task SendChatMessagesNotifications<TNotificationBase>(TNotificationBase notification,
            TChatMessagesClient chatMessagesClient) where TNotificationBase : INotification
        {
            if (notification is IChatMessageAddedNotification<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo,
                TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection,
                TChatRefMessage, TContactMessageCollection, TContactMessage> chatMessageAddedNotification)
            {
                await chatMessagesClient.ChatMessageAdded(chatMessageAddedNotification);
            }

            if (notification is IChatMessageEditedNotification<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo,
                TTextMessage,
                TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection,
                TChatRefMessage, TContactMessageCollection, TContactMessage> chatMessageEditedNotification)
            {
                await chatMessagesClient.ChatMessageEdited(chatMessageEditedNotification);
            }

            if (notification is IChatMessageRemovedNotification<TChatInfo, TChatUser, TChatMessage, TChatMessageInfo,
                TTextMessage,
                TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection,
                TChatRefMessage, TContactMessageCollection, TContactMessage> chatMessageRemovedNotification)
            {
                await chatMessagesClient.ChatMessageRemoved(chatMessageRemovedNotification);
            }

            if (notification is IChatMessagesReadNotification chatMessagesReadNotification)
            {
                await chatMessagesClient.ChatMessagesRead(chatMessagesReadNotification);
            }
        }

        protected virtual async Task SendChatParticipantsNotifications<TNotificationBase>(TNotificationBase notification,
            TChatParticipantsClient chatParticipantsClient) where TNotificationBase : INotification
        {
            if (notification is IChatParticipantAddedNotification<TChatParticipant> chatParticipantAddedNotification)
            {
                await chatParticipantsClient.ChatParticipantAdded(chatParticipantAddedNotification);
            }

            if (notification is IChatParticipantInvitedNotification<TChatParticipant> chatParticipantInvitedNotification)
            {
                await chatParticipantsClient.ChatParticipantInvited(chatParticipantInvitedNotification);
            }

            if (notification is IChatParticipantAppliedNotification<TChatParticipant> chatParticipantAppliedNotification)
            {
                await chatParticipantsClient.ChatParticipantApplied(chatParticipantAppliedNotification);
            }

            if (notification is IChatParticipantRemovedNotification<TChatParticipant> chatParticipantRemovedNotification)
            {
                await chatParticipantsClient.ChatParticipantRemoved(chatParticipantRemovedNotification);
            }

            if (notification is IChatParticipantBlockedNotification<TChatParticipant> chatParticipantBlockedNotification)
            {
                await chatParticipantsClient.ChatParticipantBlocked(chatParticipantBlockedNotification);
            }

            if (notification is
                IChatParticipantsAppendedNotification<TParticipationResultCollection, TParticipationResult, TChatParticipant>
                chatParticipantsAppendedNotification)
            {
                await chatParticipantsClient.ChatParticipantsAppended(chatParticipantsAppendedNotification);
            }

            if (notification is IChatParticipantTypeChangedNotification<TChatParticipant> chatParticipantTypeChangedNotification
            )
            {
                await chatParticipantsClient.ChatParticipantTypeChanged(chatParticipantTypeChangedNotification);
            }
        }

        protected virtual async Task SendChatsNotifications<TNotificationBase>(TNotificationBase notification,
            TChatsClient chatsClient) where TNotificationBase : INotification
        {
            if (notification is IChatAddedNotification<TPersonalizedChat, TChat, TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
                chatAddedNotification)
            {
                await chatsClient.ChatAdded(chatAddedNotification);
            }

            if (notification is IChatInfoEditedNotification<TChatInfo> chatInfoEditedNotification)
            {
                await chatsClient.ChatInfoEdited(chatInfoEditedNotification);
            }

            if (notification is IChatRemovedNotification<TChatInfo> chatRemovedNotification)
            {
                await chatsClient.ChatRemoved(chatRemovedNotification);
            }
        }
    }
}
