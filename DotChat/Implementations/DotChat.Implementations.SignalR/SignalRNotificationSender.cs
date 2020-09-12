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

    public class SignalRNotificationSender<TChatsHub, TChatsClient, TChatParticipantsHub, TChatParticipantsClient, TChatMessagesHub, TChatMessagesClient> : INotificationSender
        where TChatsHub : Hub<TChatsClient>
        where TChatParticipantsHub : Hub<TChatParticipantsClient>
        where TChatMessagesHub : Hub<TChatMessagesClient>
        where TChatsClient: class, IChatsClient
        where TChatParticipantsClient: class, IChatParticipantsClient
        where TChatMessagesClient: class, IChatMessagesClient
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

        protected virtual IHubContext<TChatsClient> ChatsHubContext => _lazyChatsHubContext.Value;
        protected virtual IHubContext<TChatParticipantsClient> ChatParticipantsHubContext => _lazyChatParticipantsHubContext.Value;
        protected virtual IHubContext<TChatMessagesClient> ChatMessagesHubContext => _lazyChatMessagesHubContext.Value;

        public virtual Task NotifyChatParticipants<TNotificationBase>(TNotificationBase notification, Guid chatId) where TNotificationBase : INotification
        {
            throw new NotImplementedException();
        }

        public virtual async Task NotifyUsers<TNotificationBase>(TNotificationBase notification, IEnumerable<Guid> userIds) where TNotificationBase : INotification
        {
            var stringUserId = userIds.Select(r => r.ToString()).ToList();

            await SendNotifications(notification, stringUserId);
        }

        protected virtual async Task SendNotifications<TNotificationBase>(TNotificationBase notification, List<string> stringUserId)
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
            if (notification is IChatMessageAddedNotification chatMessageAddedNotification)
            {
                await chatMessagesClient.ChatMessageAdded(chatMessageAddedNotification);
            }

            if (notification is IChatMessageEditedNotification chatMessageEditedNotification)
            {
                await chatMessagesClient.ChatMessageEdited(chatMessageEditedNotification);
            }

            if (notification is IChatMessageRemovedNotification chatMessageRemovedNotification)
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
            if (notification is IChatParticipantAddedNotification chatParticipantAddedNotification)
            {
                await chatParticipantsClient.ChatParticipantAdded(chatParticipantAddedNotification);
            }

            if (notification is IChatParticipantInvitedNotification chatParticipantInvitedNotification)
            {
                await chatParticipantsClient.ChatParticipantInvited(chatParticipantInvitedNotification);
            }

            if (notification is IChatParticipantAppliedNotification chatParticipantAppliedNotification)
            {
                await chatParticipantsClient.ChatParticipantApplied(chatParticipantAppliedNotification);
            }

            if (notification is IChatParticipantRemovedNotification chatParticipantRemovedNotification)
            {
                await chatParticipantsClient.ChatParticipantRemoved(chatParticipantRemovedNotification);
            }

            if (notification is IChatParticipantBlockedNotification chatParticipantBlockedNotification)
            {
                await chatParticipantsClient.ChatParticipantBlocked(chatParticipantBlockedNotification);
            }

            if (notification is IChatParticipantsAppendedNotification chatParticipantsAppendedNotification)
            {
                await chatParticipantsClient.ChatParticipantsAppended(chatParticipantsAppendedNotification);
            }

            if (notification is IChatParticipantTypeChangedNotification chatParticipantTypeChangedNotification)
            {
                await chatParticipantsClient.ChatParticipantTypeChanged(chatParticipantTypeChangedNotification);
            }
        }

        protected virtual async Task SendChatsNotifications<TNotificationBase>(TNotificationBase notification,
            TChatsClient chatsClient) where TNotificationBase : INotification
        {
            if (notification is IChatAddedNotification chatAddedNotification)
            {
                await chatsClient.ChatAdded(chatAddedNotification);
            }

            if (notification is IChatInfoEditedNotification chatInfoEditedNotification)
            {
                await chatsClient.ChatInfoEdited(chatInfoEditedNotification);
            }

            if (notification is IChatRemovedNotification chatRemovedNotification)
            {
                await chatsClient.ChatRemoved(chatRemovedNotification);
            }
        }
    }
}
