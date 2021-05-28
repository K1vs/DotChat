using K1vs.DotChat.Chats;

namespace K1vs.DotChat.PersonalizedChats
{
    public interface IPersonalizedChatBuilder
    {
        IPersonalizedChat Build(IChat chat, long readIndex, long unreadCount);
    }
}
