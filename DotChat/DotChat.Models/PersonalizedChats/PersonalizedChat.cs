namespace K1vs.DotChat.Models.PersonalizedChats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;
    using K1vs.DotChat.Models.Chats;
    using K1vs.DotChat.PersonalizedChats;

    public class PersonalizedChat: IPersonalizedChat
    {
        public PersonalizedChat()
        {
        }

        public PersonalizedChat(IChat chat, long readIndex, long unreadCount)
            : base()
        {
            Chat = chat;
            ReadIndex = readIndex;
            UnreadCount = unreadCount;
        }

        public long ReadIndex { get; set; }
        public long UnreadCount { get; set; }
        public IChat Chat { get; set; }
    }
}
