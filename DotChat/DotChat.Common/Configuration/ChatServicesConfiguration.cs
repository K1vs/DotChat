namespace K1vs.DotChat.Common.Configuration
{
    using DotChat.Configuration;

    public class ChatServicesConfiguration: IChatServicesConfiguration
    {
        public ChatServicesConfiguration()
            : this(100, 100)
        {
        }

        public ChatServicesConfiguration(int defaultChatsPageSize, int defaultChatMessagesPageSize)
        {
            DefaultChatsPageSize = defaultChatsPageSize;
            DefaultChatMessagesPageSize = defaultChatMessagesPageSize;
        }

        public int DefaultChatsPageSize { get; }
        public int DefaultChatMessagesPageSize { get; }
    }
}
