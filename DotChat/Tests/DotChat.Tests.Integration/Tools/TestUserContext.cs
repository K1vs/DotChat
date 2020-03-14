namespace K1vs.DotChat.Tests.Integration.Tools
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Basic;
    using Basic.Chats;
    using Basic.Messages;
    using Basic.Notifications.Chats;
    using Basic.Notifications.Messages;
    using Basic.Participants;
    using Chats;
    using Demo.Others;
    using Exceptions;
    using K1vs.DotChat.Basic.Messages.Typed;
    using Messages;
    using Models.Chats;
    using Models.Messages.Typed;
    using Models.Participants;
    using Notifications;
    using Participants;

    public class TestUserContext
    {
        private readonly TimeSpan _checkWaitTime = TimeSpan.FromSeconds(3);
        private readonly int _tries = 3;
        private readonly IDotChat _dotChat;
        private readonly Guid _userId;
        private readonly EventNotificationSender _eventNotificationSender;
        private readonly ConcurrentDictionary<string, Guid> _chatIds;
        private readonly ConcurrentDictionary<Guid, PersonalizedChat> _chats = new ConcurrentDictionary<Guid, PersonalizedChat>();
        private readonly ConcurrentDictionary<Guid, List<ChatMessage>> _chatsMessages = new ConcurrentDictionary<Guid, List<ChatMessage>>();
        
        public TestUserContext(Guid userId, IDotChat dotChat, EventNotificationSender eventNotificationSender, ConcurrentDictionary<string, Guid> chatIds)
        {
            if (Debugger.IsAttached)
            {
                _tries = int.MaxValue;
            }
            _userId = userId;
            _dotChat = dotChat;
            _eventNotificationSender = eventNotificationSender;
            _chatIds = chatIds;
        }

        public async Task Activate()
        {
            var page = await _dotChat.Chats.GetPage(_userId);
            foreach (var item in page.Items)
            {
                _chatIds.TryAdd(item.Name, item.ChatId);
                _chats.TryAdd(item.ChatId, item);
                _chatsMessages.TryAdd(item.ChatId, new List<ChatMessage>());
            }
            _eventNotificationSender.OnNotification += TestNotificationSenderOnOnNotification;
        }

        public async Task Activate(string chat)
        {
            var chatId = _chatIds[chat];
            var messages = await _dotChat.ChatMessages.GetPage(_userId, _chatIds[chat]);
            var chatMessages = this._chatsMessages[chatId];
            chatMessages.AddRange(messages.Items);
        }

        public async Task CreateChat(string name, ChatPrivacyMode privacyMode, params Guid[] userIds)
        {
            var chatId = await _dotChat.Chats.Add(_userId, new ChatInfo(name, $"{name} description", privacyMode, 0),
                new ParticipationCandidates(userIds.Concat(Enumerable.Repeat(_userId, 1))
                    .Select(r => new ParticipationCandidate(r, ChatParticipantType.Participant)).ToList(), new List<ParticipationCandidate>()));
            _chatIds.TryAdd(name, chatId);
        }

        public Task AddTextMessage(string chat, string msg)
        {
            return _dotChat.ChatMessages.Add(_userId, _chatIds[chat], null,
                new ChatMessageInfo(MessageType.Text, 0, text: new TextMessage(msg)));
        }

        public async Task CheckMessages(string chatName, params string[] messages)
        {
            if(messages.Length == 0)
                return;
            var chat = _chatsMessages[_chatIds[chatName]];
            var i = 0;
            for (var tryIndex = 0; tryIndex < _tries; tryIndex++)
            {
                i = 0;
                foreach (var message in chat)
                {
                    if (messages[i] == message.Text?.Content)
                    {
                        i++;
                        if (i == messages.Length)
                        {
                            return;
                        }
                    }
                }
                await Task.Delay((int) _checkWaitTime.TotalMilliseconds / _tries);
            }
            throw new CheckMessageException($"Check message error. From message: {messages[i]}");
        }

        public void CheckNotMessages(string chatName, params string[] messages)
        {
            if (_chatsMessages.TryGetValue(_chatIds[chatName], out var chatMessages))
            {
                var badMessages = messages.Where(r => chatMessages.Where(r => r.Type.HasFlag(MessageType.Text)).Any(e => r == e.Text.Content)).ToList();
                if (badMessages.Any())
                {
                    throw new CheckMessageException($"Check NOT message error. Unnecessary messages: {string.Join(",", badMessages)}");
                }
            }
        }

        public async Task CheckChat(string name)
        {
            for (var tryIndex = 0; tryIndex < _tries; tryIndex++)
            {
                if (_chats.ContainsKey(_chatIds[name]))
                {
                    return;
                }
                await Task.Delay((int)_checkWaitTime.TotalMilliseconds / _tries);
            }
            throw new CheckChatException($"Check chat error. Name: {name}");
        }

        public void CheckNotChat(string name)
        {
            if (_chats.ContainsKey(_chatIds[name]))
            {
                throw new CheckChatException($"Check NOT chat error. Unnecessary chat: {name}");
            }
        }

        private void TestNotificationSenderOnOnNotification(INotification notification, Guid user)
        {
            if (user == _userId)
            {
                Guid chatId = Guid.Empty;
                if (notification is IChatRelated chatRelated)
                {
                    chatId = chatRelated.ChatId;
                }
                else if (notification is IHasPersonalizedChat<PersonalizedChat, Chat, ChatInfo, List<ChatParticipant>, ChatParticipant, ChatUser, ChatMessageInfo, TextMessage, QuoteMessage, List<MessageAttachment>, MessageAttachment, List<ChatRefMessage>, ChatRefMessage, List<ContactMessage>, ContactMessage> hasPersonalizedChat)
                {
                    chatId = hasPersonalizedChat.PersonalizedChat.ChatId;
                }
                var chatMessages = _chatsMessages.GetOrAdd(chatId, k => new List<ChatMessage>());
                if (notification is ChatMessageAddedNotification msgAdded)
                {
                    chatMessages.Add(msgAdded.Message);
                }
                else if (notification is ChatAddedNotification chatAdded)
                {
                    _chatIds.TryAdd(chatAdded.PersonalizedChat.Name, chatAdded.PersonalizedChat.ChatId);
                    _chats.TryAdd(chatAdded.PersonalizedChat.ChatId, chatAdded.PersonalizedChat);
                }
            }
        }
    }
}
