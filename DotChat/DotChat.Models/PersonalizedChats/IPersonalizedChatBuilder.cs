namespace K1vs.DotChat.Models.PersonalizedChats
{
    using K1vs.DotChat.Chats;
    using K1vs.DotChat.PersonalizedChats;

    public class PersonalizedChatBuilder : IPersonalizedChatBuilder
    {
        public IPersonalizedChat Build(IChat chat, long readIndex, long unreadCount)
        {
            return new PersonalizedChat(chat, readIndex, unreadCount);
        }
    }
}
