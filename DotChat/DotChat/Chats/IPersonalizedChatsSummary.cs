namespace K1vs.DotChat.Chats
{
    public interface IPersonalizedChatsSummary
    {
        long UnreadMessagesCount { get; }
        long UnreadChatsCount { get; }
    }
}
