namespace K1vs.DotChat.Models.PersonalizedChats
{
    using DotChat.Chats;
    using K1vs.DotChat.Models.Chats;
    using K1vs.DotChat.PersonalizedChats;

    public class PersonalizedChatsSummary : ChatsSummary, IPersonalizedChatsSummary
    {
        public PersonalizedChatsSummary()
        {
        }

        public PersonalizedChatsSummary(long unreadMessagesCount, long unreadChatsCount)
        {
            UnreadMessagesCount = unreadMessagesCount;
            UnreadChatsCount = unreadChatsCount;
        }

        public long UnreadMessagesCount { get; set; }
        public long UnreadChatsCount { get; set; }
    }
}
