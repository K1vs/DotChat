namespace K1vs.DotChat.Models.Chats
{
    using DotChat.Chats;

    public class PersonalizedChatsSummary: IPersonalizedChatsSummary
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
