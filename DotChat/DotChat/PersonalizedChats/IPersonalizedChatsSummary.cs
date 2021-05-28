namespace K1vs.DotChat.PersonalizedChats
{
    using K1vs.DotChat.Chats;

    public interface IPersonalizedChatsSummary: IChatsSummary
    {
        long UnreadMessagesCount { get; }
        long UnreadChatsCount { get; }
    }
}
