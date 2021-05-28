namespace K1vs.DotChat.Models.Chats
{
    using DotChat.Chats;

    public class ChatsSummary: IChatsSummary
    {
        public ChatsSummary()
        {
        }

        public ChatsSummary(long messagesCount, long chatsCount)
        {
            MessagesCount = messagesCount;
            ChatsCount = chatsCount;
        }

        public long MessagesCount { get; set; }
        public long ChatsCount { get; set; }
    }
}
