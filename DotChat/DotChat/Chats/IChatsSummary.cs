namespace K1vs.DotChat.Chats
{
    public interface IChatsSummary
    {
        long MessagesCount { get; }
        long ChatsCount { get; }
    }
}
